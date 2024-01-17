using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Nethereum.KeyStore;
using Nethereum.Web3.Accounts;
using System.Security.Cryptography;
using System.Numerics;
using Newtonsoft.Json;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.FunctionEncoding.AttributeEncoding;
using Nethereum.JsonRpc.WebSocketStreamingClient;
using System.Reflection;
using System.Reflection.Emit;
using Nethereum.Hex.HexConvertors;
using Nethereum.Hex.HexTypes;
using Nethereum.Signer;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Infrastructure;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Reactive.Eth.Subscriptions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Nethereum.Web3;
using Nethereum.HdWallet;
using Nethereum.Signer.Crypto;

namespace RO.Common3.Ethereum.RPC
{
    #region newtonsoft custom JSON coverter
    class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    public class MyHexRPCTypeJsonConverter<TValue> : JsonConverter where TValue : HexBigInteger
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var hexRPCType = (TValue)value;
            writer.WriteValue(hexRPCType.HexValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return new HexBigInteger(reader.Value is string ? (string)reader.Value : "0x" + string.Format("{0:X}", reader.Value));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TValue);
        }
    }
    public class BytesToHexConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(byte[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var hex = serializer.Deserialize<string>(reader);
                if (!string.IsNullOrEmpty(hex))
                {
                    return hex.HexToByteArray();
                }
            }
            return Enumerable.Empty<byte>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytes = value as byte[];
            serializer.Serialize(writer, bytes.ToHex(true));
        }
    }
    #endregion

    #region web3 JSONRPC custom DTO
    public class Block
    {
        /// <summary>
        ///     QUANTITY - the block number. null when its pending block. 
        /// </summary>
        [JsonProperty(PropertyName = "number")]
        public HexBigInteger Number { get; set; }

        /// <summary>
        ///     DATA, 32 Bytes - hash of the block.  
        /// </summary>
        [JsonProperty(PropertyName = "hash")]
        public string BlockHash { get; set; }

        /// <summary>
        ///  block author.
        /// </summary>
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }


        /// <summary>
        ///  Seal fiels. 
        /// </summary>
        [JsonProperty(PropertyName = "sealFields")]
        public string[] SealFields { get; set; }

        /// <summary>
        ///     DATA, 32 Bytes - hash of the parent block. 
        /// </summary>
        [JsonProperty(PropertyName = "parentHash")]
        public string ParentHash { get; set; }

        /// <summary>
        ///     DATA, 8 Bytes - hash of the generated proof-of-work. null when its pending block. 
        /// </summary>
        [JsonProperty(PropertyName = "nonce")]
        public string Nonce { get; set; }

        /// <summary>
        ///     DATA, 32 Bytes - SHA3 of the uncles data in the block. 
        /// </summary>
        [JsonProperty(PropertyName = "sha3Uncles")]
        public string Sha3Uncles { get; set; }


        /// <summary>
        ///     DATA, 256 Bytes - the bloom filter for the logs of the block. null when its pending block. 
        /// </summary>
        [JsonProperty(PropertyName = "logsBloom")]
        public string LogsBloom { get; set; }


        /// <summary>
        ///     DATA, 32 Bytes - the root of the transaction trie of the block.
        /// </summary>
        [JsonProperty(PropertyName = "transactionsRoot")]
        public string TransactionsRoot { get; set; }

        /// <summary>
        ///     DATA, 32 Bytes - the root of the final state trie of the block.
        /// </summary>
        [JsonProperty(PropertyName = "stateRoot")]
        public string StateRoot { get; set; }

        /// <summary>
        ///     DATA, 32 Bytes - the root of the receipts trie of the block. 
        /// </summary>
        [JsonProperty(PropertyName = "receiptsRoot")]
        public string ReceiptsRoot { get; set; }

        /// <summary>
        ///     DATA, 20 Bytes - the address of the beneficiary to whom the mining rewards were given.
        /// </summary>
        [JsonProperty(PropertyName = "miner")]
        public string Miner { get; set; }

        /// <summary>
        ///     QUANTITY - integer of the difficulty for this block.   
        /// </summary>
        [JsonProperty(PropertyName = "difficulty")]
        public HexBigInteger Difficulty { get; set; }

        /// <summary>
        ///     QUANTITY - integer of the total difficulty of the chain until this block.
        /// </summary>
        [JsonProperty(PropertyName = "totalDifficulty")]
        public HexBigInteger TotalDifficulty { get; set; }

        /// <summary>
        ///     DATA - the "mix hash" field of this block.  
        /// </summary>
        [JsonProperty(PropertyName = "mixHash")]
        public string MixHash { get; set; }

        /// <summary>
        ///     DATA - the "extra data" field of this block.  
        /// </summary>
        [JsonProperty(PropertyName = "extraData")]
        public string ExtraData { get; set; }

        /// <summary>
        ///     QUANTITY - integer the size of this block in bytes. 
        /// </summary>
        [JsonConverter(typeof(MyHexRPCTypeJsonConverter<HexBigInteger>))]
        [JsonProperty(PropertyName = "size")]
        public HexBigInteger Size { get; set; }

        /// <summary>
        ///     QUANTITY - the maximum gas allowed in this block. 
        /// </summary>
        [JsonProperty(PropertyName = "gasLimit")]
        public HexBigInteger GasLimit { get; set; }

        /// <summary>
        ///     QUANTITY - the total used gas by all transactions in this block. 
        /// </summary>
        [JsonProperty(PropertyName = "gasUsed")]
        public HexBigInteger GasUsed { get; set; }

        /// <summary>
        ///     QUANTITY - the unix timestamp for when the block was collated.
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public HexBigInteger Timestamp { get; set; }

        /// <summary>
        ///     Array - Array of uncle hashes.
        /// </summary>
        [JsonProperty(PropertyName = "uncles")]
        public string[] Uncles { get; set; }
    }
    public class BlockWithTransactions : Block
    {
        /// <summary>
        ///     Array - Array of transaction objects
        /// </summary>
        [JsonProperty(PropertyName = "transactions")]
        public Nethereum.RPC.Eth.DTOs.Transaction[] Transactions { get; set; }
    }
    public class TxPoolContent
    {
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<int, List<Nethereum.RPC.Eth.DTOs.Transaction>>> content;
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<int, List<Nethereum.RPC.Eth.DTOs.Transaction>>> pending;
    }
    public class SignedTransaction
    {
        public string raw;
        public Transaction tx;
    }
    #endregion

    #region custom web3 JSONRPC calls
    public class EthSignTransaction : RpcRequestResponseHandler<SignedTransaction>
    {
        public EthSignTransaction(IClient client)
            : base(client, "eth_signTransaction")
        {
        }

        public Task<SignedTransaction> SendRequestAsync(TransactionInput input, object id = null)
        {
            return base.SendRequestAsync(id, input);
        }

        public RpcRequest BuildRequest(TransactionInput input, object id = null)
        {
            return base.BuildRequest(id, input);
        }

    }

    public class EthRpcRequest<T> : Nethereum.JsonRpc.Client.RpcRequestResponseHandler<T>
    {
        public EthRpcRequest(Nethereum.JsonRpc.Client.IClient client, string callName)
            : base(client, callName)
        {
        }
    }
    public class EthGetBlockByNumber : Nethereum.JsonRpc.Client.RpcRequestResponseHandler<Block>
    {
        public EthGetBlockByNumber(Nethereum.JsonRpc.Client.IClient client)
            : base(client, "eth_getBlockByNumber")
        {
        }

        public Task<Block> SendRequestAsync(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, object id = null)
        {
            if (blockParameter == null) throw new ArgumentNullException("blockParameter");
            return base.SendRequestAsync(id, blockParameter, false);
        }

        public Task<Block> SendRequestAsync(Nethereum.Hex.HexTypes.HexBigInteger number, object id = null)
        {
            if (number == null) throw new ArgumentNullException("number");
            return base.SendRequestAsync(id, number, false);
        }

        public Nethereum.JsonRpc.Client.RpcRequest BuildRequest(Nethereum.Hex.HexTypes.HexBigInteger number, object id = null)
        {
            if (number == null) throw new ArgumentNullException("number");
            return base.BuildRequest(id, number, false);
        }

        public Nethereum.JsonRpc.Client.RpcRequest BuildRequest(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, object id = null)
        {
            if (blockParameter == null) throw new ArgumentNullException("blockParameter");
            return base.BuildRequest(id, blockParameter, false);
        }
    }
    public class EthGetBlockByHash : Nethereum.JsonRpc.Client.RpcRequestResponseHandler<Block>
    {
        public EthGetBlockByHash(Nethereum.JsonRpc.Client.IClient client)
            : base(client, "eth_getBlockByHash")
        {
        }

        public Task<Block> SendRequestAsync(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, object id = null)
        {
            if (blockParameter == null) throw new ArgumentNullException("blockParameter");
            return base.SendRequestAsync(id, blockParameter, false);
        }

        public Task<Block> SendRequestAsync(Nethereum.Hex.HexTypes.HexBigInteger number, object id = null)
        {
            if (number == null) throw new ArgumentNullException("number");
            return base.SendRequestAsync(id, number, false);
        }

        public Nethereum.JsonRpc.Client.RpcRequest BuildRequest(Nethereum.Hex.HexTypes.HexBigInteger number, object id = null)
        {
            if (number == null) throw new ArgumentNullException("number");
            return base.BuildRequest(id, number, false);
        }

        public Nethereum.JsonRpc.Client.RpcRequest BuildRequest(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, object id = null)
        {
            if (blockParameter == null) throw new ArgumentNullException("blockParameter");
            return base.BuildRequest(id, blockParameter, false);
        }
    }
    public class EthGetBlockWithTransactionsByNumber : Nethereum.JsonRpc.Client.RpcRequestResponseHandler<BlockWithTransactions>
    {
        public EthGetBlockWithTransactionsByNumber(Nethereum.JsonRpc.Client.IClient client)
            : base(client, "eth_getBlockByNumber")
        {
        }

        public Task<BlockWithTransactions> SendRequestAsync(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, object id = null)
        {
            if (blockParameter == null) throw new ArgumentNullException("blockParameter");
            return base.SendRequestAsync(id, blockParameter, true);
        }

        public Task<BlockWithTransactions> SendRequestAsync(Nethereum.Hex.HexTypes.HexBigInteger number, object id = null)
        {
            if (number == null) throw new ArgumentNullException("number");
            return base.SendRequestAsync(id, number, true);
        }

        public Nethereum.JsonRpc.Client.RpcRequest BuildRequest(Nethereum.Hex.HexTypes.HexBigInteger number, object id = null)
        {
            if (number == null) throw new ArgumentNullException("number");
            return base.BuildRequest(id, number, true);
        }

        public Nethereum.JsonRpc.Client.RpcRequest BuildRequest(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, object id = null)
        {
            if (blockParameter == null) throw new ArgumentNullException("blockParameter");
            return base.BuildRequest(id, blockParameter, true);
        }
    }
    public class EthGetBlockWithTransactionsByHash : Nethereum.JsonRpc.Client.RpcRequestResponseHandler<BlockWithTransactions>
    {
        public EthGetBlockWithTransactionsByHash(Nethereum.JsonRpc.Client.IClient client)
            : base(client, "eth_getBlockByHash")
        {
        }

        public Task<BlockWithTransactions> SendRequestAsync(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, object id = null)
        {
            if (blockParameter == null) throw new ArgumentNullException("blockParameter");
            return base.SendRequestAsync(id, blockParameter, true);
        }

        public Task<BlockWithTransactions> SendRequestAsync(Nethereum.Hex.HexTypes.HexBigInteger number, object id = null)
        {
            if (number == null) throw new ArgumentNullException("number");
            return base.SendRequestAsync(id, number, true);
        }

        public Nethereum.JsonRpc.Client.RpcRequest BuildRequest(Nethereum.Hex.HexTypes.HexBigInteger number, object id = null)
        {
            if (number == null) throw new ArgumentNullException("number");
            return base.BuildRequest(id, number, true);
        }

        public Nethereum.JsonRpc.Client.RpcRequest BuildRequest(Nethereum.RPC.Eth.DTOs.BlockParameter blockParameter, object id = null)
        {
            if (blockParameter == null) throw new ArgumentNullException("blockParameter");
            return base.BuildRequest(id, blockParameter, true);
        }
    }
    public class GEthPendingTransactions : RpcRequestResponseHandler<Nethereum.RPC.Eth.DTOs.Transaction[]>
    {
        public GEthPendingTransactions(IClient client)
            : base(client, "eth_pendingTransactions")
        {
        }


        public Task<Nethereum.RPC.Eth.DTOs.Transaction[]> SendRequestAsync(object id = null)
        {
            return base.SendRequestAsync(id);
        }


        public RpcRequest BuildRequest(object id = null)
        {
            return base.BuildRequest(id);
        }
    }
    public class EthChainId : RpcRequestResponseHandler<Nethereum.Hex.HexTypes.HexBigInteger>
    {
        public EthChainId(IClient client)
            : base(client, "eth_chainId")
        {
        }


        public Task<Nethereum.Hex.HexTypes.HexBigInteger> SendRequestAsync(object id = null)
        {
            return base.SendRequestAsync(id);
        }


        public RpcRequest BuildRequest(object id = null)
        {
            return base.BuildRequest(id);
        }
    }
    public class GEthTxPoolContent : RpcRequestResponseHandler<TxPoolContent>
    {
        public GEthTxPoolContent(IClient client)
            : base(client, "txpool_content")
        {
        }


        public Task<TxPoolContent> SendRequestAsync(object id = null)
        {
            return base.SendRequestAsync(id);
        }


        public RpcRequest BuildRequest(object id = null)
        {
            return base.BuildRequest(id);
        }
    }
    #endregion
}

namespace RO.Common3.Ethereum
{
    /// <summary>
    /// Represents the definition of a dynamic property which can be added to an object at runtime.
    /// </summary>
    public class DTODynamicProperty
    {
        /// <summary>
        /// The Name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The Display Name of the property for the end-user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The Name of the underlying System Type of the property.
        /// </summary>
        public string PropertyTypeName { get; set; }

        public Type PropertyType { get; set; }

        /// <summary>
        /// The underlying System Type of the property.
        /// </summary>
        public ParameterAttribute Attributes { get; set; }
    }
    /// <summary>
    /// Generates new Types with dynamically added properties.
    /// </summary>
    public class DTODynamicTypeFactory
    {
        #region Fields

        private TypeBuilder _typeBuilder;

        #endregion

        #region Readonlys

        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly ModuleBuilder _moduleBuilder;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DTODynamicTypeFactory()
        {
            var uniqueIdentifier = Guid.NewGuid().ToString();
            var assemblyName = new AssemblyName(uniqueIdentifier);

            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(uniqueIdentifier);
        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Creates a new Type based on the specified parent Type and attaches dynamic properties.
        /// </summary>
        /// <param name="parentType">The parent Type to base the new Type on</param>
        /// <param name="dynamicProperties">The collection of dynamic properties to attach to the new Type</param>
        /// <returns>An extended Type with dynamic properties added to it</returns>
        public Type CreateNewTypeWithDynamicProperties(Type parentType, IEnumerable<DTODynamicProperty> dynamicProperties)
        {
            _typeBuilder = _moduleBuilder.DefineType(parentType.Name + Guid.NewGuid().ToString(), TypeAttributes.Public);
            _typeBuilder.SetParent(parentType);

            foreach (DTODynamicProperty property in dynamicProperties)
                AddDynamicPropertyToType(property);

            return _typeBuilder.CreateType();
        }

        #endregion

        #region Private

        /// <summary>
        /// Adds the specified dynamic property to the new type.
        /// </summary>
        /// <param name="dynamicProperty">The definition of the dynamic property to add to the Type</param>
        private void AddDynamicPropertyToType(DTODynamicProperty dynamicProperty)
        {
            Type propertyType = dynamicProperty.PropertyType;
            string propertyName = dynamicProperty.PropertyName;
            string fieldName = "_" + propertyName;

            FieldBuilder fieldBuilder = _typeBuilder.DefineField(fieldName, propertyType, FieldAttributes.Private);

            // The property set and get methods require a special set of attributes.
            MethodAttributes getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            // Define the 'get' accessor method.
            MethodBuilder getMethodBuilder = _typeBuilder.DefineMethod("get_" + propertyName, getSetAttributes, propertyType, Type.EmptyTypes);
            ILGenerator propertyGetGenerator = getMethodBuilder.GetILGenerator();
            propertyGetGenerator.Emit(OpCodes.Ldarg_0);
            propertyGetGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            propertyGetGenerator.Emit(OpCodes.Ret);

            // Define the 'set' accessor method.
            MethodBuilder setMethodBuilder = _typeBuilder.DefineMethod("set_" + propertyName, getSetAttributes, null, new Type[] { propertyType });
            ILGenerator propertySetGenerator = setMethodBuilder.GetILGenerator();
            propertySetGenerator.Emit(OpCodes.Ldarg_0);
            propertySetGenerator.Emit(OpCodes.Ldarg_1);
            propertySetGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            propertySetGenerator.Emit(OpCodes.Ret);

            // Lastly, we must map the two methods created above to a PropertyBuilder and their corresponding behaviors, 'get' and 'set' respectively.
            PropertyBuilder propertyBuilder = _typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);

            // Add a 'DisplayName' attribute.
            var attributeType = typeof(DisplayNameAttribute);
            var attributeBuilder = new CustomAttributeBuilder(
                attributeType.GetConstructor(new Type[] { typeof(string) }), // Constructor selection.
                new object[] { dynamicProperty.DisplayName }, // Constructor arguments.
                new PropertyInfo[] { }, // Properties to assign to.                    
                new object[] { } // Values for property assignment.
                );
            propertyBuilder.SetCustomAttribute(attributeBuilder);

            // Add a 'ParameterAttribute' attribute that is key to Nethereum Encode/Decode
            attributeType = typeof(ParameterAttribute);
            attributeBuilder = new CustomAttributeBuilder(
                attributeType.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(int), typeof(bool) }), // Constructor selection.
                new object[] { dynamicProperty.Attributes.Type, dynamicProperty.Attributes.Name, dynamicProperty.Attributes.Order, false }, // Constructor arguments.
                new PropertyInfo[] { }, // Properties to assign to.                    
                new object[] { } // Values for property assignment.
                );
            propertyBuilder.SetCustomAttribute(attributeBuilder);

        }

        #endregion

        #endregion
    }

    public class EthTxOptions
    {
        public EthereumAccount fromAccount;
        public BigInteger AmountToSend;
        public BigInteger? MaxFeePerGas;
        public BigInteger? MaxPriorityFeePerGas;
        public BigInteger? GasPrice;
        public BigInteger? Nonce;
        public BigInteger? GasLimit;

        public T ApplyTo<T>(T functionParams, Nethereum.Web3.Web3 web3) where T : Nethereum.Contracts.FunctionMessage
        {
            functionParams.AmountToSend = AmountToSend;
            functionParams.MaxFeePerGas = MaxFeePerGas;
            functionParams.MaxPriorityFeePerGas = MaxPriorityFeePerGas;
            functionParams.GasPrice = GasPrice;
            functionParams.Nonce = Nonce;
            functionParams.Gas = GasLimit;

            // use whatever is there if specified
            if (!string.IsNullOrEmpty(functionParams.FromAddress)) return functionParams;

            if (web3.TransactionManager.Account == null)
            {
                if (fromAccount == null || string.IsNullOrEmpty(fromAccount.myAddress))
                {
                    if (web3.TransactionManager.Account == null || string.IsNullOrEmpty(web3.TransactionManager.Account.Address))
                    {
                        var roEthereum = new RO.Common3.Ethereum.Ethereum();
                        functionParams.FromAddress = roEthereum.GetDefaultAccount(web3).Result;
                    }
                    else
                    {
                        functionParams.FromAddress = web3.TransactionManager.Account.Address;
                    }
                }
                else
                {
                    functionParams.FromAddress = fromAccount.myAddress;
                }
            }
            else
            {
                functionParams.FromAddress = web3.TransactionManager.Account.Address;
            }
            return functionParams;
        }
    }

    public class Eth2DepositJson
    {
        public string pubkey;
        public string withdrawal_credentials;
        public Int64 amount;
        public string signature;
        public string deposit_message_root;
        public string deposit_data_root;
        public string fork_version;
        public string eth2_network_name;
        public string deposit_cli_version;
    }
    public class Eht2KeyStoreCryptoParam
    {
        public int? dklen;
        public int? n;
        public int? r;
        public int? p;
        public string salt;
        public string iv;
    }
    public class Eth2KeyStoreCryptoChecksum
    {
        public string function;
        [JsonProperty("params")]
        public Eht2KeyStoreCryptoParam Params;
        public string message;
    }
    public class Eth2KeyStoreCryptoCipher
    {
        public string function;
        [JsonProperty("params")]
        public Eht2KeyStoreCryptoParam Params;
        public string message;
    }
    public class Eth2KeyStoreCryptoKdf
    {
        public string function;
        [JsonProperty("params")]
        public Eht2KeyStoreCryptoParam Params;
        public string message;
    }
    public class Eth2KeyStoreCypto
    {
        public Eth2KeyStoreCryptoKdf kdf;
        public Eth2KeyStoreCryptoChecksum checksum;
        public Eth2KeyStoreCryptoCipher cipher;
    }

    public class Eth2KeyStoreJson
    {
        public Eth2KeyStoreCypto crypto;
        public string description;
        public string pubkey;
        public string path;
        public string uuid;
        public int version;
    }

    /* partial port from RO */
    internal partial class Utils
    {
        private static Dictionary<char, byte> hexLookupTable = new Dictionary<char, byte>{
                {'0',0},{'1',1},{'2',2},{'3',3},{'4',4},{'5',5},{'6',6},{'7',7},{'8',8},{'9',9},{'a',10},{'b',11},{'c',12},{'d',13},{'e',14},{'f',15}
            };

        public static byte[] HexToByteArray(string hexString)
        {
            List<byte> byteArray = new List<byte>();
            if (string.IsNullOrEmpty(hexString)) return null;
            if (!hexString.ToLower().StartsWith("0x") || hexString.Length % 2 != 0 || hexString.Length == 2) throw new Exception("malformed hex string, must start with 0x with zero padding for each byte(i.e. even length)");
            var lowerHexString = System.Text.Encoding.ASCII.GetBytes(hexString.Substring(2).ToLower());
            for (var idx = 0; idx < lowerHexString.Length; idx += 2)
            {
                byte upper = lowerHexString[idx];
                byte lower = lowerHexString[idx + 1];
                byteArray.Add(unchecked((byte)((hexLookupTable[(char)upper] << 4) + (hexLookupTable[(char)lower]))));
            }
            return byteArray.ToArray();
        }
    }
    public class EthereumRPCException : Exception
    {
        public EthereumRPCException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
        public EthereumRPCException(string msg)
            : base(msg)
        {
        }
    }
    public class TransactionReceipt
    {
        public string TransactionHash { get; private set; }
        public string GasUsed { get; private set; }
        public string GasLimit { get; private set; }
        public string BlockNumber { get; private set; }
        public string ContractAddress { get; private set; }
        public string Status { get; private set; }

        public TransactionReceipt(Dictionary<string, string> txReceipt)
        {
            if (txReceipt != null)
            {
                Status = txReceipt["Status"];
                GasUsed = txReceipt["GasUsed"];
                GasLimit = txReceipt["GasLimit"];
                BlockNumber = txReceipt["BlockNumber"];
                ContractAddress = txReceipt["ContractAddress"];
                TransactionHash = txReceipt["TransactionHash"];
            }
        }
    }
    public class EventTopicDecoder : Nethereum.ABI.FunctionEncoding.ParameterDecoder
    {
        public Dictionary<string, object> DecodeAttributes(string output, Dictionary<string, object> result, PropertyInfo[] properties, object sample, IEnumerable<Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute> attributes, bool useAbiFieldName)
        {
            if (output == "0x") return result;
            var parameterObjects = new List<ParameterOutputProperty>();
            Dictionary<string, PropertyInfo> propertiesByName = properties.ToDictionary(x => x.Name, x => x);
            Dictionary<string, Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute> attributesByName = attributes.ToDictionary(x => x.Parameter.Name, x => x);

            if (sample is System.Dynamic.ExpandoObject)
            {
                IDictionary<string, object> d = sample as IDictionary<string, object>;
                foreach (var propertyName in d.Keys)
                {
                    if (attributesByName.ContainsKey(propertyName))
                    {
                        var parameter = attributesByName[propertyName].Parameter;
                        var parameterOutputProperty = new ParameterOutputProperty
                        {
                            Parameter = parameter
                            //DecodedType = d[propertyName].GetType()
                        };
                        parameterOutputProperty.Parameter.DecodedType = d[propertyName].GetType();
                        parameterObjects.Add(parameterOutputProperty);
                    }
                }
            }
            else
            {
                foreach (var propertyName in propertiesByName.Keys)
                {
                    if (attributesByName.ContainsKey(propertyName))
                    {
                        var parameter = attributesByName[propertyName].Parameter;
                        var parameterOutputProperty = new ParameterOutputProperty
                        {
                            Parameter = parameter
                            //DecodedType = propertiesByName[propertyName].PropertyType
                        };
                        parameterOutputProperty.Parameter.DecodedType = propertiesByName[propertyName].PropertyType;
                        parameterObjects.Add(parameterOutputProperty);
                    }
                }
            }

            var orderedParameters = parameterObjects.OrderBy(x => x.Parameter.Order).ToArray();
            var parameterResults = DecodeOutput(output, orderedParameters);

            foreach (var parameterResult in parameterResults)
            {
                var parameter = (ParameterOutputProperty)parameterResult;
                var abiType = parameter.Parameter.Type;
                if (abiType.StartsWith("byte") && parameter.Result is byte[])
                {
                    result[parameter.Parameter.Name] = "0x" + (((byte[])parameter.Result).Length == 0 ? "" : BitConverter.ToString((byte[])parameter.Result).Replace("-", "").ToLowerInvariant());
                }
                else
                    result[parameter.Parameter.Name] = parameter.Result;
            }

            return result;
        }

        public Dictionary<string, object> DecodeTopics<T>(object[] topics, string data, T memberType, IEnumerable<Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute> attributes, bool useAbiFieldName)
        {

            var indexedProperties = attributes.Where(x => x.Parameter.Indexed == true).OrderBy(x => x.Order).ToArray();
            var dataProperties = attributes.Where(x => x.Parameter.Indexed == false).OrderBy(x => x.Order).ToArray();
            var type = typeof(T);
            var typeProperties = type.GetTypeInfo().GetProperties();
            var topicNumber = 0;
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (var topic in topics)
            {
                //skip the first one as it is the signature
                if (topicNumber > 0)
                {
                    var property = indexedProperties[topicNumber - 1];
                    var typeInfo = type.GetTypeInfo().GetProperty(property.Parameter.Name);
                    var attribute = property;
                    //skip dynamic types as the topic value is the sha3 keccak
                    if (!attribute.Parameter.ABIType.IsDynamic())
                    {
                        result = DecodeAttributes(topic.ToString(), result, typeProperties, memberType, new List<Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute> { attribute }, useAbiFieldName);
                    }
                    else
                    {
                        if (typeInfo.PropertyType != typeof(string))
                            throw new Exception(
                                "Indexed Dynamic Types (string, arrays) value is the Keccak SHA3 of the value, the property type of " +
                                property.Name + "should be a string");
                        result[typeInfo.Name] = topic.ToString();
                    }
                }
                topicNumber = topicNumber + 1;
            }

            // var dataProperties = properties.Where(x => x.GetCustomAttribute<ParameterAttribute>().Order >= topicNumber);
            result = DecodeAttributes(data, result, typeProperties, memberType, dataProperties.ToArray(), useAbiFieldName);
            return result;
        }
    }

    public class FunctionOutputDecoder : Nethereum.ABI.FunctionEncoding.ParameterDecoder
    {
        public static readonly DTODynamicTypeFactory dtoDynamicTypeFactory = new DTODynamicTypeFactory();

        public static IList GetList(Type type)
        {
            return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
        }

        public static object MapToSystemTypeObj(Nethereum.ABI.Model.Parameter p)
        {
            Type defaultType = p.ABIType.GetDefaultDecodingType();
            var obj = p.ABIType.Name == "string" || p.ABIType.Name == "address"
                    ? (object)""
                    : p.ABIType.Name.StartsWith("byte") ? (object)new byte[1]
                    : (object)Activator.CreateInstance(defaultType);
            if (p.ABIType.IsDynamic()
                    && p.ABIType.Name != "string"
                    && !p.ABIType.Name.StartsWith("byte")
                    && p.ABIType.CanonicalName != "tuple"
                )
            {
                ((IList)obj).Add(p.ABIType.CanonicalName == "tuple[]" ? (object)new Dictionary<string, object>() : (object)"");
            }
            return obj;
        }
        public Dictionary<string, object> DecodeAttributes(string output, Dictionary<string, object> result, PropertyInfo[] properties, object sample, IEnumerable<Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute> attributes, IEnumerable<Nethereum.ABI.Model.Parameter> outputParams, bool useAbiFieldName)
        {
            if (output == "0x") return result;
            var parameterObjects = new List<ParameterOutputProperty>();
            Dictionary<string, PropertyInfo> propertiesByName = properties.ToDictionary(x => x.Name, x => x);
            Dictionary<string, Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute> attributesByName = attributes.ToDictionary(x => string.IsNullOrEmpty(x.Parameter.Name) ? "p" + (x.Order - 1).ToString() : x.Parameter.Name, x => x);
            Dictionary<string, Nethereum.ABI.Model.Parameter> outputParamsByName = outputParams.ToDictionary(x => string.IsNullOrEmpty(x.Name) ? "p" + (x.Order - 1).ToString() : x.Name, x => x);
            if (sample is System.Dynamic.ExpandoObject)
            {
                IDictionary<string, object> d = sample as IDictionary<string, object>;
                foreach (var propertyName in d.Keys)
                {
                    if (attributesByName.ContainsKey(propertyName))
                    {
                        var parameter = outputParamsByName.ContainsKey(propertyName) ? outputParamsByName[propertyName] : attributesByName[propertyName].Parameter;

                        var parameterOutputProperty = new ParameterOutputProperty
                        {
                            Parameter = parameter,
                        };
                        if (parameter.ABIType is Nethereum.ABI.ArrayType && parameter.ABIType.Name.StartsWith("tuple["))
                        {
                            // tuple array
                            Nethereum.ABI.ArrayType a = parameter.ABIType as Nethereum.ABI.ArrayType;
                            FieldInfo type = a.GetType().GetField("ElementType", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                            PropertyInfo typeP = a.GetType().GetProperty("ElementType", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                            Nethereum.ABI.TupleType elementType = type != null ? type.GetValue(a) as Nethereum.ABI.TupleType : typeP.GetValue(a) as Nethereum.ABI.TupleType;
                            List<DTODynamicProperty> dtoProperties = elementType.Components.Select(param =>
                            {
                                return new DTODynamicProperty()
                                {
                                    PropertyType = MapToSystemTypeObj(param).GetType(),
                                    PropertyName = param.Name,
                                    DisplayName = param.Name,
                                    Attributes = new ParameterAttribute(param.ABIType.CanonicalName, param.Name, param.Order, param.Indexed)
                                };
                            }).ToList();
                            Type functionOutputDTO = dtoDynamicTypeFactory.CreateNewTypeWithDynamicProperties(typeof(object), dtoProperties);
                            parameterOutputProperty.Parameter.DecodedType = GetList(functionOutputDTO).GetType();
                        }
                        else
                        {
                            var dataType = d[propertyName].GetType().ToString();
                            parameterOutputProperty.Parameter.DecodedType = dataType.EndsWith("[]") && parameter.ABIType.IsDynamic()
                                //? (new List<byte[]>()).GetType() 
                                                ? (parameter.DecodedType ?? d[propertyName].GetType())
                                                : d[propertyName].GetType();
                        } 
                        parameterObjects.Add(parameterOutputProperty);
                    }
                }
            }
            else
            {
                foreach (var propertyName in propertiesByName.Keys)
                {
                    if (attributesByName.ContainsKey(propertyName))
                    {
                        if (propertiesByName[propertyName].PropertyType.ToString().EndsWith("[]") && attributesByName[propertyName].Parameter.ABIType.IsDynamic())
                        {
                            throw new Exception(
                                "function return values of array type " +
                                propertyName + " should be declare as List<>");
                        }
                        var parameter = attributesByName[propertyName].Parameter;
                        var parameterOutputProperty = new ParameterOutputProperty
                        {
                            Parameter = parameter
                            //DecodedType = propertiesByName[propertyName].PropertyType
                        };
                        parameterOutputProperty.Parameter.DecodedType = propertiesByName[propertyName].PropertyType;
                        parameterObjects.Add(parameterOutputProperty);
                    }
                }
            }

            var orderedParameters = parameterObjects.OrderBy(x => x.Parameter.Order).ToArray();
            var parameterResults = DecodeOutput(output, orderedParameters);

            var idx = 0;
            var resultCount = parameterResults.Count;
            foreach (var parameterResult in parameterResults)
            {
                var parameter = (ParameterOutputProperty)parameterResult;
                var propertyInfo = parameter.PropertyInfo;
                var abiType = parameter.Parameter.Type;
                var fieldName = string.IsNullOrEmpty(parameter.Parameter.Name) || !useAbiFieldName
                    ? string.Format(resultCount > 9 ? "p{0:D2}" : "p{0}", idx)
                                        + (string.IsNullOrEmpty(parameter.Parameter.Name)
                                            ? ""
                                            : "(" + parameter.Parameter.Name + ")"
                                          )
                                    : parameter.Parameter.Name;
                Func<List<Nethereum.ABI.FunctionEncoding.ParameterOutput>, Dictionary<string, object>> decodeTuple = null;
                decodeTuple = ((List<Nethereum.ABI.FunctionEncoding.ParameterOutput> results) =>
                {
                    Dictionary<string, object> o = new Dictionary<string, object>();
                    foreach (var v in results)
                    {
                        if (v.Parameter.ABIType is Nethereum.ABI.TupleType)
                        {
                            o[v.Parameter.Name] = decodeTuple(v.Result as List<Nethereum.ABI.FunctionEncoding.ParameterOutput>);
                        }
                        else
                        {
                            o[v.Parameter.Name] = v.Result;
                        }
                    }
                    return o;
                });
                if (abiType.StartsWith("uint") && parameter.Result is BigInteger)
                {
                    BigInteger x = (BigInteger)parameter.Result;
                    int bits = int.Parse(abiType == "uint" ? "256" : abiType.Replace("uint", ""));
                    int bytes = bits / 8;
                    result[fieldName] = x < new BigInteger(0) ? x + BigInteger.Parse("00" + string.Concat(Enumerable.Repeat("ff", bytes)), System.Globalization.NumberStyles.AllowHexSpecifier) + 1 : x;
                }
                else if (abiType.StartsWith("byte") && parameter.Result is byte[])
                {
                    result[fieldName] = "0x" + (((byte[])parameter.Result).Length == 0 ? "" : BitConverter.ToString((byte[])parameter.Result).Replace("-", "").ToLowerInvariant());
                }
                else if (abiType == "tuple" && sample is System.Dynamic.ExpandoObject && parameter.Result is List<Nethereum.ABI.FunctionEncoding.ParameterOutput>)
                {
                    result[fieldName] = decodeTuple(parameter.Result as List<Nethereum.ABI.FunctionEncoding.ParameterOutput>);
                }
                else if (abiType.StartsWith("byte") && parameter.Result is List<byte[]>)
                {
                    result[fieldName] = (parameter.Result as List<byte[]>).Select(d => "0x" + (d.Length == 0 ? "" : BitConverter.ToString(d).Replace("-", "").ToLowerInvariant()));
                }
                else
                {
                    result[fieldName] = parameter.Result;
                }
                idx = idx + 1;
            }

            return result;
        }

        public Dictionary<string, object> DecodeFunctionOutput<T>(string data, T memberType, IEnumerable<Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute> attributes, IEnumerable<Nethereum.ABI.Model.Parameter> outputParams, bool useAbiFieldName)
        {

            var dataProperties = attributes.OrderBy(x => x.Order).ToArray();
            var type = typeof(T);
            var typeProperties = type.GetTypeInfo().GetProperties();
            Dictionary<string, object> result = new Dictionary<string, object>();
            result = DecodeAttributes(data, result, typeProperties, memberType, dataProperties.ToArray(), outputParams, useAbiFieldName);
            return result;
        }
    }
    public class EthereumAccount
    {
        public string myAddress { get; set; }
        public Nethereum.Web3.Accounts.Account nethereumAccount { get; set; }
        public Nethereum.Web3.Accounts.Managed.ManagedAccount gethManagedAccount { get; set; }
    }
    public class EthereumCompiledClass
    {
        public string abi { get; set; }
        public string bin { get; set; }
        [Newtonsoft.Json.JsonProperty("bin-runtime")]
        public string bin_runtime { get; set; }
        public Dictionary<string, string> hashes { get; set; }
        public string metadata { get; set; }
        /* no longer string from 0.8+, not used so skip to have better backward compatiblity
        public string userdoc { get; set; }
         * */
    }

    public class EthereumCompiledClassPost7
    {
        public List<EthereumFunctionAbi> abi { get; set; }
        public string bin { get; set; }
        [Newtonsoft.Json.JsonProperty("bin-runtime")]
        public string bin_runtime { get; set; }
        public Dictionary<string, string> hashes { get; set; }
        public string metadata { get; set; }
        /* no longer string from 0.8+, not used so skip to have better backward compatiblity
        public string userdoc { get; set; }
         * */
    }

    public class EthereumFunctionAbi
    {
        // for event
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? anonymous { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? constant { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>[] inputs;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>[] outputs { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? payable { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string stateMutability { get; set; }
        public string type { get; set; }
    }
    public class EthereumCompilerMetaData
    {
        public class EthereumSolcCompilerInfo
        {
            public string version { get; set; }
        }
        public class EthereumSolcSetting
        {
            public class Optimizer
            {
                public bool enabled { get; set; }
                public int runs { get; set; }
            }
            public Dictionary<string, string> compilationTarget { get; set; }
            public Dictionary<string, string> libraries { get; set; }
            public Optimizer optimizer { get; set; }
        }
        public EthereumSolcCompilerInfo compiler { get; set; }
        public string language { get; set; }
        public EthereumSolcSetting settings { get; set; }
    }
    public class EthereumSolcOutoutVersion
    {
        public string version;
    }
    public class EthereumSolcOutput
    {
        public Dictionary<string, EthereumCompiledClass> contracts { get; set; }
        public Dictionary<string, EthereumCompiledClass> contractsNormailzed { get { return contracts.ToDictionary(kvp => kvp.Key.Replace("\\", "/"), kvp => kvp.Value); } }
        public string version;
    }
    public class EthereumSolcOutputPost7
    {
        public Dictionary<string, EthereumCompiledClassPost7> contracts { get; set; }
        public Dictionary<string, EthereumCompiledClassPost7> contractsNormailzed { get { return contracts.ToDictionary(kvp => kvp.Key.Replace("\\", "/"), kvp => kvp.Value); } }
        public string version;
        public EthereumSolcOutput ToEthereumSolcOutput()
        {
            var c = contracts.ToDictionary(kvp => kvp.Key,
                                   kvp => new EthereumCompiledClass()
                                   {
                                       abi = Newtonsoft.Json.JsonConvert.SerializeObject(kvp.Value.abi),
                                       bin_runtime = kvp.Value.bin_runtime,
                                       bin = kvp.Value.bin,
                                       hashes = kvp.Value.hashes,
                                       metadata = kvp.Value.metadata
                                   });
            var x = new EthereumSolcOutput() { contracts = c };
            return x;
        }
    }
    public class Ethereum
    {
        protected static Dictionary<string, Dictionary<BigInteger, DateTime>> ChainBlockTimestamp = new Dictionary<string, Dictionary<BigInteger, DateTime>>();
        protected static Dictionary<string, Dictionary<string, Transaction>> ChainTransactions = new Dictionary<string, Dictionary<string, Transaction>>();
        protected static Dictionary<string, Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt>> ChainTransactionReceipts = new Dictionary<string, Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt>>();

        private static byte[] salt = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", "")));
        private static Dictionary<string, string> cachedEthereumInfo = new Dictionary<string, string>();
        public int EthCallGas { get; set; }
        public EthereumAccount nethereumAccount { get; private set; }
        public BigInteger gasBoost(BigInteger gasNeeded, bool hardLimit = false)
        {
            var gasBoosted = gasNeeded
                            + (hardLimit
                                ? (0)
                                // > 4M gas, adding 1/10 is generally enough
                                : (gasNeeded > 4000000 ? gasNeeded / 10
                                // newer version of geth can be very wrong about estimate, double it
                                : gasNeeded / 1
                            ));
            return gasBoosted;
        }
        public readonly static BigInteger GWei = new BigInteger(1000000000);
        public readonly static string EmptyAddress = "0x0";
        public static BlockParameter MakeBlockParameter(string blockNumber)
        {
            return string.IsNullOrEmpty(blockNumber)
                                ? BlockParameter.CreateLatest()
                                : (blockNumber == "latest" ? Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest()
                                : (blockNumber == "earliest" ? Nethereum.RPC.Eth.DTOs.BlockParameter.CreateEarliest()
                                : (blockNumber == "pending" ? Nethereum.RPC.Eth.DTOs.BlockParameter.CreatePending()
                                : new Nethereum.RPC.Eth.DTOs.BlockParameter((uint.Parse(blockNumber)))
                                )));
        }
        private static string Encrypt(byte[] content, string inKey)
        {
            if (content == null || content.Length == 0) return null;

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                byte[] keyHash = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey));
                aes.Mode = CipherMode.CBC;
                aes.Key = keyHash;
                aes.IV = salt;
                return Convert.ToBase64String(aes.CreateEncryptor().TransformFinalBlock(content, 0, content.Length));
            };
        }
        private static byte[] Decrypt(string inStrBase64, string inKey)
        {
            if (string.IsNullOrEmpty(inStrBase64)) return null;

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                byte[] keyHash = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey));
                aes.Mode = CipherMode.CBC;
                aes.Key = keyHash;
                aes.IV = salt;
                byte[] encryptedContent = Convert.FromBase64String(inStrBase64);
                return aes.CreateDecryptor().TransformFinalBlock(encryptedContent, 0, encryptedContent.Length);
            };
        }

        public static string AbiJSONTranslate(string abiJSON)
        {
            Regex rxMutability = new Regex("(\"stateMutability\"\\w*:\\w*\"(pure|view|payable|nonpayable)\"\\w*)");
            bool constant = new Regex("(\"constant\"\\w*:\\w*(true|false)\\w*)").IsMatch(abiJSON);
            bool payable = new Regex("(\"payable\"\\w*:\\w*(true|false)\\w*)").IsMatch(abiJSON);

            // nethereum 3.x plus support newer solc output
            if (constant || payable || rxMutability.IsMatch(abiJSON)) return abiJSON;

            string abi = rxMutability.Replace(abiJSON, (m =>
            {
                string newPayable = payable ? "" : string.Format("\"payable\":{0}", m.Groups[2].Value == "nonpayable" ? "false" : "true");
                string newConstant = constant ? "" : string.Format("\"constant\":{0}", m.Groups[2].Value.Contains("pure") || m.Groups[2].Value.Contains("view") ? "true" : "false");
                return m.Value
                    + (string.IsNullOrEmpty(newConstant) ? "" : "," + newConstant)
                    + (string.IsNullOrEmpty(newPayable) ? "" : "," + newPayable);
            }));
            return abi;

        }
        public static string CreateEth2Validators(string depositEXE, string resultKeyFolder, string keyStorePassword, int validatorCount, string eth2Chain, string eth1WithdrawalAddress = null)
        {
            string mnemonic = "";
            bool waitingMnemonic = false;
            List<string> errMsg = new List<string>();
            Action<StreamWriter, Process> initHandler = (ws, proc) =>
            {
                //ws.WriteLine("");
                //ws.WriteLine("");
                //ws.WriteLine("");
                ws.WriteLine(keyStorePassword);
            };

            Action<string, Process, StreamWriter, string> depositPromptHandler = (v, proc, ws, src) =>
            {
                string x = v;
                if (v == "Repeat your keystore password for confirmation:")
                {
                }
                else if (
                    v == "This is your seed phrase. Write it down and store it safely, it is the ONLY way to retrieve your deposit."
                    ||
                    v == "This is your mnemonic (seed phrase). Write it down and store it safely. It is the ONLY way to retrieve your deposit."
                    )
                {
                    waitingMnemonic = true;
                }
                else if (waitingMnemonic && string.IsNullOrEmpty(mnemonic) && !string.IsNullOrEmpty(v))
                {
                    mnemonic = v;
                    waitingMnemonic = false;
                }
                else if (
                    !string.IsNullOrEmpty(mnemonic)
                    && !string.IsNullOrEmpty(v)
                    &&
                    (
                    v == "Please type your mnemonic (separated by spaces) to confirm you have written it down"
                    )
                    )
                {
                    ws.WriteLine(mnemonic);
                }
                else if (v == "Creating your keys.")
                {
                }
                else if (v == "Success!")
                {
                }
                else if (v == "Your keys can be found at: .\\validator_keys")
                {
                }

                else if (v == "Press any key when you have written down your mnemonic.")
                {

                }
                else if (v == "Press any key.")
                {
                }
                //ws.WriteLine("");
            };
            Action<string, Process, StreamWriter, string> stdErrHandler = (v, proc, ws, src) =>
            {
                string x = v;
                //ws.WriteLine("");
            };
            Action<object, EventArgs> exitHandler = (v, ws) =>
            {
                var x = v;
                errMsg.Append(x);
                //ws.WriteLine("");
            };

            string eth1 = !string.IsNullOrEmpty(eth1WithdrawalAddress) ? " --eth1_withdrawal_address " + eth1WithdrawalAddress : string.Empty;
            string lang = depositEXE.Contains("20") || true ? "--language english " : "";
            string nonInteractive = depositEXE.Contains("20") ? "--non_interactive " : "";
            string cmd_params = lang + "new-mnemonic"
                                + " --chain " + eth2Chain
                                + " --keystore_password " + keyStorePassword
                                + " --mnemonic_language english"
                                + " --num_validators " + validatorCount.ToString()
                                + " --folder . "
                                + eth1;
            var result = RO.Common3.Utils.WinProcEx(depositEXE, resultKeyFolder, null, cmd_params, depositPromptHandler, stdErrHandler, exitHandler, initHandler);
            Regex rx = new Regex("^mnemonic:");
            return rx.Replace(mnemonic, "");
        }
        public static string CreateEth2Validators(string depositEXE, string resultKeyFolder, string mnemonic, string keyStorePassword, int startFrom, int validatorCount, string eth2Chain, string eth1WithdrawalAddress = null)
        {
            List<string> errMsg = new List<string>();
            Action<StreamWriter, Process> initHandler = (ws, proc) =>
            {
                if (!depositEXE.Contains("20"))
                    //ws.WriteLine(mnemonic);
                    ws.WriteLine(mnemonic);
            };
            Action<string, Process, StreamWriter, string> depositPromptHandler = (v, proc, ws, src) =>
            {
                string x = v;
                if (v == null) return;
                else if (v == "Repeat your keystore password for confirmation:")
                {
                }
                else if (v == "Please repeat the index to confirm:")
                {
                    ws.WriteLine(startFrom);
                }
                else if (v.StartsWith("Please enter your mnemonic separated by spaces (\" \"). Note: you only need to enter the first 4 letters of each word if you'd prefer.:"))
                {
                    ws.WriteLine(mnemonic);
                }
                else if (v == "Creating your keys.")
                {
                }
                else if (v == "Success!")
                {
                }
                else if (v == "Your keys can be found at: .\\validator_keys")
                {
                }
                //ws.WriteLine("");
            };
            Action<string, Process, StreamWriter, string> stdErrHandler = (v, proc, ws, src) =>
            {
                string x = v;
                errMsg.Append(x);
                //ws.WriteLine("");
            };
            Action<object, EventArgs> exitHandler = (v, ws) =>
            {
                var x = v;
                //ws.WriteLine("");S
            };

            string eth1 = !string.IsNullOrEmpty(eth1WithdrawalAddress) ? " --eth1_withdrawal_address " + eth1WithdrawalAddress : string.Empty;
            string lang = depositEXE.Contains("20") || true ? "--language english " : "";
            string mnemonic_param = depositEXE.Contains("20") ? " --mnemonic \"" + mnemonic + "\" " : "";
            string cmd_params = lang + "existing-mnemonic"
                                + mnemonic_param
                                + " --chain " + eth2Chain
                                + " --keystore_password " + keyStorePassword
                                + " --validator_start_index " + startFrom.ToString()
                                + " --num_validators " + validatorCount.ToString()
                                + " --folder ."
                                + eth1;
            var result = RO.Common3.Utils.WinProcEx(depositEXE, resultKeyFolder, null, cmd_params, depositPromptHandler, stdErrHandler, exitHandler, initHandler);
            return mnemonic;
        }

        public async Task<string> GetDefaultAccount(Nethereum.Web3.Web3 web3)
        {
            try
            {
                var accounts = await web3.Eth.Accounts.SendRequestAsync();
                if (accounts.Length > 0)
                {
                    return accounts[0];
                }
                throw new EthereumRPCException("no default account for web3 provider");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is System.Net.Http.HttpRequestException)
                {
                    throw ex.InnerException;
                }
                else
                    throw new EthereumRPCException("no default account for web3 provider", ex);
            }
        }

        public Ethereum(int callGas = 8000000, string from = null)
        {
            nethereumAccount = new EthereumAccount();
            EthCallGas = callGas;
            if (from != null) SetGethManagedAccount(from);
        }

        private static bool IsIpcClient(Nethereum.Web3.Web3 web3)
        {
            return web3.Client.GetType() == typeof(Nethereum.JsonRpc.IpcClient.IpcClient);
        }
        public static async Task<bool> UnlockAccount(Nethereum.Web3.Web3 web3, string address, string password, int? durationInSeconds = 300)
        {
            bool isUnLocked = false;
            Exception err = null;
            for (int ii = 0; ii < 2; ii = ii + 1)
            {
                try
                {
                    isUnLocked = await IsAccountUnlocked(web3, address);
                    if (!isUnLocked)
                    {
                        return await web3.Personal.UnlockAccount.SendRequestAsync(address, password, new HexBigInteger(durationInSeconds.HasValue ? durationInSeconds.Value : 300));
                    }
                    else return true;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException is NullReferenceException && IsIpcClient(web3) && ii == 0)
                    {
                        err = ex;
                        // do nothing for first round to work around namepipe(ipc) bug of Nethereum
                    }
                    else throw;
                }
            }
            throw err; // whatever error

        }

        public static async Task<bool> LockAccount(Nethereum.Web3.Web3 web3, string address)
        {

            return await web3.Personal.LockAccount.SendRequestAsync(address);
        }

        public static async Task<string> NewAccount(Nethereum.Web3.Web3 web3, string password)
        {
            return await web3.Personal.NewAccount.SendRequestAsync(password);
        }


        public string GetExecFromAccount(string web3EndPoint)
        {
            var fromAddress = nethereumAccount.myAddress;
            if (string.IsNullOrEmpty(fromAddress))
            {
                try
                {
                    fromAddress = System.Threading.Tasks.Task.Run(async () =>
                    {
                        var web3 = GetWeb3Client(web3EndPoint);
                        var account = await GetDefaultAccount(web3);
                        return account;
                    }).Result;
                }
#pragma warning disable 168
                catch (Exception ex)
                {
                }
#pragma warning restore 168

            }
            return fromAddress;
        }
        public static async Task<bool> IsAccountUnlocked(Nethereum.Web3.Web3 web3, string address)
        {
            try
            {
                var signedData = await web3.Eth.Sign.SendRequestAsync(address, "0x" + BitConverter.ToString(System.Text.UTF8Encoding.UTF8.GetBytes("test")).Replace("-", "").ToLower());
                return true;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                if (x == "authentication needed: password or unlock") return false;
                throw;
            }
        }

        public static async Task<bool> IsMining(Nethereum.Web3.Web3 web3)
        {
            var isMining = await web3.Eth.Mining.IsMining.SendRequestAsync();
            return isMining;
        }
        public static async Task<bool> StartMiner(Nethereum.Web3.Web3 web3)
        {
            var gethWeb3 = new Nethereum.Geth.Web3Geth(web3.Client);
            return await gethWeb3.Miner.Start.SendRequestAsync();
        }

        public static async Task<bool> StopMiner(Nethereum.Web3.Web3 web3)
        {
            var gethWeb3 = new Nethereum.Geth.Web3Geth(web3.Client);
            return await gethWeb3.Miner.Stop.SendRequestAsync();
        }

        public static KeyValuePair<string, string> CreateEthereumWallet(string password, byte[] privateKey = null)
        {
            var service = new KeyStoreService();

            //Creating a new key 
            var ecKey = privateKey == null ? EthECKey.GenerateKey() : new EthECKey(privateKey.ToHex(true));
            //We can use EthECKey to generate a new ECKey pair, this is using SecureRandom
            privateKey = privateKey ?? ecKey.GetPrivateKeyAsBytes();
            var genAddress = ecKey.GetPublicAddress();

            //instead of the default service we can use either
            //Scrypt
            var scryptService = new KeyStoreScryptService();
            var scryptResult = scryptService.EncryptAndGenerateKeyStoreAsJson(password, privateKey, genAddress);
            //or pkbdf2
            //var pbkdf2Service = new KeyStorePbkdf2Service();
            //var pkbdf2Result = pbkdf2Service.EncryptAndGenerateKeyStoreAsJson(password, privateKey, genAddress);

            // filename convention used by geth for key
            var fileName = service.GenerateUTCFileName(genAddress);

            // return intended file name(for geth) and JSON keystore content 
            return new KeyValuePair<string, string>(fileName, scryptResult);
        }

        public string GetAddressFromKeyStore(string keystoreJson)
        {
            var service = new KeyStoreService();

            return service.GetAddressFromKeyStore(keystoreJson);
        }

        public static Tuple<string[], string, string> CreateHDWallet(string seedPassword = null)
        {
            // this should return a 12/24 word Mnemonic which is the 'seed' of HD wallet, the first account private key and first address
            var mnemonic = new NBitcoin.Mnemonic(NBitcoin.Wordlist.English, NBitcoin.WordCount.TwentyFour);
            var hdWallet = new Nethereum.HdWallet.Wallet(mnemonic.DeriveSeed(seedPassword));
            return new Tuple<string[], string, string>(mnemonic.Words, hdWallet.GetAccount(0).PrivateKey, hdWallet.GetAddresses(1)[0]);
        }

        public static Tuple<string, string> CreateKeystoreWallet(string password, string privateKey = null)
        {
            var key = privateKey == null ? Nethereum.Signer.EthECKey.GenerateKey() : new Nethereum.Signer.EthECKey(privateKey);
            var address = key.GetPublicAddress();
            var service = new Nethereum.KeyStore.KeyStoreService();

            string keyStoreJson = service.EncryptAndGenerateDefaultKeyStoreAsJson(
                        password, key.GetPrivateKeyAsBytes(), address);
            return new Tuple<string, string>(keyStoreJson, address);
        }

        public static Tuple<string, string> CreateKeystoreWallet(string password, string[] mnemonic)
        {
            var hdWallet = new Nethereum.HdWallet.Wallet(string.Join(" ", mnemonic), null);
            var account = hdWallet.GetAccount(0);
            var key = account.PrivateKey;
            var address = account.Address;
            var service = new Nethereum.KeyStore.KeyStoreService();

            string keyStoreJson = service.EncryptAndGenerateDefaultKeyStoreAsJson(
                        password, key.HexToByteArray(), address);
            return new Tuple<string, string>(keyStoreJson, address);
        }

        public static Tuple<string, string> CreateGethWallet(Nethereum.Web3.Web3 web3, string password = null)
        {
            return Task.Run(async () =>
            {
                password = password ?? Nethereum.Signer.EthECKey.GenerateKey().GetPrivateKey();
                return new Tuple<string, string>(password, await NewAccount(web3, password));
            }).Result;
        }
        public string GetERC20TokenABI()
        {
            return @"[
    {
        'constant': true,
        'inputs': [],
        'name': 'name',
        'outputs': [
            {
                'name': '',
                'type': 'string'
            }
        ],
        'payable': false,
        'stateMutability': 'view',
        'type': 'function'
    },
    {
        'constant': false,
        'inputs': [
            {
                'name': '_spender',
                'type': 'address'
            },
            {
                'name': '_value',
                'type': 'uint256'
            }
        ],
        'name': 'approve',
        'outputs': [
            {
                'name': '',
                'type': 'bool'
            }
        ],
        'payable': false,
        'stateMutability': 'nonpayable',
        'type': 'function'
    },
    {
        'constant': true,
        'inputs': [],
        'name': 'totalSupply',
        'outputs': [
            {
                'name': '',
                'type': 'uint256'
            }
        ],
        'payable': false,
        'stateMutability': 'view',
        'type': 'function'
    },
    {
        'constant': false,
        'inputs': [
            {
                'name': '_from',
                'type': 'address'
            },
            {
                'name': '_to',
                'type': 'address'
            },
            {
                'name': '_value',
                'type': 'uint256'
            }
        ],
        'name': 'transferFrom',
        'outputs': [
            {
                'name': '',
                'type': 'bool'
            }
        ],
        'payable': false,
        'stateMutability': 'nonpayable',
        'type': 'function'
    },
    {
        'constant': true,
        'inputs': [],
        'name': 'decimals',
        'outputs': [
            {
                'name': '',
                'type': 'uint8'
            }
        ],
        'payable': false,
        'stateMutability': 'view',
        'type': 'function'
    },
    {
        'constant': true,
        'inputs': [
            {
                'name': '_owner',
                'type': 'address'
            }
        ],
        'name': 'balanceOf',
        'outputs': [
            {
                'name': 'balance',
                'type': 'uint256'
            }
        ],
        'payable': false,
        'stateMutability': 'view',
        'type': 'function'
    },
    {
        'constant': true,
        'inputs': [],
        'name': 'symbol',
        'outputs': [
            {
                'name': '',
                'type': 'string'
            }
        ],
        'payable': false,
        'stateMutability': 'view',
        'type': 'function'
    },
    {
        'constant': false,
        'inputs': [
            {
                'name': '_to',
                'type': 'address'
            },
            {
                'name': '_value',
                'type': 'uint256'
            }
        ],
        'name': 'transfer',
        'outputs': [
            {
                'name': '',
                'type': 'bool'
            }
        ],
        'payable': false,
        'stateMutability': 'nonpayable',
        'type': 'function'
    },
    {
        'constant': true,
        'inputs': [
            {
                'name': '_owner',
                'type': 'address'
            },
            {
                'name': '_spender',
                'type': 'address'
            }
        ],
        'name': 'allowance',
        'outputs': [
            {
                'name': '',
                'type': 'uint256'
            }
        ],
        'payable': false,
        'stateMutability': 'view',
        'type': 'function'
    },
    {
        'payable': true,
        'stateMutability': 'payable',
        'type': 'fallback'
    },
    {
        'anonymous': false,
        'inputs': [
            {
                'indexed': true,
                'name': 'owner',
                'type': 'address'
            },
            {
                'indexed': true,
                'name': 'spender',
                'type': 'address'
            },
            {
                'indexed': false,
                'name': 'value',
                'type': 'uint256'
            }
        ],
        'name': 'Approval',
        'type': 'event'
    },
    {
        'anonymous': false,
        'inputs': [
            {
                'indexed': true,
                'name': 'from',
                'type': 'address'
            },
            {
                'indexed': true,
                'name': 'to',
                'type': 'address'
            },
            {
                'indexed': false,
                'name': 'value',
                'type': 'uint256'
            }
        ],
        'name': 'Transfer',
        'type': 'event'
    }
]".Replace("\r", "").Replace("\n", "");

        }
        public EthECKey UnlockEthereumWallet(string password, string keyStoreJson)
        {
            var service = new KeyStoreService();
            //decrypt the private key
            byte[] key = service.DecryptKeyStoreFromJson(password, keyStoreJson);
            var eckey = new EthECKey(key.ToHex(true));
            return eckey;
        }


        public EthereumAccount SetGethManagedAccount(string address, string password = null, bool cache = false)
        {
            string hashKey = BitConverter.ToString((new HMACSHA256(salt)).ComputeHash(Encoding.UTF8.GetBytes(address.ToLower()))).Replace("-", "");
            if (cachedEthereumInfo.ContainsKey(hashKey))
            {

                byte[] savedPassword = Decrypt(cachedEthereumInfo[hashKey], System.BitConverter.ToString(salt));
                nethereumAccount = new EthereumAccount()
                {
                    myAddress = address,
                    nethereumAccount = null,
                    gethManagedAccount = new Nethereum.Web3.Accounts.Managed.ManagedAccount(address, password ?? Encoding.UTF8.GetString(savedPassword))
                };
            }
            else
            {
                nethereumAccount = new EthereumAccount()
                {
                    myAddress = address,
                    nethereumAccount = null,
                    gethManagedAccount = new Nethereum.Web3.Accounts.Managed.ManagedAccount(address, password ?? "")
                };
            }

            if (cache && password != null) cachedEthereumInfo[hashKey] = Encrypt(Encoding.UTF8.GetBytes(password), System.BitConverter.ToString(salt));

            return nethereumAccount;

        }
        public EthereumAccount LoadFromKeyStoreJson(string gethKeyStoreJson, string password)
        {
            var keyStoreService = new Nethereum.KeyStore.KeyStoreService();
            string address = keyStoreService.GetAddressFromKeyStore(gethKeyStoreJson).ToLower();
            string hashKey = BitConverter.ToString((new HMACSHA256(salt)).ComputeHash(Encoding.UTF8.GetBytes(gethKeyStoreJson.ToLower() + (password ?? "")))).Replace("-", "");
            if (cachedEthereumInfo.ContainsKey(hashKey))
            {

                byte[] privateKey = Decrypt(cachedEthereumInfo[hashKey], password);
                nethereumAccount = new EthereumAccount()
                {
                    myAddress = address,
                    nethereumAccount = new Account(privateKey),
                    gethManagedAccount = null
                };
            }
            else
            {
                byte[] privateKey = keyStoreService.DecryptKeyStoreFromJson(password, gethKeyStoreJson);
                cachedEthereumInfo[hashKey] = Encrypt(privateKey, password);
                nethereumAccount = new EthereumAccount()
                {
                    myAddress = address,
                    nethereumAccount = new Account(privateKey),
                    gethManagedAccount = null
                };
            }
            return nethereumAccount;
        }

        public static string SignMessage(string mnemonic, string message, string walletPassword = "")
        {
            var wallet = new Wallet(mnemonic, walletPassword);
            var account0 = wallet.GetAccount(0);
            var address0 = account0.Address;
            var signer = new EthereumMessageSigner();
            //var signature = signer.HashAndSign(System.Text.UTF8Encoding.UTF8.GetBytes(message), new EthECKey(account0.PrivateKey));
            //var recoveredAddress = signer.HashAndEcRecover(message, signature);
            var signature = signer.EncodeUTF8AndSign(message, new EthECKey(account0.PrivateKey));
            var recoveredAddress = signer.EncodeUTF8AndEcRecover(message, signature);
            return signature;
        }

        public string SignTransaction(string to, BigInteger ethInWei, BigInteger nounce, BigInteger gasPriceInWei, BigInteger gasLimit, string data, int? chainId = null)
        {

            var signer = new Nethereum.Signer.LegacyTransactionSigner();

            if (nethereumAccount == null || nethereumAccount.nethereumAccount == null) throw new Exception("No private key is available");

            if (chainId != null && chainId > 0)
                return signer.SignTransaction(nethereumAccount.nethereumAccount.PrivateKey, new BigInteger(chainId.Value), to, ethInWei, nounce, gasPriceInWei, gasLimit, data);
            else
                return signer.SignTransaction(nethereumAccount.nethereumAccount.PrivateKey, to, ethInWei, nounce, gasPriceInWei, gasLimit, data);
        }

        public string SignTransaction1559(string to, BigInteger ethInWei, BigInteger nonce, BigInteger maxPriorityFeePerGasInWei, BigInteger maxFeePerGasInWei, BigInteger gasLimit, string data, int chainId, List<AccessListItem> accessList = null)
        {
            if (nethereumAccount == null || nethereumAccount.nethereumAccount == null) throw new Exception("No private key is available");
            Nethereum.Signer.EthECKey ethECKey = new EthECKey(nethereumAccount.nethereumAccount.PrivateKey);
            var transaction = new Nethereum.Signer.Transaction1559(chainId, nonce, maxPriorityFeePerGasInWei, maxFeePerGasInWei, gasLimit, to, ethInWei, data, accessList);
            transaction.Sign(ethECKey);
            return transaction.GetRLPEncoded().ToHex();
        }

        public async Task<string> SignTransactionAsync(Nethereum.Web3.Web3 web3, string to, BigInteger ethInWei, BigInteger gasPriceInWei, BigInteger gasLimit, string data, int chainId, BigInteger? maxPriorityFeePerGas = null, BigInteger? nonce = null)
        {
            var myAddress = string.IsNullOrEmpty(nethereumAccount.myAddress) ? await GetDefaultAccount(web3) : nethereumAccount.myAddress;
            int round = 0;
            try
            {
                if (nethereumAccount.gethManagedAccount != null && !string.IsNullOrEmpty(nethereumAccount.gethManagedAccount.Address))
                {
                    var gethAccount = this.nethereumAccount.gethManagedAccount;
                    var txInput = maxPriorityFeePerGas != null
                                    ? new Nethereum.RPC.Eth.DTOs.TransactionInput(
                                            new HexBigInteger((int)Nethereum.Signer.TransactionType.EIP1559)
                                            , data, to, myAddress
                                            , new HexBigInteger(gasLimit)
                                            , new HexBigInteger(ethInWei)
                                            , new HexBigInteger(gasPriceInWei)
                                            , new HexBigInteger(maxPriorityFeePerGas.Value))
                                    : new Nethereum.RPC.Eth.DTOs.TransactionInput(
                                            data, to, gethAccount.Address
                                            , new Nethereum.Hex.HexTypes.HexBigInteger(gasLimit)
                                            , new Nethereum.Hex.HexTypes.HexBigInteger(gasPriceInWei)
                                            , new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei));
                    ;
                    if (nonce != null) txInput.Nonce = new HexBigInteger(nonce.Value);
                    return await gethAccount.TransactionManager.SignTransactionAsync(txInput);
                }
                else if (nethereumAccount.nethereumAccount != null)
                {
                    Func<Task<string>> task = async () =>
                    {
                        var nonceService = new Nethereum.RPC.NonceServices.InMemoryNonceService(myAddress, web3.Client);
                        HexBigInteger newNonce = nonce != null ? new HexBigInteger(nonce.Value) : await nonceService.GetNextNonceAsync();
                        string signedDataHexString = maxPriorityFeePerGas == null
                                                    ? SignTransaction(to, ethInWei, newNonce, gasPriceInWei, gasLimit, data, chainId)
                                                    : SignTransaction1559(to, ethInWei, newNonce, gasPriceInWei, maxPriorityFeePerGas.Value, gasLimit, data, chainId)
                                                    ;
                        return signedDataHexString;
                    };

                    return await task();
                }
                else
                {
                    if (!string.IsNullOrEmpty(myAddress))
                    {
                        var nonceService = new Nethereum.RPC.NonceServices.InMemoryNonceService(myAddress, web3.Client);
                        HexBigInteger newNonce = nonce != null ? new HexBigInteger(nonce.Value) : await nonceService.GetNextNonceAsync();
                        if (maxPriorityFeePerGas == null)
                        {
                            var txInput = new Nethereum.RPC.Eth.DTOs.TransactionInput(data, to, myAddress
                                , new Nethereum.Hex.HexTypes.HexBigInteger(gasLimit), new Nethereum.Hex.HexTypes.HexBigInteger(gasPriceInWei), new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei));
                            txInput.Nonce = newNonce;
                            var signedTransaction = await new RO.Common3.Ethereum.RPC.EthSignTransaction(web3.Client).SendRequestAsync(txInput);
                            return signedTransaction.raw;
                        }
                        else
                        {
                            var txInput = new Nethereum.RPC.Eth.DTOs.TransactionInput(
                                                new HexBigInteger(Nethereum.Signer.TransactionType.EIP1559.AsByte())
                                                , data, to, myAddress
                                                , new HexBigInteger(gasLimit)
                                                , new HexBigInteger(ethInWei)
                                                , new HexBigInteger(gasPriceInWei)
                                                , new HexBigInteger(maxPriorityFeePerGas.Value));
                            txInput.Nonce = newNonce;
                            var signedTransaction = await new RO.Common3.Ethereum.RPC.EthSignTransaction(web3.Client).SendRequestAsync(txInput);
                            return signedTransaction.raw;
                        }
                    }
                    else
                    {
                        throw new Exception("No account/private key is available for executing transaction");
                    }
                }
            }
            catch (Exception ex)
            {
                var x = ex.InnerException;
                if ((x ?? ex).Message == "authentication needed: password or unlock")
                {
                    throw new EthereumRPCException((x ?? ex).Message + " " + myAddress);
                }
                else
                    throw new EthereumRPCException(x != null ? x.Message : ex.Message + " round(" + round.ToString() + ")" + (nonce != null ? "nonce " + nonce.Value.ToString() : ""), ex);
            }
        }

        public async Task<string> SendTransactionAsync(Nethereum.Web3.Web3 web3, string to, BigInteger ethInWei, BigInteger gasPriceInWei, BigInteger gasLimit, string data, int chainId, BigInteger? maxPriorityFeePerGas = null, BigInteger? nonce = null)
        {
            var myAddress = string.IsNullOrEmpty(nethereumAccount.myAddress) ? await GetDefaultAccount(web3) : nethereumAccount.myAddress;
            int round = 0;
            try
            {
                if (nethereumAccount.gethManagedAccount != null && !string.IsNullOrEmpty(nethereumAccount.gethManagedAccount.Address))
                {
                    var gethAccount = this.nethereumAccount.gethManagedAccount;
                    var txInput = maxPriorityFeePerGas != null
                                    ? new Nethereum.RPC.Eth.DTOs.TransactionInput(
                                            new HexBigInteger((int)Nethereum.Signer.TransactionType.EIP1559)
                                            , data, to, myAddress
                                            , new HexBigInteger(gasLimit)
                                            , new HexBigInteger(ethInWei)
                                            , new HexBigInteger(gasPriceInWei)
                                            , new HexBigInteger(maxPriorityFeePerGas.Value))
                                    : new Nethereum.RPC.Eth.DTOs.TransactionInput(
                                            data, to, gethAccount.Address
                                            , new Nethereum.Hex.HexTypes.HexBigInteger(gasLimit)
                                            , new Nethereum.Hex.HexTypes.HexBigInteger(gasPriceInWei)
                                            , new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei));
                    ;
                    gethAccount.TransactionManager.Client = web3.Client;
                    if (nonce != null) txInput.Nonce = new HexBigInteger(nonce.Value); 
                    if (!string.IsNullOrEmpty(gethAccount.Password))
                    {
                        return await gethAccount.TransactionManager.SendTransactionAsync(txInput);
                    }
                    else
                    {
                        return await web3.Eth.Transactions.SendTransaction.SendRequestAsync(txInput);
                    }
                }
                else if (nethereumAccount.nethereumAccount != null)
                {
                    Func<Task<string>> task = async () =>
                    {
                        var nonceService = new Nethereum.RPC.NonceServices.InMemoryNonceService(myAddress, web3.Client);
                        nonce = nonce ?? await nonceService.GetNextNonceAsync();
                        var newNounce = BigInteger.Add(nonce.Value, round);
                        string signedDataHexString = maxPriorityFeePerGas == null
                                                    ? SignTransaction(to, ethInWei, newNounce, gasPriceInWei, gasLimit, data, chainId)
                                                    : SignTransaction1559(to, ethInWei, newNounce, gasPriceInWei, maxPriorityFeePerGas.Value, gasLimit, data, chainId)
                                                    ;
                        return await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync((
                            signedDataHexString.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) ? "" : "0x") + signedDataHexString);
                    };

                    Exception error = null;
                    for (round = 0; round < 10; round++)
                    {
                        try
                        {
                            return await task();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "replacement transaction underpriced" || ex.Message == "nonce too low")
                            {
                                error = ex;
                                //System.Threading.Thread.Sleep((round + 1) * 500);
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    throw error;

                }
                else
                {
                    if (!string.IsNullOrEmpty(myAddress))
                    {
                        if (maxPriorityFeePerGas == null)
                        {
                            var txInput = new Nethereum.RPC.Eth.DTOs.TransactionInput(data, to, myAddress
                                , new Nethereum.Hex.HexTypes.HexBigInteger(gasLimit), new Nethereum.Hex.HexTypes.HexBigInteger(gasPriceInWei), new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei));
                            if (nonce != null) txInput.Nonce = new HexBigInteger(nonce.Value);
                            return await web3.TransactionManager.SendTransactionAsync(txInput);
                        }
                        else
                        {
                            var txInput = new Nethereum.RPC.Eth.DTOs.TransactionInput(
                                                new HexBigInteger(Nethereum.Signer.TransactionType.EIP1559.AsByte())
                                                , data, to, myAddress
                                                , new HexBigInteger(gasLimit)
                                                , new HexBigInteger(ethInWei)
                                                , new HexBigInteger(gasPriceInWei)
                                                , new HexBigInteger(maxPriorityFeePerGas.Value));
                            if (nonce != null) txInput.Nonce = new HexBigInteger(nonce.Value);
                            var defaultGasPrice = web3.TransactionManager.DefaultGasPrice;
                            web3.TransactionManager.DefaultGasPrice = -1;
                            var task = web3.TransactionManager.SendTransactionAsync(txInput);
                            web3.TransactionManager.DefaultGasPrice = defaultGasPrice;
                            return await task;
                        }
                    }
                    else
                    {
                        throw new Exception("No account/private key is available for executing transaction");
                    }
                }
            }
            catch (Exception ex)
            {
                var x = ex.InnerException;
                if ((x ?? ex).Message == "authentication needed: password or unlock")
                {
                    throw new EthereumRPCException((x ?? ex).Message + " " + myAddress);
                }
                else
                    throw new EthereumRPCException(x != null ? x.Message : ex.Message + " round(" + round.ToString() + ")" + (nonce != null ? "nounce " + nonce.Value.ToString() : ""), ex);
            }
        }

        public string CreateDeployContractData(Nethereum.Web3.Web3 web3, string byteCode, string abiJson, object[] constructorParams)
        {
            var contractBuilder = new Nethereum.Contracts.DeployContractTransactionBuilder();
            var constructor = new Nethereum.ABI.FunctionEncoding.ConstructorCallEncoder();
            var contractCreationData = constructorParams == null || constructorParams.Length == 0
                                        ? constructor.EncodeRequest(byteCode, "")
                                        : contractBuilder.GetData(AbiJSONTranslate(abiJson), byteCode, constructorParams);
            return contractCreationData;
        }

        public async Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, string byteCode, string abiJson, BigInteger ethInWei, BigInteger gasPriceInWei, BigInteger gasLimit, int chainId, BigInteger? maxPriorityFeePerGas, BigInteger? nonce, params object[] constructorParams)
        {
            var myAddress = string.IsNullOrEmpty(nethereumAccount.myAddress) ? await GetDefaultAccount(web3) : nethereumAccount.myAddress;
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            var constructorAbi = contract.ContractABI.Constructor;
            var contractBuilder = new Nethereum.Contracts.DeployContractTransactionBuilder();
            var constructor = new Nethereum.ABI.FunctionEncoding.ConstructorCallEncoder();
            var revisedParams = constructorAbi != null ? NormalizedFunctParam(constructorAbi.InputParameters, constructorParams) : constructorParams;
            var contractCreationData = revisedParams == null || revisedParams.Length == 0
                                        ? constructor.EncodeRequest(byteCode, "")
                                        : contractBuilder.GetData(byteCode, AbiJSONTranslate(abiJson), TranslateConstructorInput(abiJson, revisedParams));
            var callInput = contractBuilder.BuildTransaction(AbiJSONTranslate(abiJson), byteCode, myAddress, new Nethereum.Hex.HexTypes.HexBigInteger(10000000), new Nethereum.Hex.HexTypes.HexBigInteger(gasPriceInWei), new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei), TranslateConstructorInput(abiJson, revisedParams));
            BigInteger gasNeeded = new BigInteger(-1);
            try
            {
                gasNeeded = this.EstimateGasAsync(web3, callInput).Result
                    + (gasLimit == 0 ? 0 : 1000); // slightly increment the estimate for better failed transaction diagnostic 
                gasNeeded = gasNeeded < gasLimit ? gasLimit : gasNeeded;
                return await SendTransactionAsync(web3, null, ethInWei, gasPriceInWei, gasNeeded, contractCreationData, chainId, maxPriorityFeePerGas, nonce);
            }
            catch (Exception ex)
            {
                var x = ex.InnerException;
                if (x != null && x.Message == "authentication needed: password or unlock")
                {
                    throw new EthereumRPCException(x.Message + " " + myAddress);
                }
                else if (x != null && x.Message.Contains("exceeds block gas limit") && gasNeeded != -1)
                {
                    throw new EthereumRPCException(x.Message + " " + gasNeeded.ToString());
                }
                else throw;
            }
        }
        public string DeployContract(string web3Endpoint, string byteCode, string abiJson, BigInteger ethInWei, BigInteger gasPriceInWei, BigInteger gasLimit, int chainId, BigInteger? maxPriorityFeePerGas, HexBigInteger nonce, params object[] constructorParams)
        {
            try
            {
                var txResult = System.Threading.Tasks.Task.Run(async () =>
                {
                    var web3 = Ethereum.GetWeb3Client(web3Endpoint);

                    //"0x" + byteCode

                    var hash = await DeployContractAsync(web3, byteCode, abiJson, ethInWei, gasPriceInWei, gasLimit, chainId, maxPriorityFeePerGas, nonce, constructorParams);
                    if (hash != null)
                    {
                        var tx = await GetTransactionAsync(hash, web3);
                        if (tx != null)
                        {
                            var txInfo = new
                            {
                                TransactionHash = tx.TransactionHash,
                                ContractAddress = (string)null,
                                GasPrice = tx.GasPrice.Value,
                                GasLimit = tx.Gas.Value,
                                Nounce = tx.Nonce.Value,
                            };
                            var txInfoJson = Newtonsoft.Json.JsonConvert.SerializeObject(txInfo);
                            return txInfoJson;
                        }

                    }
                    return hash;
                }).Result;
                return txResult;
            }
            catch (Exception ex)
            {
                var ex1 = (ex.InnerException ?? ex);
                throw ex1;
            }
        }
        public async Task<string> SendRawTransactionAsync(Nethereum.Web3.Web3 web3, string signedDataHexString)
        {
            return await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(
                (signedDataHexString.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase) ? "" : "0x") + signedDataHexString);
        }

        public async Task<BigInteger> GetNextNonceAsync(string address, Nethereum.Web3.Web3 web3)
        {
            var nonceService = new Nethereum.RPC.NonceServices.InMemoryNonceService(address, web3.Client);
            var nounce = await nonceService.GetNextNonceAsync();
            return nounce.Value;
        }

        public async Task<Nethereum.RPC.Eth.DTOs.Transaction> GetTransactionAsync(string txHash, Nethereum.Web3.Web3 web3)
        {
            return await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(txHash);
        }

        public async Task<Nethereum.RPC.Eth.DTOs.TransactionReceipt> GetTransactionReceiptAsync(string txHash, Nethereum.Web3.Web3 web3)
        {
            return await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txHash);
        }
        public async Task<string> GetTransactionInfoAsync(string txHash, Nethereum.Web3.Web3 web3, string contractAbi = null, bool includeContractByteCode = false)
        {
            var txReceiptRequest = GetTransactionReceiptAsync(txHash, web3);
            var txRequest = GetTransactionAsync(txHash, web3);
            var txReceipt = await txReceiptRequest;
            var tx = await txRequest;

            if (tx == null && txReceipt == null) return null;

            DateTime? timestamp = txReceipt != null
                            ? await GetBlockTimestampAsync((ulong)txReceipt.BlockNumber.Value, web3)
                            : (DateTime?)null;

            var txInfo = new
            {
                TransactionHash = tx.TransactionHash,
                To = !string.IsNullOrEmpty(tx.To) ? Nethereum.Util.AddressExtensions.ConvertToEthereumChecksumAddress(tx.To) : tx.To,
                From = Nethereum.Util.AddressExtensions.ConvertToEthereumChecksumAddress(tx.From),
                Value = BigInteger.Parse(tx.Value.ToString()),
                GasPrice = tx.GasPrice.Value,
                MaxFeePerGas = tx.MaxFeePerGas != null ? BigInteger.Parse(tx.MaxFeePerGas.ToString()) : (BigInteger?)null,
                MaxPriorityFeePerGas = tx.MaxFeePerGas != null ? BigInteger.Parse(tx.MaxPriorityFeePerGas.ToString()) : (BigInteger?)null,
                TxType = tx.Type,
                GasLimit = tx.Gas.Value,
                Nounce = tx.Nonce.Value,
                GasUsed = txReceipt != null ? txReceipt.GasUsed.Value : (BigInteger?)null,
                BlockNumber = txReceipt != null ? txReceipt.BlockNumber.Value : (BigInteger?)null,
                Timestamp = timestamp,
                Status = string.Format("{0}", txReceipt != null ? txReceipt.Status.Value : (BigInteger?)null),
                ContractAddress = txReceipt != null && txReceipt.Status.Value > 0 && !string.IsNullOrEmpty(txReceipt.ContractAddress) ? Nethereum.Util.AddressExtensions.ConvertToEthereumChecksumAddress(txReceipt.ContractAddress) : null,
                Input = txReceipt != null && (!string.IsNullOrEmpty(txReceipt.ContractAddress) || includeContractByteCode) ? tx.Input : "<skipped>",
                EffectiveGasPrice = txReceipt.EffectiveGasPrice != null ? BigInteger.Parse(txReceipt.EffectiveGasPrice.ToString()) : (BigInteger?)null,
                Logs = DecodeEventLog(contractAbi,
                    txReceipt.Logs.Select(evt => evt.ToObject<Nethereum.RPC.Eth.DTOs.FilterLog>()).ToArray()
                    , (sha33Signature, name) =>
                    {
                        return GetEventInputDTO(contractAbi, name);
                    }, true)
            };

            var txInfoJson = Newtonsoft.Json.JsonConvert.SerializeObject(txInfo);
            return txInfoJson;
        }

        public string GetTransactionInfo(string txHash, Nethereum.Web3.Web3 web3, string contractAbi = null, bool includeContractByteCode = false)
        {
            var result = System.Threading.Tasks.Task.Run(async () =>
            {
                var ret = await GetTransactionInfoAsync(txHash, web3, contractAbi, includeContractByteCode);
                return ret;
            }).Result;
            return result;
        }

        public DateTime GetBlockTimestamp(ulong blockNumber, Nethereum.Web3.Web3 web3)
        {
            return GetBlockTimestampAsync(blockNumber, web3).Result;
        }

        public async Task<DateTime> GetBlockTimestampAsync(ulong blockNumber, Nethereum.Web3.Web3 web3)
        {
            var ret = await GetBlockWithTransactionsHashesAsync(web3, new BlockParameter(blockNumber));
            return UnixTimeToDateTime(ret.Timestamp.ToUlong());
        }

        public async Task<BigInteger> EstimateGasAsync(Nethereum.Web3.Web3 web3, Nethereum.RPC.Eth.DTOs.CallInput callInput)
        {
            //return new BigInteger(5000000);
            var gasNeeded = await web3.TransactionManager.EstimateGasAsync(callInput);
            return gasNeeded.Value;
        }
        public async Task<BigInteger> EstimateGasAsync(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string functionName, BigInteger ethInWei, params object[] functionInput)
        {
            try
            {
                var myAddress = string.IsNullOrEmpty(nethereumAccount.myAddress) ? await GetDefaultAccount(web3) : nethereumAccount.myAddress;
                var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
                var caller = contract.GetFunction(functionName);
                var txInput = caller.CreateCallInput(myAddress
                    , new Nethereum.Hex.HexTypes.HexBigInteger(EthCallGas)
                    , new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei), TranslateCallInput(AbiJSONTranslate(abiJson), functionName, functionInput));
                return await this.EstimateGasAsync(web3, txInput);
            }
            catch (Exception ex)
            {
                var x = ex.InnerException;
                throw;
            }
        }

        public async Task<string> CallAsync(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string functionName, BigInteger ethInWei, object[] functionInput, BlockParameter block, bool decodeToJSON = false, bool useAbiFieldName = false)
        {
            var myAddress = nethereumAccount.myAddress;
            if (string.IsNullOrEmpty(myAddress))
            {
                try
                {
                    myAddress = await GetDefaultAccount(web3);
                }
                catch
                {
                    // we don't need from for function call generally BUT it can't be empty string !!!
                    myAddress = null;
                }
            }
            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
            var caller = contract.GetFunction(functionName);
            var callInput = caller.CreateCallInput(myAddress
                , new Nethereum.Hex.HexTypes.HexBigInteger(EthCallGas)
                , new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei), TranslateCallInput(AbiJSONTranslate(abiJson), functionName, functionInput));

            var result = await web3.Eth.Transactions.Call.SendRequestAsync(callInput, block);
            if (decodeToJSON)
            {
                dynamic output = GetFunctionOutputDTO(AbiJSONTranslate(abiJson), functionName);
                var function = Ethereum.GetFunctionBuilder(AbiJSONTranslate(abiJson), functionName);
                var revisedFunctionInput = NormalizedFunctParam(function.FunctionABI.InputParameters, functionInput);
                var retVal = Ethereum.DecodeFunctionOutput(AbiJSONTranslate(abiJson), result, functionName, revisedFunctionInput, () => output, useAbiFieldName);
                var retValJson = Newtonsoft.Json.JsonConvert.SerializeObject(retVal);
                return retValJson;

            }
            else
                return result;
        }
        public Task<string> CallAsync(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string functionName, BigInteger ethInWei, object[] functionInput, bool decodeToJSON = false, bool useAbiFieldName = false)
        {
            return CallAsync(abiJson, contractAddress, web3, functionName, ethInWei, functionInput, BlockParameter.CreateLatest(), decodeToJSON, useAbiFieldName);
        }
        public async Task<TResult> CallAsync<TResult>(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string functionName, BigInteger ethInWei, BlockParameter block, object[] functionInput)
        {
            var myAddress = nethereumAccount.myAddress;
            if (string.IsNullOrEmpty(myAddress))
            {
                try
                {
                    myAddress = await GetDefaultAccount(web3);
                }
                catch
                {
                    // we don't need from for function call generally BUT it can't be empty string !!!
                    myAddress = null;
                }
            }

            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
            var caller = contract.GetFunction(functionName);
            var callInput = caller.CreateCallInput(myAddress
                , new Nethereum.Hex.HexTypes.HexBigInteger(EthCallGas)
                , new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei), TranslateCallInput(AbiJSONTranslate(abiJson), functionName, functionInput));
            var x = await web3.Eth.Transactions.Call.SendRequestAsync(callInput);
            var result = await caller.CallAsync<TResult>(myAddress
                , new Nethereum.Hex.HexTypes.HexBigInteger(5000000)
                , new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei), block, functionInput);
            return result;
        }
        public async Task<List<Nethereum.Contracts.EventLog<TResult>>> GetEventAsync<TResult>(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string eventName, Nethereum.Hex.HexTypes.HexBigInteger filterId) where TResult : new()
        {
            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
            var contractEvent = contract.GetEvent(eventName);
            var eventLog = await contractEvent.GetFilterChangesAsync<TResult>(filterId);
            return eventLog;
        }

        public async Task<Nethereum.Hex.HexTypes.HexBigInteger> CreateEventFilterAsync(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string eventName, Nethereum.RPC.Eth.DTOs.BlockParameter block, object[] filterTopics)
        {
            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
            var contractEvent = contract.GetEvent(eventName);
            var eventFilter = await contractEvent.CreateFilterAsync(block);
            return eventFilter;
        }

        public async Task<Nethereum.Hex.HexTypes.HexBigInteger> CreateEventFilterAsync<T>(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string eventName, Nethereum.RPC.Eth.DTOs.BlockParameter block, T filteredBy)
        {
            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
            var contractEvent = contract.GetEvent(eventName);
            var eventFilter = await contractEvent.CreateFilterAsync<T>(filteredBy, block);
            return eventFilter;
        }
        public async Task<Nethereum.Hex.HexTypes.HexBigInteger> CreateEventFilterAsync<T1, T2>(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string eventName, Nethereum.RPC.Eth.DTOs.BlockParameter block, T1 filteredByV1, T2 filteredByV2)
        {
            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
            var contractEvent = contract.GetEvent(eventName);
            var eventFilter = await contractEvent.CreateFilterAsync<T1, T2>(filteredByV1, filteredByV2, block);
            return eventFilter;
        }
        public async Task<Nethereum.Hex.HexTypes.HexBigInteger> CreateEventFilterAsync<T1, T2, T3>(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string eventName, Nethereum.RPC.Eth.DTOs.BlockParameter block, T1 filteredByV1, T2 filteredByV2, T3 filteredByV3)
        {
            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
            var contractEvent = contract.GetEvent(eventName);
            var eventFilter = await contractEvent.CreateFilterAsync<T1, T2, T3>(filteredByV1, filteredByV2, filteredByV3, block);
            return eventFilter;
        }
        public object[] CreateContractEventTopics(Nethereum.Web3.Web3 web3, string abiJson, string eventName, object[][] topics)
        {
            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), null);
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null);
            var eventAbi = contractBuilder.GetEventAbi(eventName);
            var eventTopicBuilder = new Nethereum.Contracts.EventTopicBuilder(eventAbi);
            if (topics == null || topics.Length == 0) return eventTopicBuilder.GetSignatureTopicAsTheOnlyTopic();
            else if (topics.Length == 1) return eventTopicBuilder.GetTopics(topics[0]);
            else if (topics.Length == 2) return eventTopicBuilder.GetTopics(topics[0], topics[1]);
            else return eventTopicBuilder.GetTopics(topics[0], topics[1], topics[2]);
        }
        public static Dictionary<string, object> DecodeEventLog<T>(string abiJson, Nethereum.RPC.Eth.DTOs.FilterLog eventLog, Func<T> memberTypeFactory, bool useAbiFieldName)
        {
            var contract = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null);
            Dictionary<string, Nethereum.ABI.Model.EventABI> eventAbi = contract.ContractABI.Events.ToDictionary(abi => abi.Sha3Signature.EnsureHexPrefix(), x => x);
            var eventDecoder = new EventTopicDecoder();
            var memberTypeObject = memberTypeFactory();
            var paramAttributes = eventAbi[eventLog.Topics[0] as string].InputParameters.Select<Nethereum.ABI.Model.Parameter, Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute>(
                x => new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order, x.Indexed));
            var result = eventDecoder.DecodeTopics(eventLog.Topics, eventLog.Data, memberTypeObject, paramAttributes, useAbiFieldName);
            return result;
        }

        public static List<Dictionary<string, object>> DecodeEventLog<T>(string abiJson, Nethereum.RPC.Eth.DTOs.FilterLog[] eventLog, Func<string, string, T> memberTypeFactory, bool useAbiFieldName)
        {
            var eventDecoder = new EventTopicDecoder();
            List<Dictionary<string, object>> output = new List<Dictionary<string, object>>();
     
            var contract = !string.IsNullOrEmpty(abiJson) ? new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null) : null;
            Dictionary<string, Nethereum.ABI.Model.EventABI> eventAbi = contract != null ? contract.ContractABI.Events.ToDictionary(abi => abi.Sha3Signature.EnsureHexPrefix(), x => x) : null;
            foreach (var log in eventLog)
            {
                var abi = eventAbi != null && eventAbi.ContainsKey(log.Topics[0] as string) ?  eventAbi[log.Topics[0] as string] : null;
                if (abi != null)
                {
                    var memberTypeObject = memberTypeFactory(abi.Sha3Signature, abi.Name);
                    var paramAttributes = abi.InputParameters.Select<Nethereum.ABI.Model.Parameter, Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute>(
                        x => new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order, x.Indexed));
                    var result = eventDecoder.DecodeTopics(log.Topics, log.Data, memberTypeObject, paramAttributes, useAbiFieldName);
                    result.Add("_LogInfo", new
                    {
                        EventName = abi.Name
                     ,
                        EventSha3Sig = abi.Sha3Signature
                     ,
                        Address = log.Address
                     ,
                        LogIndex = log.LogIndex,
                        BlockNumber = log.BlockNumber.Value,
                        TransactionHash = log.TransactionHash
                    });
                    output.Add(result);
                }
                else
                {
                    output.Add(new Dictionary<string, object>() {{ "_LogInfo", log }});
                }
            }
            return output;
        }
        public static string GetEventName(string abiJson, string eventNameHash)
        {
            var contract = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null);
            foreach (var evt in contract.ContractABI.Events)
            {
                if (evt.Sha3Signature.EnsureHexPrefix() == eventNameHash.EnsureHexPrefix())
                {
                    return evt.Name;
                }
            }
            return null;
        }
        public static string GetEventSignature(string abiJson, string eventName)
        {
            var contract = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null);
            foreach (var evt in contract.ContractABI.Events)
            {
                if (evt.Name == eventName)
                {
                    return evt.Sha3Signature.EnsureHexPrefix();
                }
            }
            return null;
        }
        public static string GetFunctionName(string abiJson, string functionNameHash)
        {
            var contract = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null);
            foreach (var function in contract.ContractABI.Functions)
            {
                if (function.Sha3Signature.EnsureHexPrefix() == functionNameHash.EnsureHexPrefix())
                {
                    return function.Name;
                }
            }
            return null;
        }
        public static Dictionary<string, object> DecodeFunctionOutput<T>(string abiJson, string data, string functionName, object[] functParam, Func<T> memberTypeFactory, bool useAbiFieldName)
        {
            var contract = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null);
            var functionAbi = (from fx in contract.ContractABI.Functions
                               where (fx.Sha3Signature == functionName || fx.Name == functionName)
                                     && (functParam == null || fx.InputParameters.Length == functParam.Length)
                               select fx).First();
            var memberTypeObject = memberTypeFactory();
            var functionDecoder = new FunctionOutputDecoder();
            var paramAttributes = functionAbi.OutputParameters.Select<Nethereum.ABI.Model.Parameter, Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute>(
                x =>
                {
                    if (x.ABIType.CanonicalName == "tuple")
                    {
                        Nethereum.ABI.TupleType t = x.ABIType as Nethereum.ABI.TupleType;
                        return new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order);
                    }
                    else
                    {
                        var attribute = new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order);
                        return attribute;
                    }
                }
                );
            var result = functionDecoder.DecodeFunctionOutput(data, memberTypeObject, paramAttributes, functionAbi.OutputParameters, useAbiFieldName);

            return result;
        }

        public static Dictionary<string, object> DecodeFunctionInput<T>(string contractAbi, string functionName, string data, Func<T> memberTypeFactory)
        {
            if (functionName == "constructor") return DecodeConstructorInput(contractAbi, data, memberTypeFactory);

            var contractBuilder = new Nethereum.Contracts.ContractBuilder(contractAbi, "0x0");
            var sig = data.HexToByteArray().Take(4).ToArray().ToHex(true);
            var functionBuilder = string.IsNullOrEmpty(functionName)
                ? contractBuilder.GetFunctionBuilderBySignature(sig)
                : contractBuilder.GetFunctionBuilder(functionName);
            //dynamic input = Ethereum.GetFunctionInputDTO(contractAbi, data.HexToByteArray());
            var functionInput = functionBuilder.DecodeInput(data);
            var functionDecoder = new FunctionOutputDecoder();
            
            var paramAttributes = functionInput.Select<Nethereum.ABI.FunctionEncoding.ParameterOutput, Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute>(
                o =>
                {
                    var x = o.Parameter;
                    if (x.ABIType.CanonicalName == "tuple")
                    {
                        Nethereum.ABI.TupleType t = x.ABIType as Nethereum.ABI.TupleType;
                        return new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order);
                    }
                    else
                    {
                        var attribute = new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order);
                        return attribute;
                    }
                }
                );
            var inputParameters = functionInput.Select(o => o.Parameter);

            var memberTypeObject = memberTypeFactory();

            data = new Regex("^0x" + functionBuilder.FunctionABI.Sha3Signature, RegexOptions.IgnoreCase).Replace(data, "0x");
            object[] revisedFunctParam = functionInput.Select(o => o.Result).ToArray();
            var result = functionDecoder.DecodeFunctionOutput(data, memberTypeObject, paramAttributes, inputParameters, true);
            return result;
        }

        public static Dictionary<string, object> DecodeConstructorInput<T>(string contractAbi, string data, Func<T> memberTypeFactory)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(contractAbi, "0x0");
            var constructorAbi = contractBuilder.ContractABI.Constructor;
            var constructorBuilder = new Nethereum.ABI.FunctionEncoding.ConstructorCallDecoder();
            var functionInput = constructorBuilder.DecodeDefaultData(data, constructorAbi.InputParameters);

            dynamic input = Ethereum.GetFunctionInputDTO(contractAbi, "constructor");
            var functionDecoder = new FunctionOutputDecoder();

            var paramAttributes = functionInput.Select<Nethereum.ABI.FunctionEncoding.ParameterOutput, Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute>(
                o =>
                {
                    var x = o.Parameter;
                    if (x.ABIType.CanonicalName == "tuple")
                    {
                        Nethereum.ABI.TupleType t = x.ABIType as Nethereum.ABI.TupleType;
                        return new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order);
                    }
                    else
                    {
                        var attribute = new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order);
                        return attribute;
                    }
                }
                );
            var inputParameters = functionInput.Select(o => o.Parameter);

            var memberTypeObject = memberTypeFactory();

            var result = functionDecoder.DecodeFunctionOutput(data, memberTypeObject, paramAttributes, inputParameters, true);
            return result;
        }

        public static string DecodeFunctionInput(string contractAbi, string functionName, string data)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(contractAbi, "0x0");
            dynamic input = string.IsNullOrEmpty(functionName)
                ? Ethereum.GetFunctionInputDTO(contractAbi, data.HexToByteArray())
                : Ethereum.GetFunctionInputDTO(contractAbi, functionName);
            var result = DecodeFunctionInput(contractAbi, functionName, (data ?? "").Trim(), () => input);
            var retValJson = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return retValJson;
        }

        public async Task<BigInteger> GetBalanceAsync(Nethereum.Web3.Web3 web3, string address)
        {
            return (await web3.Eth.GetBalance.SendRequestAsync(address)).Value;
        }
        public BigInteger GetBalance(string web3EndPoint, string address)
        {
            try
            {
                var web3 = Ethereum.GetWeb3Client(web3EndPoint);
                var result = System.Threading.Tasks.Task.Run(async () =>
                {
                    return await GetBalanceAsync(web3, address);
                }).Result;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException) throw ex.InnerException;
                else throw;
            }

        }
        public static async Task<BigInteger> GetPriceAsync(Nethereum.Web3.Web3 web3)
        {
            return (await web3.Eth.GasPrice.SendRequestAsync()).Value;
        }
        public static BigInteger GetPrice(Nethereum.Web3.Web3 web3)
        {
            try
            {
                var result = System.Threading.Tasks.Task.Run(async () =>
                {
                    return await GetPriceAsync(web3);
                }).Result;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException) throw ex.InnerException;
                else throw;
            }

        }

        public async Task<string> GetContractCode(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            return (await web3.Eth.GetCode.SendRequestAsync(contractAddress));
        }

        public async Task<Nethereum.RPC.Eth.DTOs.BlockWithTransactions> GetBlockWithTransactionsAsync(Nethereum.Web3.Web3 web3, Nethereum.RPC.Eth.DTOs.BlockParameter block = null)
        {
            return await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(block ?? Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());
        }
        public async Task<Nethereum.RPC.Eth.DTOs.BlockWithTransactionHashes> GetBlockWithTransactionsHashesAsync(Nethereum.Web3.Web3 web3, Nethereum.RPC.Eth.DTOs.BlockParameter block = null)
        {
            return await web3.Eth.Blocks.GetBlockWithTransactionsHashesByNumber.SendRequestAsync(block ?? Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());
        }

        public static List<T> DecodeEventLog<T>(string abiJson, Nethereum.RPC.Eth.DTOs.FilterLog[] eventLog) where T : new()
        {
            var contract = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null);
            Dictionary<string, Nethereum.ABI.Model.EventABI> eventAbi = contract.ContractABI.Events.ToDictionary(abi => abi.Sha3Signature.EnsureHexPrefix(), x => x);
            List<T> output = new List<T>();

            foreach (var log in eventLog)
            {
                var abi = eventAbi[log.Topics[0] as string];
                var eventDecoder = new Nethereum.ABI.FunctionEncoding.EventTopicDecoder();
                var result = eventDecoder.DecodeTopics<T>(log.Topics, log.Data);
                output.Add(result);
            }
            return output;
        }
        public async Task<Nethereum.Hex.HexTypes.HexBigInteger> CreateEventFilterAsync(Nethereum.Web3.Web3 web3, Nethereum.RPC.Eth.DTOs.BlockParameter fromBlock, Nethereum.RPC.Eth.DTOs.BlockParameter toBlock, string[] addresses, object[] topics)
        {
            Nethereum.RPC.Eth.DTOs.NewFilterInput filterInput = new Nethereum.RPC.Eth.DTOs.NewFilterInput
            {
                Address = addresses,
                FromBlock = fromBlock ?? Nethereum.RPC.Eth.DTOs.BlockParameter.CreateEarliest(),
                ToBlock = toBlock ?? Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest(),
                Topics = topics
            };
            return await web3.Eth.Filters.NewFilter.SendRequestAsync(filterInput);
        }

        public async Task<Nethereum.Hex.HexTypes.HexBigInteger> CreatePendingTransactionFilterAsync(Nethereum.Web3.Web3 web3)
        {
            return await web3.Eth.Filters.NewPendingTransactionFilter.SendRequestAsync();
        }

        public async Task<Nethereum.Hex.HexTypes.HexBigInteger> CreateNewBlockFilterAsync(Nethereum.Web3.Web3 web3)
        {
            return await web3.Eth.Filters.NewBlockFilter.SendRequestAsync();
        }

        public async Task<Nethereum.RPC.Eth.DTOs.FilterLog[]> GetFilterLogChangesAsync(Nethereum.Web3.Web3 web3, Nethereum.Hex.HexTypes.HexBigInteger filterId)
        {
            return await web3.Eth.Filters.GetFilterChangesForEthNewFilter.SendRequestAsync(filterId);
        }

        public async Task<Nethereum.RPC.Eth.DTOs.FilterLog[]> GetFilterLogs(Nethereum.Web3.Web3 web3, string[] addresses, object[] topics, Nethereum.RPC.Eth.DTOs.BlockParameter fromBlock, Nethereum.RPC.Eth.DTOs.BlockParameter toBlock)
        {
            Nethereum.RPC.Eth.DTOs.NewFilterInput input = new Nethereum.RPC.Eth.DTOs.NewFilterInput() { Address = addresses, FromBlock = fromBlock, ToBlock = toBlock, Topics = topics };
            return await web3.Eth.Filters.GetLogs.SendRequestAsync(input);
        }

        public async Task<string[]> GetFilterChangesForBlockOrTransactionAsync(Nethereum.Web3.Web3 web3, Nethereum.Hex.HexTypes.HexBigInteger filterId)
        {
            return await web3.Eth.Filters.GetFilterChangesForBlockOrTransaction.SendRequestAsync(filterId);
        }

        public async Task<bool> RemoveFilterAsync(Nethereum.Web3.Web3 web3, Nethereum.Hex.HexTypes.HexBigInteger filterId)
        {
            return await web3.Eth.Filters.UninstallFilter.SendRequestAsync(filterId);
        }

        public string CreateFunctionCallData(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string functionName, object[] functionInput)
        {
            var contract = web3.Eth.GetContract(AbiJSONTranslate(abiJson), contractAddress);
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.GetFunctionBuilder(functionName);
            var caller = contract.GetFunction(functionName);
            var callData = caller.GetData(TranslateCallInput(AbiJSONTranslate(abiJson), functionName, functionInput));
            return callData;
        }

        public async Task<string> ExecuteAsync(string to, string callData, Nethereum.Web3.Web3 web3, BigInteger gasLimit, BigInteger gasPriceInWei, BigInteger ethInWei, int chainId, BigInteger? maxPriorityFeePerGas = null, BigInteger? nonce = null, bool hardLimit = false)
        {
            var myAddress = string.IsNullOrEmpty(nethereumAccount.myAddress) ? await GetDefaultAccount(web3) : nethereumAccount.myAddress;
            var callInput = new CallInput()
            {
                To = to,
                Data = callData,
                From = myAddress,
                ChainId = chainId.ToHexBigInteger(),
                Value = ethInWei.ToHexBigInteger(),
                Gas = EthCallGas.ToHexBigInteger()
            };
            var gasNeeded = await this.EstimateGasAsync(web3, callInput);
            var gasBoosted = gasBoost(gasNeeded, hardLimit);
            var finalGasLimit = hardLimit ? (gasLimit == 0 ? gasNeeded : (gasLimit)) : (gasBoosted < gasLimit ? gasLimit : gasBoosted);
            return await SendTransactionAsync(web3, to, ethInWei, gasPriceInWei, finalGasLimit, callData, chainId, maxPriorityFeePerGas, nonce);
        }

        public async Task<string> ExecuteAsync(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string functionName, BigInteger gasLimit, BigInteger gasPriceInWei, BigInteger ethInWei, object[] functionInput, int chainId, BigInteger? maxPriorityFeePerGas, BigInteger? nonce = null, bool hardLimit = false)
        {
            var myAddress = string.IsNullOrEmpty(nethereumAccount.myAddress) ? await GetDefaultAccount(web3) : null;
            var contract = web3.Eth.GetContract(AbiJSONTranslate(AbiJSONTranslate(abiJson)), contractAddress);
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.GetFunctionBuilder(functionName);
            var caller = contract.GetFunction(functionName);
            var callData = caller.GetData(TranslateCallInput(AbiJSONTranslate(abiJson), functionName, functionInput));
            var gasNeeded = await this.EstimateGasAsync(AbiJSONTranslate(abiJson), contractAddress, web3, functionName, ethInWei, functionInput);
            var gasBoosted = gasBoost(gasNeeded, hardLimit);
            var finalGasLimit = hardLimit ? (gasLimit == 0 ? gasNeeded : (gasLimit)) : (gasBoosted < gasLimit ? gasLimit : gasBoosted);
            return await SendTransactionAsync(web3, contractAddress, ethInWei, gasPriceInWei, finalGasLimit, callData, chainId, maxPriorityFeePerGas, nonce);
        }

        public async Task<List<Dictionary<string, string>>> GetGEthPendingTransactionsAsync(string web3EndPoint)
        {
            var web3 = Ethereum.GetWeb3Client(web3EndPoint);
            var pendingTransactionsRequest = new RO.Common3.Ethereum.RPC.GEthPendingTransactions(web3.Client);
            var pendingTransactions = await web3.Client.SendRequestAsync<Nethereum.RPC.Eth.DTOs.Transaction[]>(pendingTransactionsRequest.BuildRequest());
            List<Dictionary<string, string>> transactions = new List<Dictionary<string, string>>();
            foreach (var t in pendingTransactions)
            {
                transactions.Add(new Dictionary<string, string>() {
                    {"To",t.To},
                    {"From",t.From},
                    {"Value",t.Value.Value.ToString()},
                    {"Input",t.Input},
                    {"Gas",t.Gas.Value.ToString()},
                    {"GasPrice",t.GasPrice.Value.ToString()},
                    {"Nonce",t.Nonce.Value.ToString()},
                    {"TransactionHash",t.TransactionHash},

                });
            }
            return transactions;
        }

        public static async Task<RO.Common3.Ethereum.RPC.TxPoolContent> GetGEthTxPoolContentAsync(string web3EndPoint)
        {
            var web3 = Ethereum.GetWeb3Client(web3EndPoint);
            var txPoolContentRequest = new RO.Common3.Ethereum.RPC.GEthTxPoolContent(web3.Client);
            var x = await web3.Client.SendRequestAsync<object>(txPoolContentRequest.BuildRequest());
            var y = Newtonsoft.Json.JsonConvert.SerializeObject(x);
            var txPoolContent = Newtonsoft.Json.JsonConvert.DeserializeObject<RO.Common3.Ethereum.RPC.TxPoolContent>(y, new RO.Common3.Ethereum.RPC.SingleOrArrayConverter<Nethereum.RPC.Eth.DTOs.Transaction>());

            return txPoolContent;
        }

        public static async Task<string> GetEthChainIdAsync(string web3EndPoint)
        {
            var web3 = Ethereum.GetWeb3Client(web3EndPoint);
            var chainIdRequest = new RO.Common3.Ethereum.RPC.EthChainId(web3.Client);
            var chainId = await web3.Client.SendRequestAsync<Nethereum.Hex.HexTypes.HexBigInteger>(chainIdRequest.BuildRequest());
            return chainId.Value.ToString();
        }

        public static string GetEthChainId(string web3EndPoint)
        {
            var result = System.Threading.Tasks.Task.Run(async () =>
            {
                var ret = await GetEthChainIdAsync(web3EndPoint);
                return ret;
            }).Result;
            return result;
        }
        public static object[] NormalizedFunctParam(Nethereum.ABI.Model.Parameter[] functInput, object[] functParam)
        {
            var revisedfunctParam = new object[functInput.Length];
            if (functParam != null)
            {
                for (int ii = 0; ii < functParam.Length && ii < functInput.Length; ii++)
                {
                    revisedfunctParam[ii] = functParam[ii];
                }
            }
            for (int ii = 0; ii < revisedfunctParam.Length; ii++)
            {
                if (revisedfunctParam[ii] == null)
                    revisedfunctParam[ii] = functInput[ii].ABIType.CanonicalName == "string" ? "" : (functInput[ii].ABIType.CanonicalName == "bool" ? (object)false : 0);
            }
            return revisedfunctParam;
        }
        public string ExecuteEthereumFunction(string web3EndPoint, string contractAddress, string contractAbi, string functionName, object[] functParam, BlockParameter block, BigInteger gasPriceInWei, int chainid, string ethInWeiToSent = "0", BigInteger? maxPriorityFeePerGas = null, BigInteger? nonce = null, bool hardLimit = false, int gasLimit = 0, bool useAbiFieldName = false, bool asConstant = false)
        {

            var function = Ethereum.GetFunctionBuilder(contractAbi, functionName);
            var revisedFunctParam = NormalizedFunctParam(function.FunctionABI.InputParameters, functParam);
            var isConstant = function.FunctionABI.Constant || asConstant;
            string result = null;
            var ethToSent = Ethereum.FromWei(ethInWeiToSent);
            try
            {
                result = System.Threading.Tasks.Task.Run(async () =>
                {
                    var web3 = Ethereum.GetWeb3Client(web3EndPoint);
                    if (isConstant)
                    {
                        string ret = await CallAsync(contractAbi, contractAddress, web3, functionName, ethToSent, revisedFunctParam, block);
                        return ret;
                    }
                    else
                    {
                        var txHash = await ExecuteAsync(contractAbi, contractAddress, web3, functionName, new BigInteger(gasLimit), gasPriceInWei, ethToSent, revisedFunctParam, chainid, maxPriorityFeePerGas, nonce, hardLimit);
                        return txHash;
                    }
                }).Result;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException) throw ex.InnerException;
                else throw;
            }

            if (isConstant)
            {
                dynamic output = Ethereum.GetFunctionOutputDTO(contractAbi, functionName);
                var retVal = Ethereum.DecodeFunctionOutput(contractAbi, result, functionName, revisedFunctParam, () => output, useAbiFieldName);
                var retValJson = Newtonsoft.Json.JsonConvert.SerializeObject(retVal);
                return retValJson;
            }
            else
            {
                return result;
            }
        }
        public string ExecuteEthereumFunction(string web3EndPoint, string contractAddress, string contractAbi, string functionName, object[] functParam, BigInteger gasPriceInWei, int chainid, string ethInWeiToSent = "0", BigInteger? maxPriorityFeePerGas = null, BigInteger? nonce = null, bool hardLimit = false, int gasLimit = 0, bool useAbiFieldName = false, bool asConstant = false)
        {
            return ExecuteEthereumFunction(web3EndPoint, contractAddress, contractAbi, functionName, functParam, BlockParameter.CreateLatest(), gasPriceInWei, chainid, ethInWeiToSent, maxPriorityFeePerGas, nonce, hardLimit, gasLimit, useAbiFieldName, asConstant);
        }

        public BigInteger EstimateGas(string web3EndPoint, string contractAddress, string contractAbi, string functionName, object[] functParam, string ethInWeiToSent = "0", bool hardLimit = false, bool calcEthNeeded = true)
        {
            var function = Ethereum.GetFunctionBuilder(contractAbi, functionName);
            BigInteger result = new BigInteger(0);
            var ethToSent = Ethereum.FromWei(ethInWeiToSent);
            try
            {
                if (function.FunctionABI.Constant) return new BigInteger(0);
                if (string.IsNullOrEmpty(contractAddress)) return new BigInteger(21000); // send ether
                result = System.Threading.Tasks.Task.Run(async () =>
                {
                    var web3 = Ethereum.GetWeb3Client(web3EndPoint);
                    var gasNeeded = await EstimateGasAsync(contractAbi, contractAddress, web3, functionName, BigInteger.Parse(ethInWeiToSent), functParam);
                    return gasBoost(gasNeeded, hardLimit) + new BigInteger(calcEthNeeded ? 1000 : 0);

                }).Result;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException) throw ex.InnerException;
                else throw;
            }
        }

        public async Task<string> SendEtherAsync(string web3EndPoint, string toWalletAddress, BigInteger ethInWei, BigInteger gasPriceInWei, int chainId, BigInteger? maxPriorityFeePerGas, BigInteger? nonce)
        {
            var web3 = Ethereum.GetWeb3Client(web3EndPoint);
            var myAddress = string.IsNullOrEmpty(nethereumAccount.myAddress) ? await GetDefaultAccount(web3) : nethereumAccount.myAddress;
            BigInteger gasNeeded = await this.EstimateGasAsync(web3, new CallInput() { To = toWalletAddress, From = myAddress, Value = new HexBigInteger(ethInWei) });

            string txHash = await SendTransactionAsync(web3, toWalletAddress, ethInWei, gasPriceInWei, gasNeeded, null, chainId, maxPriorityFeePerGas, nonce);
            return txHash;
        }
        public string SendEther(string web3EndPoint, string toWalletAddress, BigInteger ethInWei, BigInteger gasPriceInWei, int chainId, BigInteger? maxPriorityFeePerGas, BigInteger? nonce)
        {

            try
            {
                var result = System.Threading.Tasks.Task.Run(async () =>
                {
                    return await SendEtherAsync(web3EndPoint, toWalletAddress, ethInWei, gasPriceInWei, chainId, maxPriorityFeePerGas, nonce);
                }).Result;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException) throw ex.InnerException;
                else throw;
            }

        }

        public static Nethereum.Web3.Web3 GetWeb3Client(string web3url, int waitForSeconds = 90)
        {
            if (!web3url.StartsWith("http:") && !web3url.StartsWith("https:") && !web3url.StartsWith("ws:") && !web3url.StartsWith("wss:"))
            {
                Nethereum.JsonRpc.IpcClient.IpcClient ipcClient = new Nethereum.JsonRpc.IpcClient.IpcClient(web3url);
                try
                {
                    var _pipeClient = new System.IO.Pipes.NamedPipeClientStream(web3url);
                    _pipeClient.Connect(5000);
                }
                catch (TimeoutException)
                {
                    throw new EthereumRPCException(string.Format("connection timeout, check to see if the node is running locally ({0})", web3url));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                }
                var web3 = new Nethereum.Web3.Web3(ipcClient);
                return web3;
            }
            else
            {
                // should use our own rpcClient(i.e. httpClient) if we need to control the HTTP request say additional headers(bear token, infura origin etc.)
                Nethereum.JsonRpc.Client.RpcClient rpcClient = new RpcClient(new Uri(web3url));
                Nethereum.JsonRpc.Client.RpcClient.ConnectionTimeout = new TimeSpan(0, 0, waitForSeconds);
                var web3 = new Nethereum.Web3.Web3(rpcClient);
                return web3;
            }
        }

        public static Nethereum.Geth.Web3Geth GetGEthWeb3Client(string web3url)
        {
            if (!web3url.StartsWith("http:") && !web3url.StartsWith("https:") && !web3url.StartsWith("ws:") && !web3url.StartsWith("wss:"))
            {
                Nethereum.JsonRpc.IpcClient.IpcClient ipcClient = new Nethereum.JsonRpc.IpcClient.IpcClient(web3url);
                try
                {
                    var _pipeClient = new System.IO.Pipes.NamedPipeClientStream(web3url);
                    _pipeClient.Connect(5000);
                }
                catch (TimeoutException)
                {
                    throw new EthereumRPCException(string.Format("connection timeout, check to see if the node is running locally ({0})", web3url));
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                }
                var web3 = new Nethereum.Geth.Web3Geth(ipcClient);
                return web3;
            }
            else
            {
                var web3 = new Nethereum.Geth.Web3Geth(web3url);
                return web3;
            }
        }

        public static object[] ConvertCallParam(string jsonParamArray)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            return jss.Deserialize<List<object>>(jsonParamArray).ToArray();
        }
        public static BigInteger ConvertFromHex(string hexString)
        {
            return new Nethereum.Hex.HexConvertors.HexBigIntegerBigEndianConvertor().ConvertFromHex(hexString);
        }
        public static string ConvertToHex(BigInteger num)
        {
            return new Nethereum.Hex.HexConvertors.HexBigIntegerBigEndianConvertor().ConvertToHex(num);
        }
        public static EthereumSolcOutput GetCompiledContracts(string compiledJsonOutput)
        {
            if (string.IsNullOrEmpty(compiledJsonOutput)) return null;
            EthereumSolcOutoutVersion version = Newtonsoft.Json.JsonConvert.DeserializeObject<EthereumSolcOutoutVersion>(compiledJsonOutput);
            if (version == null) return null;
            Regex rxVer = new Regex(@"(\d+)\.(\d+)\.(\d+).*");
            var m = rxVer.Match(version.version ?? "");
            if (m == null) return null;
            var g = rxVer.Match(version.version).Groups;
            if (g == null || g.Count < 4) return null;
            int major = int.Parse(g[1].Value);
            int minor = int.Parse(g[2].Value);
            int patch = int.Parse(g[3].Value);
            if (major > 0 ||
                minor > 7)
            {
                EthereumSolcOutputPost7 x = GetCompiledContractsPost7(compiledJsonOutput);
                return x.ToEthereumSolcOutput();
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<EthereumSolcOutput>(compiledJsonOutput);
            }
        }
        public static EthereumSolcOutputPost7 GetCompiledContractsPost7(string compiledJsonOutput)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<EthereumSolcOutputPost7>(compiledJsonOutput);
        }
        public static EthereumCompilerMetaData GetCompiledContractCompilerSetting(string metadataJson)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<EthereumCompilerMetaData>(metadataJson);
        }
        public static string GetKeyCompilerInfo(EthereumCompilerMetaData meta)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                language = meta.language,
                version = meta.compiler.version,
                optimizer = new { enabled = meta.settings.optimizer.enabled, run = meta.settings.optimizer.runs },
                libraries = meta.settings.libraries
                //target = meta.settings.compilationTarget.First()
            });
        }
        public static System.Numerics.BigInteger FromGWei(string value)
        {
            decimal val = decimal.Parse(value);
            decimal exp = decimal.Parse("100000000");
            return new System.Numerics.BigInteger(val) * new System.Numerics.BigInteger(exp);
        }
        public static System.Numerics.BigInteger FromEth(string value)
        {
            decimal val = decimal.Parse(value);
            decimal exp = decimal.Parse("1000000000000000000");
            return new System.Numerics.BigInteger(val) * new System.Numerics.BigInteger(exp);
        }
        public static System.Numerics.BigInteger FromWei(string value)
        {
            decimal val = decimal.Parse(value);
            decimal exp = decimal.Parse("1");
            return new System.Numerics.BigInteger(val) * new System.Numerics.BigInteger(exp);
        }
        public static Nethereum.ABI.FunctionEncoding.SignatureEncoder GetSignatureEncoder()
        {
            return new Nethereum.ABI.FunctionEncoding.SignatureEncoder();
        }
        public static List<EthereumFunctionAbi> GetCompiledContractAbi(string abiJson)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<EthereumFunctionAbi>>(AbiJSONTranslate(abiJson));
        }
        public static Nethereum.Contracts.FunctionBuilder GetFunctionBuilder(string abiJson, string functionName)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.GetFunctionBuilder(functionName);
            return functionBuilder;
        }
        //public static Nethereum.Contracts.EventBuilder GetEventBuilder(string abiJson, string eventName)
        //{
        //    var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
        //    var eventBuilder = contractBuilder.GetEventBuilder(eventName);
        //    return eventBuilder;
        //}
        public static Nethereum.Contracts.ContractBuilder GetContractBuilder(string abiJson)
        {
            return new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
        }
        public static string[] GetStructDef(string abiJson)
        {
            var structures = new Dictionary<string, string[]>();
            var rxStruct = new Regex("struct\\s+[^.]*\\.");
            var rxArray = new Regex("\\[.*\\]");
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            Action<Nethereum.ABI.Model.Parameter[]> extractStruct = null;
            extractStruct = (parameters) =>
            {
                foreach (var p in parameters)
                {
                    string pTypeName = rxArray.Replace(p.InternalType ?? p.ABIType.Name, "");
                    if (p.ABIType is Nethereum.ABI.TupleType)
                    {
                        var components = (p.ABIType as Nethereum.ABI.TupleType).Components;
                        extractStruct(components);
                        var elements = components.Select(p1 => (p1.InternalType ?? p1.ABIType.CanonicalName) + " " + p1.Name);
                        structures[pTypeName] = elements.ToArray();
                    }
                    else if (p.ABIType.Name.StartsWith("tuple["))
                    {
                        Nethereum.ABI.ArrayType a = p.ABIType as Nethereum.ABI.ArrayType;
                        FieldInfo type = a.GetType().GetField("ElementType", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        PropertyInfo typeP = a.GetType().GetProperty("ElementType", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        Nethereum.ABI.TupleType elementType = type != null ? type.GetValue(a) as Nethereum.ABI.TupleType : typeP.GetValue(a) as Nethereum.ABI.TupleType;
                        var components = elementType.Components;
                        extractStruct(components);
                        var elements = elementType.Components.Select(p1 => (p1.InternalType ?? p1.ABIType.CanonicalName) + " " + p1.Name);
                        structures[pTypeName] = elements.ToArray();
                    }
                }
            };
            foreach (var f in contract.ContractABI.Functions)
            {
                extractStruct(f.InputParameters);
                extractStruct(f.OutputParameters);
            }
            foreach (var e in contract.ContractABI.Events)
            {
                extractStruct(e.InputParameters);
            }
            if (contract.ContractABI.Constructor != null)
            {
                extractStruct(contract.ContractABI.Constructor.InputParameters);
            }
            var solidityStruct = structures.Keys.Select(k =>
            {
                string structDef = string.Format("{0} {{ {1}; }}"
                    , rxStruct.Replace(k, "struct ")
                    , string.Join(";", structures[k].Select(t => rxStruct.Replace(t, "")).ToArray()));
                return structDef;
            }).ToArray();
            //return structures;
            return solidityStruct;
        }
        public static string decodeParameters(Nethereum.ABI.Model.Parameter[] parameters, bool forSig, bool expandStruct, bool forEvent)
        {
            var rxStruct = new Regex("struct\\s+[^.]*\\.");
            var rxArray = new Regex("\\[.*\\]");
            return string.Join(",", parameters.Select(pp =>
            {
                string ppTypeName = rxStruct.Replace(pp.InternalType ?? pp.ABIType.CanonicalName, "");
                if (pp.ABIType is Nethereum.ABI.TupleType)
                {
                    //string ppTypeName = pp.InternalType.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                    if (expandStruct) ppTypeName = "tuple";
                    Nethereum.ABI.TupleType cc = pp.ABIType as Nethereum.ABI.TupleType;
                    return ((forSig ? "" : ppTypeName)
                            + (expandStruct || forSig ? "(" + decodeParameters(cc.Components, forSig, expandStruct, forEvent) + ")" : "")
                            + (forSig ? "" : (pp.ABIType.IsDynamic() && !forEvent && !expandStruct ? " calldata" : "") + (pp.Indexed ? " indexed" : "") + " " + pp.Name)).Trim();
                }
                else if (pp.ABIType.Name.StartsWith("tuple["))
                {

                    // tuple array
                    //string ppTypeName = pp.InternalType.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                    if (expandStruct) ppTypeName = "tuple";
                    Nethereum.ABI.ArrayType a = pp.ABIType as Nethereum.ABI.ArrayType;
                    var arraySuffix = ppTypeName.Replace(rxArray.Replace(ppTypeName, ""), "");
                    FieldInfo type = a.GetType().GetField("ElementType", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    PropertyInfo typeP = a.GetType().GetProperty("ElementType", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    Nethereum.ABI.TupleType elementType = type != null ? type.GetValue(a) as Nethereum.ABI.TupleType : typeP.GetValue(a) as Nethereum.ABI.TupleType;
                    return ((forSig ? "" : ppTypeName)
                            + (expandStruct || forSig ? "(" + decodeParameters(elementType.Components, forSig, expandStruct, forEvent) + ")" + arraySuffix : "")
                            + (forSig
                                ? ""
                                : (pp.ABIType.IsDynamic() && !forEvent && !expandStruct ? " calldata" : "") + (pp.Indexed ? " indexed" : "") + " " + pp.Name)).Trim();
                }
                else
                    return (pp.ABIType.CanonicalName
                            + (forSig ? "" : (pp.ABIType.IsDynamic() && !forEvent && !expandStruct ? " calldata" : "" + (pp.Indexed ? " indexed" : ""))
                            + " " + pp.Name)).Trim();
            }).ToArray());
        }

        public static Dictionary<string, Nethereum.ABI.Model.FunctionABI> GetFunctionDef(string abiJson, bool expandStruct = false)
        {
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            //var sigEncoder = Ethereum.GetSignatureEncoder();
            var xx = new Nethereum.Util.Sha3Keccack();

            var functionDef = contract.ContractABI.Functions.Select(abi =>
            {
                var sig = abi.Sha3Signature;
                //var callParamName = string.Join(",", abi.InputParameters.Select(p => p.ABIType.CanonicalName).ToArray());
                //var callInterface = string.Join(",", abi.InputParameters.Select(p => (p.ABIType.CanonicalName + ' ' + p.Name).Trim()).ToArray());
                //var callParamName = string.Join(",", abi.InputParameters.Select(p => decodeParam(p, true)).ToArray());
                //var callInterface = string.Join(",", abi.InputParameters.Select(p => decodeParam(p, false)).ToArray());
                //var returnInterface = string.Join(",", abi.OutputParameters.Select(p => decodeParam(p,false)).ToArray());
                var callParamName = decodeParameters(abi.InputParameters, true, expandStruct, false);
                var callInterface = decodeParameters(abi.InputParameters, false, expandStruct, false);
                var returnInterface = decodeParameters(abi.OutputParameters, false, expandStruct, false);
                /* name format used to calc function sig(i.e. abi.Sha3Signature, keccak256(name).left(4bytes)), per ethereum spec */
                var name = abi.Name + "(" + callParamName + ")";
                /* function definition, for human */
                var docName = abi.Name + "(" + callInterface + ")" + (expandStruct ? "" : " external") + (abi.Constant ? " view" : "") + (!string.IsNullOrEmpty(returnInterface) ? " returns " + "(" + returnInterface + ")" : "");
                var yy = xx.CalculateHash(name).Substring(0, 4 * 2);
                return new
                {
                    functionName = docName,
                    functionAbi = abi,
                };
            }).ToDictionary(f => f.functionName, f => f.functionAbi);
            return functionDef;
        }

        public static Dictionary<string, Nethereum.ABI.Model.ConstructorABI> GetConstructorDef(string className, string abiJson, bool expandStruct = false)
        {
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            var abi = contract.ContractABI.Constructor;
            if (abi != null)
            {
                var contractClassName = className;
                //var callParamName = string.Join(",", abi.InputParameters.Select(p => p.ABIType.CanonicalName).ToArray());
                //var callInterface = string.Join(",", abi.InputParameters.Select(p => (p.ABIType.CanonicalName + ' ' + p.Name).Trim()).ToArray());
                var callParamName = decodeParameters(abi.InputParameters, true, expandStruct, false);
                var callInterface = decodeParameters(abi.InputParameters, false, expandStruct, false);
                var name = contractClassName + "(" + callParamName + ")";
                var docName = contractClassName + "(" + callInterface + ")";
                return new Dictionary<string, Nethereum.ABI.Model.ConstructorABI>() {
                    {docName,abi}
                };
            }
            else
                return new Dictionary<string, Nethereum.ABI.Model.ConstructorABI>()
                {
                    //                    {"", null}
                };
        }

        public static Dictionary<string, Nethereum.ABI.Model.EventABI> GetEventDef(string abiJson, bool expandStruct = false)
        {
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            //var sigEncoder = Ethereum.GetSignatureEncoder();
            var xx = new Nethereum.Util.Sha3Keccack();
            var eventDef = contract.ContractABI.Events.Select(abi =>
            {
                var sig = abi.Sha3Signature;
                //var callParamName = string.Join(",", abi.InputParameters.Select(p => p.ABIType.CanonicalName).ToArray());
                //var callInterface = string.Join(",", abi.InputParameters.Select(p => (p.ABIType.CanonicalName + ' ' + (p.Indexed ? "indexed " : "") + p.Name).Trim()).ToArray());
                var callParamName = decodeParameters(abi.InputParameters, true, expandStruct, true);
                var callInterface = decodeParameters(abi.InputParameters, false, expandStruct, true);
                var name = abi.Name + "(" + callParamName + ")";
                var docName = abi.Name + "(" + callInterface + ")";
                var yy = xx.CalculateHash(name);
                return new
                {
                    eventName = docName,
                    eventAbi = abi
                };
            }).ToDictionary(e => e.eventName, e => e.eventAbi);
            return eventDef;
        }

        public static dynamic GetFunctionInputDTO(string abiJson, byte[] txData)
        {

            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var sig = txData.Take(4).ToArray().ToHex(true);
            var functionAbi = contractBuilder.GetFunctionAbiBySignature(sig);
            dynamic functionInputDTO = new System.Dynamic.ExpandoObject();
            var inputParams = functionAbi.InputParameters;
            IDictionary<string, object> eo = functionInputDTO as IDictionary<string, object>;

            foreach (var p in inputParams)
            {
                var defaultType = p.ABIType.GetDefaultDecodingType();
                eo.Add(p.Name, p.ABIType.Name == "string" || p.ABIType.Name == "address"
                        ? (object)""
                        : p.ABIType.Name.StartsWith("byte") ? (object)new byte[1]
                        : (object)Activator.CreateInstance(defaultType)
                        );
            }
            return functionInputDTO;
        }

        public static dynamic GetFunctionInputDTO(string abiJson, string functionName)
        {
            if (functionName == "constructor") return GetConstructorInputDTO(abiJson);

            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.GetFunctionBuilder(functionName);
            dynamic functionInputDTO = new System.Dynamic.ExpandoObject();
            var inputParams = functionBuilder.FunctionABI.InputParameters;
            IDictionary<string, object> eo = functionInputDTO as IDictionary<string, object>;

            foreach (var p in inputParams)
            {
                var defaultType = p.ABIType.GetDefaultDecodingType();
                eo.Add(p.Name, p.ABIType.Name == "string" || p.ABIType.Name == "address"
                        ? (object)""
                        : p.ABIType.Name.StartsWith("byte") ? (object)new byte[1]
                        : (object)Activator.CreateInstance(defaultType)
                        );
            }
            return functionInputDTO;
        }

        public static dynamic GetConstructorInputDTO(string abiJson)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.ContractABI.Constructor;
            dynamic functionInputDTO = new System.Dynamic.ExpandoObject();
            var inputParams = functionBuilder.InputParameters;
            IDictionary<string, object> eo = functionInputDTO as IDictionary<string, object>;

            foreach (var p in inputParams)
            {
                var defaultType = p.ABIType.GetDefaultDecodingType();
                eo.Add(p.Name, p.ABIType.Name == "string" || p.ABIType.Name == "address"
                        ? (object)""
                        : p.ABIType.Name.StartsWith("byte") ? (object)new byte[1]
                        : (object)Activator.CreateInstance(defaultType)
                        );
            }
            return functionInputDTO;
        }

        public static string decodeFunctionOutput(string contractAbi, string functionName, string result, List<object> functionParams = null, bool useAbiFieldName = false)
        {
            dynamic output = GetFunctionOutputDTO(AbiJSONTranslate(contractAbi), functionName);
            var retVal = Ethereum.DecodeFunctionOutput(AbiJSONTranslate(contractAbi), result, functionName, functionParams != null ? functionParams.ToArray() : null, () => output, useAbiFieldName);
            var retValJson = Newtonsoft.Json.JsonConvert.SerializeObject(retVal);
            return retValJson;
        }

        public static dynamic GetFunctionOutputDTO(string abiJson, string functionName)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = functionName.StartsWith("0x")
                ? contractBuilder.GetFunctionBuilderBySignature(functionName)
                : contractBuilder.GetFunctionBuilder(functionName);
            dynamic functionOutputDTO = new System.Dynamic.ExpandoObject();
            var outputParams = functionBuilder.FunctionABI.OutputParameters;
            IDictionary<string, object> eo = functionOutputDTO as IDictionary<string, object>;
            int idx = 0;
            foreach (var p in outputParams)
            {
                Type defaultType = p.ABIType.GetDefaultDecodingType();
                var obj = p.ABIType.Name == "string" || p.ABIType.Name == "address"
                        ? (object)""
                        : p.ABIType.Name.StartsWith("byte") && !p.ABIType.Name.EndsWith("[]") ? (object)new byte[1]
                        : (object)Activator.CreateInstance(defaultType);
                if (p.ABIType.IsDynamic()
                        && p.ABIType.Name != "string"
                        && !p.ABIType.Name.StartsWith("byte")
                        && p.ABIType.CanonicalName != "tuple"
                    )
                {
                    ((IList)obj).Add(p.ABIType.CanonicalName == "tuple[]" ? (object)new Dictionary<string, object>() : (p.ABIType.Name.Contains("int") ? (object) new BigInteger(0) : (object)""));
                }
                eo.Add(string.IsNullOrEmpty(p.Name) ? "p" + idx.ToString() : p.Name, obj);
                idx = idx + 1;
            }
            return functionOutputDTO;
        }

        public static dynamic GetEventInputDTO(string abiJson, string eventNameOrSig)
        {
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            var eventInputDTO =
                    contract.ContractABI.Events
                        .Where(abi => abi.Sha3Signature == eventNameOrSig || abi.Name == eventNameOrSig)
                        .Select(abi =>
                        {
                            dynamic eventDTO = new System.Dynamic.ExpandoObject();
                            IDictionary<string, object> eo = eventDTO as IDictionary<string, object>;
                            foreach (var p in abi.InputParameters)
                            {
                                var defaultType = p.ABIType.GetDefaultDecodingType();
                                var obj = p.ABIType.Name == "string" || p.ABIType.Name == "address"
                                    ? (object)""
                                    : p.ABIType.Name.StartsWith("byte") ? (object)new byte[1]
                                    : (object)Activator.CreateInstance(defaultType);
                                if (p.ABIType.IsDynamic() && p.ABIType.Name != "string" && !p.ABIType.Name.StartsWith("byte"))
                                {
                                    ((IList)obj).Add("");
                                }
                                eo.Add(p.Name, obj);
                            }
                            return eventDTO;
                        }).FirstOrDefault();
            return eventInputDTO;
        }
        public static object[] TranslateEventFilterTopics(string abiJson, string eventNameOrSig, object[] filteredValues)
        {

            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            object[] translated =
                    contract.ContractABI.Events
                        .Where(abi => abi.Sha3Signature == eventNameOrSig || abi.Name == eventNameOrSig)
                        .Select(abi =>
                        {
                            //var xx = Ethereum.GetEventBuilder(AbiJSONTranslate(abiJson), eventNameOrSig);
                            List<object> translatedVal = new List<object>();
                            translatedVal.Add("0x" + abi.Sha3Signature);
                            if (filteredValues != null)
                            {
                                int ii = 0;
                                foreach (var p in abi.InputParameters)
                                {
                                    var f = ii < filteredValues.Length ? filteredValues[ii] : null;
                                    if (p.Indexed)
                                    {
                                        object v = null;
                                        if (f == null || (f is string && string.IsNullOrEmpty(f as string))) v = null;
                                        else if (f is string)
                                        {
                                            if (string.IsNullOrEmpty((string)f)) v = null;
                                            else if (p.ABIType.Name.StartsWith("uint")
                                                    || p.ABIType.Name.StartsWith("int")
                                                    || p.ABIType.Name.StartsWith("ufixed")
                                                    || p.ABIType.Name.StartsWith("fixed")
                                                    || p.ABIType.Name.StartsWith("bytes"))
                                            {
                                                // force uint* into BigInteger if supplied as string
                                                System.Numerics.BigInteger x = BigInteger.Parse((p.ABIType.Name.StartsWith("uint") || p.ABIType.Name.StartsWith("ufixed") ? "00" : "") + (f as string).Replace("0x", ""), (f as string).StartsWith("0x") ? System.Globalization.NumberStyles.AllowHexSpecifier : 0);
                                                v = x;
                                            }
                                            else if (p.ABIType.Name.StartsWith("bool"))
                                            {
                                                v = ((string)f) == "true";
                                            }
                                            else if (p.ABIType.Name == "address")
                                            {
                                                v = string.IsNullOrEmpty(f as string) ? null : f;
                                            }
                                        }
                                        else v = f;
                                        if (v == null) translatedVal.Add(null);
                                        else translatedVal.Add(p.ABIType.Encode(v).ToHex(true));
                                    }
                                    ii = ii + 1;
                                }
                            }
                            return translatedVal.ToArray();
                        }).FirstOrDefault();
            return translated;

        }
        public static object[] TranslateParamList(Nethereum.ABI.Model.Parameter[] inputParams, object[] inputs)
        {
            List<object> translated = new List<object>();
            var idx = 0;
            foreach (var p in inputParams)
            {
                if (idx == inputs.Length)
                {
                    // no more input, do nothing
                }
                else if (p.ABIType.CanonicalName == "tuple")
                {
                    var data = inputs[idx] as List<object>;
                    var translatedList = TranslateParamList((p.ABIType as Nethereum.ABI.TupleType).Components, data != null ? (object[])data.ToArray() : inputs[idx] as object[]);
                    translated.Add(translatedList);
                }
                else if (inputs[idx] is string
                    && (inputs[idx] as string).StartsWith("0x")
                    && (p.ABIType.IsDynamic() && (p.ABIType.Name == "bytes" || p.ABIType.Name == "bytes1[]")
                        || p.ABIType.Name == "bytes32"
                        || p.ABIType.Name.StartsWith("byte"))
                    )
                {
                    translated.Add(Utils.HexToByteArray(inputs[idx] as string));
                }
                else if (inputs[idx] is string && (p.ABIType.Name.StartsWith("uint") || p.ABIType.Name.StartsWith("int") || p.ABIType.Name.StartsWith("ufixed") || p.ABIType.Name.StartsWith("fixed") || p.ABIType.Name.StartsWith("bytes")))
                {
                    // force uint* into BigInteger if supplied as string
                    System.Numerics.BigInteger x = BigInteger.Parse(
                                                    (p.ABIType.Name.StartsWith("uint") || p.ABIType.Name.StartsWith("ufixed") || p.ABIType.Name.StartsWith("bool") ? "00" : "")
                                                    + (inputs[idx] as string).Replace("0x", "")
                                                    , (inputs[idx] as string).StartsWith("0x") ? System.Globalization.NumberStyles.AllowHexSpecifier : System.Globalization.NumberStyles.AllowLeadingSign);

                    translated.Add(x);
                }
                else if (p.ABIType.Name.StartsWith("address") && inputs[idx] is string)
                {
                    translated.Add(string.IsNullOrEmpty(inputs[idx] as string) ? null : (inputs[idx] as string).ConvertToEthereumChecksumAddress());
                }
                else if (p.ABIType.Name.StartsWith("string") && p.ABIType.Name != "string[]")
                {
                    translated.Add(inputs[idx] == null ? "" : inputs[idx].ToString());
                }
                else if (p.ABIType.Name.StartsWith("bool") && p.ABIType.Name != "bool[]")
                {
                    if (inputs[idx] is bool)
                    {
                        translated.Add(inputs[idx]);
                    }
                    else if (inputs[idx] is string || inputs[idx] == null)
                    {
                        var x = inputs[idx] as string;
                        if (x == "false") translated.Add(false);
                        else if (x == "true") translated.Add(true);
                        else if (string.IsNullOrEmpty(x)) translated.Add(false);
                        else
                        {
                            try
                            {
                                var y = int.Parse(x);
                                translated.Add(y != 0);
                            }
                            catch
                            {
                            }
                        }

                    }
                    else
                    {
                        try
                        {
                            var y = int.Parse(inputs[idx].ToString());
                            translated.Add(y != 0);
                        }
                        catch { }
                    }
                }
                else if (p.ABIType.Name.EndsWith("[]")
                        && p.ABIType.Name.StartsWith("byte")
                        && inputs[idx] != null
                        && (inputs[idx] is IEnumerable || inputs[idx] is IEnumerable<string>))
                {
                    translated.Add((inputs[idx] as IEnumerable<object>).Select((object x) => Utils.HexToByteArray(x as string)).ToArray());
                }
                else if (p.ABIType.Name == "tuple[]") {
                    var data = inputs[idx] as List<object>;
                    var elemementType = p.ABIType.GetPropertyValue("ElementType") as Nethereum.ABI.TupleType;
                    var translatedList = new List<object>();
                    foreach (var x in data)
                    {
                        var y = x as List<object>;
                        var translatedData = TranslateParamList(elemementType.Components, x != null ? (object[])y.ToArray() : x as object[]);
                        translatedList.Add(translatedData);
                    }
                    translated.Add(translatedList);
                }
                else
                {
                    translated.Add(inputs[idx]);
                }
                idx = idx + 1;
            }
            return translated.ToArray();

        }
        public static object[] TranslateCallInput(string abiJson, string functionName, object[] inputs)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.GetFunctionBuilder(functionName);
            if (functionBuilder == null) return inputs;
            var normalizedInputs = NormalizedFunctParam(functionBuilder.FunctionABI.InputParameters, inputs);
            return TranslateParamList(functionBuilder.FunctionABI.InputParameters, normalizedInputs);
        }

        public static object[] TranslateConstructorInput(string abiJson, object[] inputs)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var constructor = contractBuilder.ContractABI.Constructor;
            if (constructor == null) return new object[] { };
            var normalizedInputs = NormalizedFunctParam(constructor.InputParameters, inputs);
            return TranslateParamList(constructor.InputParameters, normalizedInputs);
        }
        public static string CreateContractDeployData(string byteCode, string abiJson, object[] constructorParams)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var constructor = contractBuilder.ContractABI.Constructor;
            var constructorEncoder = new Nethereum.ABI.FunctionEncoding.ConstructorCallEncoder();
            string data = constructor == null ? constructorEncoder.EncodeRequest(byteCode, "") : constructorEncoder.EncodeRequest(byteCode, constructor.InputParameters, TranslateConstructorInput(AbiJSONTranslate(abiJson), constructorParams));
            return data;
        }
        public string CreateFunctionCallData(string abiJson, string contractAddress, string functionName, object[] functionInput)
        {
            if (functionName == "constructor") return CreateConstructorData(abiJson, contractAddress, functionInput);

            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.GetFunctionBuilder(functionName);
            var callData = functionBuilder.GetData(TranslateCallInput(AbiJSONTranslate(abiJson), functionName, functionInput));
            return callData;
        }
        
        public string CreateConstructorData(string abiJson, string contractAddress, object[] constructorInput)
        {
            var contract = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var constructorAbi = contract.ContractABI.Constructor;
            var contractBuilder = new Nethereum.Contracts.DeployContractTransactionBuilder();
            var constructor = new Nethereum.ABI.FunctionEncoding.ConstructorCallEncoder();
            var callData = constructor.EncodeParameters(constructorAbi.InputParameters, TranslateConstructorInput(AbiJSONTranslate(abiJson), constructorInput));
            return callData.ToHex(true);
        }

        public static Tuple<int, string, string> CompileContract(string codeOrPath, string compilerPath, string baseDir = null)
        {
            bool isCodePath = File.Exists(codeOrPath);
            string workDirectory = isCodePath ? (baseDir != null ? baseDir : new FileInfo(codeOrPath).Directory.FullName) : "";
            string rootDirectory = isCodePath ? new FileInfo(baseDir ?? workDirectory).Directory.FullName : null;

            string cmd_arg = "--optimize" + (isCodePath ? (" --allow-paths \"" + rootDirectory + "\"") : "") + " --combined-json bin,bin-runtime,abi,hashes,metadata,userdoc,devdoc " + (isCodePath ? codeOrPath : "-");
            string er = "";
            string jsonResult = "";
            ProcessStartInfo psi = new ProcessStartInfo(compilerPath, cmd_arg);
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.WorkingDirectory = isCodePath ? workDirectory : Path.GetTempPath();
            System.Diagnostics.Process proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
            proc.ErrorDataReceived += (o, e) => er = er + (e.Data != null ? e.Data.ToString() + Environment.NewLine : string.Empty);
            proc.OutputDataReceived += (o, e) => jsonResult = jsonResult + (e.Data != null ? e.Data.ToString() : string.Empty);
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            if (!isCodePath)
            {
                proc.StandardInput.Write(codeOrPath);
                proc.StandardInput.Close();
            }
            proc.WaitForExit();
            proc.CancelErrorRead();
            proc.CancelOutputRead();
            return new Tuple<int, string, string>(proc.ExitCode, er, jsonResult);
        }
        public static string TranslateLibName(string byteCode)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("__[^_]+[_]+");
            var translated = regex.Replace(byteCode,
                (x) =>
                {
                    System.Text.RegularExpressions.Regex inner = new System.Text.RegularExpressions.Regex("[^_]+");
                    var result = inner.Replace(x.Value, y =>
                    {
                        var len = y.Length;
                        var stripped = y.Value.Replace("<stdin>:", "").Replace("[stdin]:", "");
                        return stripped + new String('_', len - stripped.Length);
                    });
                    return result;
                });
            return translated;
        }
        public static Tuple<int, string, string> TranslateAndLinkByteCode(string byteCode, string libraryAddressJson, string compilerPath)
        {
            string[] libraryAddress = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(string.IsNullOrEmpty(libraryAddressJson) ? "[]" : libraryAddressJson);
            var translatedByteCode = TranslateLibName(byteCode.Replace("<stdin>", "[stdin]"));
            var linkedResult = LinkContract(translatedByteCode, libraryAddress, compilerPath);
            return linkedResult;
        }
        public static Tuple<int, string, string> LinkContract(string byteCode, string[] libraryAddress, string compilerPath)
        {
            string cmd_arg = "- --link --libraries " + string.Join(",", libraryAddress);
            string er = "";
            string jsonResult = "";
            ProcessStartInfo psi = new ProcessStartInfo(compilerPath, cmd_arg);
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.WorkingDirectory = Path.GetTempPath();
            System.Diagnostics.Process proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
            proc.ErrorDataReceived += (o, e) => er = er + (e.Data != null ? e.Data.ToString() + Environment.NewLine : string.Empty);
            proc.OutputDataReceived += (o, e) => jsonResult = jsonResult + (e.Data != null ? e.Data.ToString() : string.Empty);
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            proc.StandardInput.Write(byteCode);
            proc.StandardInput.Close();
            proc.WaitForExit();
            proc.CancelErrorRead();
            proc.CancelOutputRead();
            // this is absolutely ridiculous to append message to output rather than console(i.e. er, this go ethereum people don't really know about programming in general
            return new Tuple<int, string, string>(proc.ExitCode, er, jsonResult.Replace("Linking completed.", ""));
        }
        /* need newer RO
        public static BackendDaemon StartGethNode(string cmdPath, string homeDirectory, string cmdParams, Action<object, EventArgs> onExit)
        {
            var daemon = new BackendDaemon(2048);
            int pid = daemon.Start(cmdPath, cmdParams, homeDirectory);
            return daemon;
        }
        */

        public static DateTime UnixTimeToDateTime(int unixTime, DateTimeKind kind = DateTimeKind.Utc)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0,  kind);
            return dateTime.AddSeconds(unixTime);
        }

        public static DateTime UnixTimeToDateTime(ulong unixTime, DateTimeKind kind = DateTimeKind.Utc)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, kind);
            return dateTime.AddSeconds(unixTime);
        }

        public static int DateTimeToUnixTime(DateTime time)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (int) (time - dateTime).TotalSeconds;
        }

        public static async Task<T> BinarySearchAsync<T,S>(Func<ulong, Task<T>> getAt, Func<T, S, int> comparer, ulong l, ulong r,  S searchFor)
        {
            ulong n = 0;
            ulong count = 0;
            while (l <= r)
            {
                n = (l + (r - l) / 2);
                if (n == 0) n = 1;
                count += 1;
                T item = await getAt(n);
                int c = comparer(item, searchFor);
                if (c > 0) r = n - 1;
                else if (c < 0) l = n + 1;
                else break;
            }
            count += 1;
            return await getAt(n);
        }
        public static async Task<Tuple<BigInteger, DateTime>> BlockNumberFromDateTimeAsync(Nethereum.Web3.IWeb3 web3, DateTime datetimeUtc) 
        {
            uint timestamp = (uint) DateTimeToUnixTime(datetimeUtc);
            var ethGetBlockByNumber = new RO.Common3.Ethereum.RPC.EthGetBlockByNumber(web3.Client);
            Func<ulong, ulong, ulong, ulong> calcBlockTime = (t0, t1, n) =>
            {
                return ((t1 >= t0 ? t1 - t0 : t0 - t1) + n / 2) / n;
            };
            Func<RPC.Block, RPC.Block, ulong> getBlockTime = (b0, b1) =>
            {
                return calcBlockTime(b0.Timestamp.ToUlong(), b1.Timestamp.ToUlong()
                    , b1.Number.ToUlong() >= b0.Number.ToUlong() 
                        ? b1.Number.ToUlong() - b0.Number.ToUlong() 
                        : b0.Number.ToUlong() - b1.Number.ToUlong());
            };
            var latestBlock = await ethGetBlockByNumber.SendRequestAsync(BlockParameter.CreateLatest());
            var latestBlockNumber = latestBlock.Number.ToUlong();
            var latestBlockTimestamp = latestBlock.Timestamp.ToUlong();

            if (timestamp >= latestBlockTimestamp) return new Tuple<BigInteger, DateTime>(new BigInteger(latestBlockNumber), UnixTimeToDateTime(latestBlockTimestamp));

            var midBlock = await ethGetBlockByNumber.SendRequestAsync(new BlockParameter(latestBlockNumber / 2));
            var midBlockNumber = midBlock.Number.ToUlong();
            var midBlockTimestamp = midBlock.Timestamp.ToUlong();

            if (timestamp == midBlockTimestamp) return new Tuple<BigInteger, DateTime>(new BigInteger(midBlockNumber), UnixTimeToDateTime(midBlockTimestamp));

            ulong blockTime = getBlockTime(midBlock, latestBlock);
            var lBlockNumber = midBlockTimestamp > timestamp ? 1 : midBlockNumber;
            var rBlockNumber = midBlockTimestamp > timestamp ? midBlockNumber : latestBlockNumber;

            Func<RPC.Block, ulong, ulong, ulong, Task<RPC.Block>> findFromRight = async (block, targetTimestamp, left, right) =>
            {
                ulong blockTimestamp = block.Timestamp.ToUlong();
                ulong blockNumber = block.Number.ToUlong();
                ulong priorBlockNumber = blockNumber;
                ulong count = 0;
                while (true) 
                {
                    ulong noOfBlocks = (
                                        (targetTimestamp >= blockTimestamp ? targetTimestamp - blockTimestamp : blockTimestamp - targetTimestamp)
                                        + blockTime/2) / blockTime;
                    if (noOfBlocks == 0) noOfBlocks = 1;
                    if (targetTimestamp >= blockTimestamp) {
                        blockNumber += noOfBlocks;
                        if (blockNumber > right) blockNumber = right;
                    }
                    else {
                        if (blockNumber > noOfBlocks)
                            blockNumber -= noOfBlocks;
                        else
                        {
                            if (blockNumber > left) blockNumber = left;
                            else break;
                        }
                    }
                    count += 1;
                    block = await ethGetBlockByNumber.SendRequestAsync(new BlockParameter(blockNumber));
                    blockTime = calcBlockTime(blockTimestamp, blockTimestamp = block.Timestamp.ToUlong(), noOfBlocks);
                    //blockTimestamp = block.Timestamp.ToUlong();
                    if (blockTimestamp >= targetTimestamp)
                    {
                        if (blockTimestamp - targetTimestamp <= 2 * blockTime)
                            break;
                    }
                    else if (blockNumber >= right) {
                        return null;
                    }
                }
                while (blockNumber > 1 && blockTimestamp > targetTimestamp)
                {
                    //ulong noOfBlocks = (blockTimestamp - targetTimestamp) / blockTime;
                    //if (noOfBlocks == 0) noOfBlocks = 1;
                    //else if (noOfBlocks >= blockNumber) noOfBlocks = blockNumber - 1;
                    ulong noOfBlocks = 1;
                    count += 1;
                    var priorBlock = await ethGetBlockByNumber.SendRequestAsync(new BlockParameter(blockNumber - noOfBlocks));
                    var priorBlockTimestamp = priorBlock.Timestamp.ToUlong();
                    if (priorBlockTimestamp < targetTimestamp)
                    {
                        if (targetTimestamp - priorBlockTimestamp < blockTimestamp - targetTimestamp)
                            return priorBlock;
                        else
                            break;
                    }
                    else {
                        blockNumber = priorBlock.Number.ToUlong();
                        block = priorBlock;
                        blockTimestamp = priorBlock.Timestamp.ToUlong();
                    }
                }
                return block;
            };

            //var x = await BinarySearchAsync<RPC.Block, ulong>(
            //                async (i) =>
            //                {
            //                    return await ethGetBlockByNumber.SendRequestAsync(new BlockParameter(i));
            //                }
            //                , (block, t) =>
            //                {
            //                    var ts = block.Timestamp.ToUlong();
            //                    return (int)ts - (int)t;
            //                }
            //                , lBlockNumber, rBlockNumber, timestamp);

            var closest = await findFromRight(midBlock, timestamp, lBlockNumber, rBlockNumber);
            
            if (closest != null)
            {
                return new Tuple<BigInteger, DateTime>(closest.Number.ToUlong(), UnixTimeToDateTime(closest.Timestamp.ToUlong()));
            }
            else if (rBlockNumber == latestBlockNumber)
            {
                return new Tuple<BigInteger, DateTime>(latestBlockNumber, UnixTimeToDateTime(latestBlockTimestamp));
            }
            else
            {
                return new Tuple<BigInteger, DateTime>(1, UnixTimeToDateTime((await ethGetBlockByNumber.SendRequestAsync(BlockParameter.CreateEarliest())).Timestamp.ToUlong()));
            }
        }
        public static async Task<System.Numerics.BigInteger> GetLatestBlockNumber(string web3UrlEndpoint)
        {
            var web3 = new Nethereum.Web3.Web3(web3UrlEndpoint);
            var lastBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            return lastBlockNumber;
        }
        public static string GetNodeStatus(string web3UrlEndpoint)
        {
            try
            {
                var web3 = new Nethereum.Web3.Web3(web3UrlEndpoint);
                var chainId = Task.Run(async () =>
                {
                    return await GetEthChainIdAsync(web3UrlEndpoint);
                }).Result;

                var gasPrice = GetPrice(web3);

                var latestBlock = Task.Run(async () =>
                {
                    var lastBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
                    return await new RO.Common3.Ethereum.RPC.EthGetBlockByNumber(web3.Client).SendRequestAsync(lastBlockNumber);
                    //return await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new Nethereum.Hex.HexTypes.HexBigInteger(lastBlockNumber.HexValue));
                }).Result;

                var blockTime = UnixTimeToDateTime(int.Parse(latestBlock.Timestamp.Value.ToString()));
                var timeDiff = DateTime.UtcNow - blockTime;

                var syncingBlock = Task.Run(async () =>
                {
                    var eth_syncing = await web3.Eth.Syncing.SendRequestAsync();
                    if (eth_syncing.IsSyncing)
                    {
                        var currentSyncingBlock = await new RO.Common3.Ethereum.RPC.EthGetBlockByNumber(web3.Client).SendRequestAsync((eth_syncing.CurrentBlock));
                        var startingSyncingBlock = await new RO.Common3.Ethereum.RPC.EthGetBlockByNumber(web3.Client).SendRequestAsync((eth_syncing.StartingBlock));
                        //                        var currentSyncingBlock = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new Nethereum.Hex.HexTypes.HexBigInteger(eth_syncing.CurrentBlock.HexValue));
                        //                        var startingSyncingBlock = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new Nethereum.Hex.HexTypes.HexBigInteger(eth_syncing.StartingBlock.HexValue));
                        //                        return new Nethereum.RPC.Eth.DTOs.BlockWithTransactions[2] { currentSyncingBlock, startingSyncingBlock };
                        return new RO.Common3.Ethereum.RPC.Block[2] { currentSyncingBlock, startingSyncingBlock };
                    }
                    else
                    {
                        return null;
                    }
                }).Result;
                return string.Format("ChainI {4}: Last block {0} - block time {1}, lag by {2} minutes, syncing : {3}, gas Price: {5}, gas Limit: {6}",
                    (int)latestBlock.Number.Value,
                    blockTime.ToLocalTime(),
                    (int)timeDiff.TotalMinutes,
                    syncingBlock == null ? "false" : (
                        string.Format("block {0}, block time {1}", syncingBlock[0].Number.Value, UnixTimeToDateTime(int.Parse(syncingBlock[0].Timestamp.Value.ToString())).ToLocalTime()
                    )),
                    chainId,
                    gasPrice,
                    latestBlock.GasLimit
                    );
            }
            catch (Exception er)
            {
                if (er.InnerException != null && er.InnerException.Message.Contains("rpc"))
                    return "Cannot connect to endpoint " + er.InnerException.Message;
                else
                    return er.Message;
            }
        }

        public static async Task<Tuple<StreamingWebSocketClient
            , EthLogsObservableSubscription
            , EthNewBlockHeadersObservableSubscription
            , EthNewPendingTransactionObservableSubscription>> abc(string contractAddress, string wsEndpoint, Action<string> logger)
        {
            //DAI contract address
            //var contractAddress = "0x6b175474e89094c44da98b954eedeac495271d0f";

            try
            {
                var client = new StreamingWebSocketClient(wsEndpoint);

                var newBlockSubscription = new Nethereum.RPC.Reactive.Eth.Subscriptions.EthNewBlockHeadersObservableSubscription(client);
                var pendingTransactionSubscription = new Nethereum.RPC.Reactive.Eth.Subscriptions.EthNewPendingTransactionObservableSubscription(client);
                var eventSubscription = new Nethereum.RPC.Reactive.Eth.Subscriptions.EthLogsObservableSubscription(client);
                eventSubscription.GetSubscriptionDataResponsesAsObservable().Subscribe(log =>
                {
                    var transfer = log.DecodeEvent<TransferEventDTO>();
                    logger(string.Format("{0}: {1} -> {2} - {3}", log.Address, transfer.Event.From, transfer.Event.To, transfer.Event.Value));
                }, (e) =>
                {
                    logger(string.Format("event subscription exception {0}", e.Message));
                }, () =>
                {
                    logger(string.Format("event subscription stopped"));
                });
                newBlockSubscription.GetSubscriptionDataResponsesAsObservable().Subscribe(b =>
                {
                    logger(string.Format("new block {0}, {1}", b.Number, b.BaseFeePerGas));
                }, (e) =>
                {
                    logger(string.Format("new block subscription exception {0}", e.Message));
                }, () =>
                {
                    logger(string.Format("new subscription stopped"));
                });
                newBlockSubscription.GetSubscribeResponseAsObservable().Subscribe((x) =>
                {
                    logger(string.Format("new block sub {0}", x));
                }, (e) =>
                {
                    logger(string.Format("new block sub exception {0}", e));
                }, () =>
                {
                    logger(string.Format("new block sub done"));
                });
                pendingTransactionSubscription.GetSubscriptionDataResponsesAsObservable().Subscribe(txHash =>
                {
                    logger(string.Format("new pending transaction {0}", txHash));
                }, (e) =>
                {
                    logger(string.Format("new pending subscription exception {0}", e.Message));
                }, () =>
                {
                    logger(string.Format("new pending subscription stopped"));
                });
                pendingTransactionSubscription.GetSubscribeResponseAsObservable().Subscribe((x) =>
                {
                    logger(string.Format("Pending transaction sub {0}", x));
                }, (e) =>
                {
                    logger(string.Format("Pending transaction sub exception {0}", e));
                }, () =>
                {
                    logger(string.Format("Pending transaction sub done"));
                });

                eventSubscription.GetSubscribeResponseAsObservable().Subscribe(
                    id => logger(string.Format("Subscribed with id: {0}", id)));

                var filterAuction = Event<TransferEventDTO>.GetEventABI().CreateFilterInput(contractAddress);

                client.Error += (sender, e) =>
                {
                    logger(string.Format("websocket client exception {0}, {1}", e, e.Message));
                };

                await client.StartAsync().ConfigureAwait(false);

                await eventSubscription.SubscribeAsync(filterAuction, 1).ConfigureAwait(false);
                await newBlockSubscription.SubscribeAsync(2).ConfigureAwait(false);
                await pendingTransactionSubscription.SubscribeAsync(3).ConfigureAwait(false);

                return new Tuple<StreamingWebSocketClient, EthLogsObservableSubscription, EthNewBlockHeadersObservableSubscription, EthNewPendingTransactionObservableSubscription>(client, eventSubscription, newBlockSubscription, pendingTransactionSubscription);
                //Console.ReadLine();

                //await eventSubscription.UnsubscribeAsync();
            }
            catch (Exception e)
            {
                logger(e.Message);
                throw;
            }
        }

        public static async Task<Tuple<StreamingWebSocketClient, EthLogsObservableSubscription>> ERC20TransferWatcher(string contractAddress, string wsEndpoint, Action<Dictionary<string, string>> transferUpdater, Action<string> logger, Action<string> busyLogger)
        {
            try
            {
                var client = new StreamingWebSocketClient(wsEndpoint);
                var newBlockSubscription = new Nethereum.RPC.Reactive.Eth.Subscriptions.EthNewBlockHeadersObservableSubscription(client);
                var eventSubscription = new Nethereum.RPC.Reactive.Eth.Subscriptions.EthLogsObservableSubscription(client);
                var tokenTransferred = Event<TransferEventDTO>.GetEventABI().CreateFilterInput(contractAddress);

                eventSubscription.GetSubscriptionDataResponsesAsObservable().Subscribe(log =>
                {
                    var answer = log.DecodeEvent<TransferEventDTO>();
                    transferUpdater(new Dictionary<string, string>() {
                        {"from",answer.Event.From.ToString()},
                        {"to", answer.Event.To.ToString()},
                        {"value", answer.Event.Value.ToString()},
                        {"blockNumber", answer.Log.BlockNumber.ToString()},
                        {"txhash", answer.Log.TransactionHash.ToString()},
                    });
                    //logger(string.Format("{0}: {1} -> {2} - {3}", log.Address, answer.Event.RoundId, answer.Event.UpdatedAt, answer.Event.Current));
                }, (e) =>
                {
                    logger(string.Format("event subscription exception {0}", e.Message));
                }, () =>
                {
                    logger(string.Format("event subscription stopped"));
                });

                // this is a must to keep the websocket alive(effectively ping)
                newBlockSubscription.GetSubscriptionDataResponsesAsObservable().Subscribe(b =>
                {
                    busyLogger(string.Format("new block {0}, {1}, {2}", b.Number, b.BaseFeePerGas, b.Timestamp));
                }, (e) =>
                {
                    logger(string.Format("new block subscription exception {0}", e.Message));
                }, () =>
                {
                    logger(string.Format("new block subscription stopped"));
                });

                client.Error += (sender, e) =>
                {
                    logger(string.Format("websocket client exception {0}, {1}", e, e.Message));
                };

                await client.StartAsync().ConfigureAwait(false);

                await eventSubscription.SubscribeAsync(tokenTransferred).ConfigureAwait(false);
                await newBlockSubscription.SubscribeAsync(2).ConfigureAwait(false);

                return new Tuple<StreamingWebSocketClient, EthLogsObservableSubscription>(client, eventSubscription);
            }
            catch
            {
                return null;
            }
        }
        public static string CreateContractInterfaceCode(string contractAbi, string interfaceName, bool withSig = false, bool expandstruct = false)
        {
            var functionDef = GetFunctionDef(contractAbi, expandstruct);
            var eventDef = GetEventDef(contractAbi, expandstruct);
            var constructorDef = GetConstructorDef(interfaceName, contractAbi, expandstruct);

            string solidityCode = string.Format(
                    (!expandstruct
                        ? "interface {0} {{\r\n\r\n{5}\r\n\r\n//constructor\r\n//{4}\r\n\r\n// functions\r\n{1}\r\n\r\n// transactions\r\n{2}\r\n\r\n//events\r\n{3}\r\n}}"
                        : "\r\n//constructor\r\n{4}\r\n\r\n// functions\r\n{1}\r\n\r\n// transactions\r\n{2}\r\n\r\n//events\r\n{3}\r\n"
                        ),
                    string.IsNullOrEmpty(interfaceName) ? "IChangeme" : interfaceName,
                    string.Join("\r\n", functionDef.Where(f => f.Value.Constant).OrderBy(f => f.Key).Select(f => "function " + f.Key + ";" + (withSig ? " //sig:0x" + f.Value.Sha3Signature : "")).ToArray()),
                    string.Join("\r\n", functionDef.Where(f => !f.Value.Constant).OrderBy(f => f.Key).Select(f => "function " + f.Key + ";" + (withSig ? " //sig:0x" + f.Value.Sha3Signature : "")).ToArray()),
                    string.Join("\r\n", eventDef.OrderBy(evt => evt.Key).Select(evt => "event " + evt.Key + ";" + (withSig ? " //topic:0x" + evt.Value.Sha3Signature : "")).ToArray()),
                    string.Join("\r\n", constructorDef.Select(c => c.Key).ToArray()) + (constructorDef.Keys.Count > 0 ? ";" : ""),
                    string.Join("\r\n", GetStructDef(contractAbi))
                    );
            return solidityCode;
        }
        public static async Task<Dictionary<BigInteger, DateTime>> GetBlockTimestampsAsync(string rpcEndpointUrl, IEnumerable<BigInteger> blocks)
        {
            Dictionary<BigInteger, DateTime> blockTimestamp;
            var roEthereum = new RO.Common3.Ethereum.Ethereum();
            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(rpcEndpointUrl);
            string chainId = await RO.Common3.Ethereum.Ethereum.GetEthChainIdAsync(rpcEndpointUrl);
            lock (ChainBlockTimestamp)
            {
                if (!ChainBlockTimestamp.ContainsKey(chainId))
                {

                    blockTimestamp = new Dictionary<BigInteger, DateTime>();
                    ChainBlockTimestamp[chainId] = blockTimestamp;
                }
                else
                {
                    blockTimestamp = ChainBlockTimestamp[chainId];
                }
            }
            var newBlocks = blocks.Where(blockNumber => !blockTimestamp.ContainsKey(blockNumber));

            var getTimestamps = newBlocks.Select(blockNumber => roEthereum.GetBlockTimestampAsync((ulong)blockNumber, web3)).ToArray();
            await Task.WhenAll(getTimestamps);
            int ii = 0;
            foreach (var b in newBlocks)
            {
                blockTimestamp[b] = getTimestamps[ii].Result;
                ii += 1;
            }
            return blockTimestamp;
        }
        public static async Task<Dictionary<string, Transaction>> GetTransactionsAsync(string rpcEndpointUrl, IEnumerable<string> txHashes, bool withReceipts = false)
        {
            Dictionary<string, Transaction> transactions;
            var roEthereum = new RO.Common3.Ethereum.Ethereum();
            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(rpcEndpointUrl);
            string chainId = await RO.Common3.Ethereum.Ethereum.GetEthChainIdAsync(rpcEndpointUrl);
            lock (ChainTransactions)
            {
                if (!ChainTransactions.ContainsKey(chainId))
                {

                    transactions = new Dictionary<string, Transaction>();
                    ChainTransactions[chainId] = transactions;
                }
                else
                {
                    transactions = ChainTransactions[chainId];
                }
            }
            var newTxns = txHashes.Where(txHash => !transactions.ContainsKey(txHash));
            var getTxns = newTxns.Select(txHash => roEthereum.GetTransactionAsync(txHash, web3)).ToArray();
            await Task.WhenAll(getTxns);
            int ii = 0;
            foreach (var b in newTxns)
            {
                transactions[b] = getTxns[ii].Result;
                ii += 1;
            }
            return transactions;
        }
        public static async Task<Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt>> GetTransactionReceiptsAsync(string rpcEndpointUrl, IEnumerable<string> txHashes)
        {
            Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt> receipts;
            var roEthereum = new RO.Common3.Ethereum.Ethereum();
            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(rpcEndpointUrl);
            string chainId = await RO.Common3.Ethereum.Ethereum.GetEthChainIdAsync(rpcEndpointUrl);
            lock (ChainTransactionReceipts)
            {
                if (!ChainTransactionReceipts.ContainsKey(chainId))
                {

                    receipts = new Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt>();
                    ChainTransactionReceipts[chainId] = receipts;
                }
                else
                {
                    receipts = ChainTransactionReceipts[chainId];
                }
            }
            var newTxns = txHashes.Where(txHash => !receipts.ContainsKey(txHash));
            var getTxnReceipts = newTxns.Select(txHash => roEthereum.GetTransactionReceiptAsync(txHash, web3)).ToArray();
            await Task.WhenAll(getTxnReceipts);
            int ii = 0;
            foreach (var b in newTxns)
            {
                receipts[b] = getTxnReceipts[ii].Result;
                ii += 1;
            }
            return receipts;
        }
        public static Dictionary<BigInteger, DateTime> UpdChainBlockTimestamp(string chainId, IEnumerable<Tuple<BigInteger, DateTime>> blockTimestamps, bool replace = true)
        {
            lock (ChainBlockTimestamp)
            {
                Dictionary<BigInteger, DateTime> cachedBlockTimestamps;
                if (ChainBlockTimestamp.ContainsKey(chainId))
                {
                    cachedBlockTimestamps = ChainBlockTimestamp[chainId];
                }
                else
                {
                    cachedBlockTimestamps = new Dictionary<BigInteger, DateTime>();
                    ChainBlockTimestamp[chainId] = cachedBlockTimestamps;
                }
                foreach (var b in blockTimestamps)
                {
                    if (replace || !cachedBlockTimestamps.ContainsKey(b.Item1))
                    {
                        cachedBlockTimestamps[b.Item1] = b.Item2;
                    }
                }
                return cachedBlockTimestamps;
            }
        }

        #region MultiSigWallet(Gnosis)
        public static async Task<List<BigInteger>> GetPendingMultiSigConfirmations(string rpcEndpointUrl, string multiSigWalletAddress, string account, BlockParameter fromBlock = null, BlockParameter toBlock = null)
        {
            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(rpcEndpointUrl);
            var submissionEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmissionEventDTO>(multiSigWalletAddress);
            var filterAllSubmissionEvent = submissionEventHandler.CreateFilterInput(fromBlock, toBlock);
            var submissionEvents = submissionEventHandler.GetAllChangesAsync(filterAllSubmissionEvent);

            var confirmationEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.ConfirmationEventDTO>(multiSigWalletAddress);
            var filterAllMyConfirmationEvent = confirmationEventHandler.CreateFilterInput(account, fromBlock, toBlock);
            var myConfirmationEvents = confirmationEventHandler.GetAllChangesAsync(filterAllMyConfirmationEvent);

            var tasks = new List<Task>() { 
                submissionEvents,
                myConfirmationEvents,
            };
            Dictionary<BigInteger, DateTime> blocks = new Dictionary<BigInteger, DateTime>();
            HashSet<string> txHashes = new HashSet<string>();

            await Task.WhenAll(tasks);
            foreach (var x in submissionEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }
            foreach (var x in myConfirmationEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }

            var roEthereum = new RO.Common3.Ethereum.Ethereum();
            var blockNumbers = blocks.Keys.ToArray();
            var lastBlockNumber = blockNumbers.Length > 0 ? blockNumbers.Max() : 0;
            Dictionary<BigInteger, DateTime> blockTimestamp = await GetBlockTimestampsAsync(rpcEndpointUrl, blockNumbers);
            var transactionInfos = await GetTransactionsAsync(rpcEndpointUrl, txHashes);
            HashSet<BigInteger> txns = new HashSet<BigInteger>();
            var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);

            foreach (var e in submissionEvents.Result)
            {
                var txnId = e.Event.TransactionId;
                txns.Add(txnId);
            }
            foreach (var e in myConfirmationEvents.Result)
            {
                var txnId = e.Event.TransactionId;
                txns.RemoveWhere((x) => txnId == x);
            }
            return txns.ToList();
        }
        public static async Task<Tuple<BigInteger, Dictionary<BigInteger, List<Tuple<string, string>>>>> GetMultiSigTx(string rpcEndpointUrl, string multiSigWalletAddress, BlockParameter fromBlock = null, BlockParameter toBlock = null)
        {
            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(rpcEndpointUrl);
            var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
            var required = await multiSigWallet.RequiredQueryAsync();
            var submissionEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmissionEventDTO>(multiSigWalletAddress);
            var filterAllSubmissionEvent = submissionEventHandler.CreateFilterInput(fromBlock, toBlock);
            var submissionEvents = submissionEventHandler.GetAllChangesAsync(filterAllSubmissionEvent);

            var confirmationEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.ConfirmationEventDTO>(multiSigWalletAddress);
            var filterAllMyConfirmationEvent = confirmationEventHandler.CreateFilterInput(fromBlock, toBlock);
            var confirmationEvents = confirmationEventHandler.GetAllChangesAsync(filterAllMyConfirmationEvent);

            var tasks = new List<Task>() { 
                submissionEvents,
                confirmationEvents,
            };
            Dictionary<BigInteger, DateTime> blocks = new Dictionary<BigInteger, DateTime>();
            HashSet<string> txHashes = new HashSet<string>();

            await Task.WhenAll(tasks);
            foreach (var x in submissionEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }
            foreach (var x in confirmationEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }

            var roEthereum = new RO.Common3.Ethereum.Ethereum();
            var blockNumbers = blocks.Keys.ToArray();
            var lastBlockNumber = blockNumbers.Length > 0 ? blockNumbers.Max() : 0;
            Dictionary<BigInteger, DateTime> blockTimestamp = await GetBlockTimestampsAsync(rpcEndpointUrl, blockNumbers);
            var transactionInfos = await GetTransactionsAsync(rpcEndpointUrl, txHashes);
            Dictionary<BigInteger, List<Tuple<string, string>>> txns = new Dictionary<BigInteger, List<Tuple<string, string>>>();

            foreach (var e in submissionEvents.Result)
            {
                var txnId = e.Event.TransactionId;
                txns[txnId] = new List<Tuple<string, string>>();
            }

            foreach (var e in confirmationEvents.Result)
            {
                var txnId = e.Event.TransactionId;
                var confirmedBy = e.Event.Sender;
                var txHash = e.Log.TransactionHash;
                List<Tuple<string, string>> confirmations = txns[txnId];
                confirmations.Add(new Tuple<string, string>(confirmedBy, txHash));
            }
            return new Tuple<BigInteger, Dictionary<BigInteger, List<Tuple<string, string>>>>(required, txns);
        }

        public static async Task<Tuple<BigInteger, Dictionary<BigInteger, List<Tuple<string, string>>>>> GetMultiSigSubmissions(string rpcEndpointUrl, string multiSigWalletAddress, BlockParameter fromBlock = null, BlockParameter toBlock = null)
        {
            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(rpcEndpointUrl);
            var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
            var required = await multiSigWallet.RequiredQueryAsync();
            var submissionEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmissionEventDTO>(multiSigWalletAddress);
            var filterAllSubmissionEvent = submissionEventHandler.CreateFilterInput(fromBlock, toBlock);
            var submissionEvents = submissionEventHandler.GetAllChangesAsync(filterAllSubmissionEvent);

            var tasks = new List<Task>() { 
                submissionEvents,
            };
            Dictionary<BigInteger, DateTime> blocks = new Dictionary<BigInteger, DateTime>();
            HashSet<string> txHashes = new HashSet<string>();

            await Task.WhenAll(tasks);
            foreach (var x in submissionEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }

            var roEthereum = new RO.Common3.Ethereum.Ethereum();
            var blockNumbers = blocks.Keys.ToArray();
            var lastBlockNumber = blockNumbers.Length > 0 ? blockNumbers.Max() : 0;
            Dictionary<BigInteger, DateTime> blockTimestamp = await GetBlockTimestampsAsync(rpcEndpointUrl, blockNumbers);
            var transactionInfos = await GetTransactionsAsync(rpcEndpointUrl, txHashes);
            Dictionary<BigInteger, List<Tuple<string, string>>> txns = new Dictionary<BigInteger, List<Tuple<string, string>>>();

            foreach (var e in submissionEvents.Result)
            {
                var txnId = e.Event.TransactionId;
                txns[txnId] = new List<Tuple<string, string>>();
            }

            return new Tuple<BigInteger, Dictionary<BigInteger, List<Tuple<string, string>>>>(required, txns);
        }
        public static async Task<Tuple<BigInteger, List<Tuple<BigInteger, string, bool, string, DateTime, BigInteger>>>> GetMultiSigConfirmations(string rpcEndpointUrl, string multiSigWalletAddress, BigInteger? txnId = null, BlockParameter fromBlock = null, BlockParameter toBlock = null)
        {
            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(rpcEndpointUrl);
            var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
            var requiredSignature = multiSigWallet.RequiredQueryAsync();
            var confirmationEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.ConfirmationEventDTO>(multiSigWalletAddress);
            var filterAllConfirmationEvent = confirmationEventHandler.CreateFilterInput((string)null, txnId, fromBlock, toBlock);
            var confirmationEvents = confirmationEventHandler.GetAllChangesAsync(filterAllConfirmationEvent);

            var revocationEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.RevocationEventDTO>(multiSigWalletAddress);
            var filterAllRevocationEvent = revocationEventHandler.CreateFilterInput((string)null, txnId, fromBlock, toBlock);
            var revocationEvents = revocationEventHandler.GetAllChangesAsync(filterAllRevocationEvent);

            var tasks = new List<Task>() { 
                requiredSignature,
                confirmationEvents,
                revocationEvents,
            };
            Dictionary<BigInteger, DateTime> blocks = new Dictionary<BigInteger, DateTime>();
            HashSet<string> txHashes = new HashSet<string>();

            await Task.WhenAll(tasks);
            foreach (var x in confirmationEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }
            foreach (var x in revocationEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }

            var roEthereum = new RO.Common3.Ethereum.Ethereum();
            var blockNumbers = blocks.Keys.ToArray();
            var lastBlockNumber = blockNumbers.Length > 0 ? blockNumbers.Max() : 0;
            Dictionary<BigInteger, DateTime> blockTimestamp = await GetBlockTimestampsAsync(rpcEndpointUrl, blockNumbers);
            var transactionInfos = await GetTransactionsAsync(rpcEndpointUrl, txHashes);
            List<Tuple<BigInteger, string, bool, string, DateTime, BigInteger>> txns = new List<Tuple<BigInteger, string, bool, string, DateTime, BigInteger>>();

            foreach (var e in confirmationEvents.Result)
            {
                var _txnId = e.Event.TransactionId;
                var confirmedBy = e.Event.Sender;
                var txHash = e.Log.TransactionHash;
                var blockNumber = e.Log.BlockNumber;
                txns.Add(new Tuple<BigInteger, string, bool, string, DateTime, BigInteger>(_txnId, confirmedBy, true, txHash, blockTimestamp[blockNumber], blockNumber));
            }
            foreach (var e in revocationEvents.Result)
            {
                var _txnId = e.Event.TransactionId;
                var confirmedBy = e.Event.Sender;
                var txHash = e.Log.TransactionHash;
                var blockNumber = e.Log.BlockNumber;
                txns.Add(new Tuple<BigInteger, string, bool, string, DateTime, BigInteger>(_txnId, confirmedBy, false, txHash, blockTimestamp[blockNumber], blockNumber));
            }
            return new Tuple<BigInteger, List<Tuple<BigInteger, string, bool, string, DateTime, BigInteger>>>(requiredSignature.Result, txns.ToList());
        }
        public static async Task<List<Tuple<BigInteger, bool, string, DateTime, BigInteger>>> GetMultiSigExecutions(string rpcEndpointUrl, string multiSigWalletAddress, BigInteger? txnId = null, BlockParameter fromBlock = null, BlockParameter toBlock = null)
        {
            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(rpcEndpointUrl);

            var executionEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.ExecutionEventDTO>(multiSigWalletAddress);
            var filterAllExecutionEvent = executionEventHandler.CreateFilterInput(txnId, fromBlock, toBlock);
            var executionEvents = executionEventHandler.GetAllChangesAsync(filterAllExecutionEvent);

            var executionFailureEventHandler = web3.Eth.GetEvent<Contract.Contracts.MultiSigWallet.ContractDefinition.ExecutionFailureEventDTO>(multiSigWalletAddress);
            var filterAllexecutionFailureEvent = executionFailureEventHandler.CreateFilterInput(txnId, fromBlock, toBlock);
            var executionFailureEvents = executionFailureEventHandler.GetAllChangesAsync(filterAllexecutionFailureEvent);

            var tasks = new List<Task>() { 
                executionEvents,
                executionFailureEvents,
            };
            Dictionary<BigInteger, DateTime> blocks = new Dictionary<BigInteger, DateTime>();
            HashSet<string> txHashes = new HashSet<string>();

            await Task.WhenAll(tasks);
            foreach (var x in executionEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }
            foreach (var x in executionFailureEvents.Result)
            {
                blocks[x.Log.BlockNumber] = DateTime.MinValue;
            }

            var roEthereum = new RO.Common3.Ethereum.Ethereum();
            var blockNumbers = blocks.Keys.ToArray();
            var lastBlockNumber = blockNumbers.Length > 0 ? blockNumbers.Max() : 0;
            Dictionary<BigInteger, DateTime> blockTimestamp = await GetBlockTimestampsAsync(rpcEndpointUrl, blockNumbers);
            var transactionInfos = await GetTransactionsAsync(rpcEndpointUrl, txHashes);
            List<Tuple<BigInteger, bool, string, DateTime, BigInteger>> txns = new List<Tuple<BigInteger, bool, string, DateTime, BigInteger>>();
            var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);

            // always failure first
            foreach (var e in executionFailureEvents.Result)
            {
                var _txnId = e.Event.TransactionId;
                var txHash = e.Log.TransactionHash;
                var blockNumber = e.Log.BlockNumber;
                txns.Add(new Tuple<BigInteger, bool, string, DateTime, BigInteger>(_txnId, false, txHash, blockTimestamp[blockNumber], blockNumber));
            }
            foreach (var e in executionEvents.Result)
            {
                var _txnId = e.Event.TransactionId;
                var txHash = e.Log.TransactionHash;
                var blockNumber = e.Log.BlockNumber;
                txns.Add(new Tuple<BigInteger, bool, string, DateTime, BigInteger>(_txnId, true, txHash, blockTimestamp[blockNumber], blockNumber));
            }
            return txns.ToList();
        }

        public static async Task<string> SubmitMultiSigTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, string destionationAddress, string hexData, EthTxOptions txOptions, bool allowPending = false)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                if (!allowPending)
                {
                    var currentTxn = await multiSigWallet.GetTransactionIdQueryAsync(
                        destionationAddress,
                        string.IsNullOrEmpty(hexData) ? null : Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.HexToByteArray(hexData));
                    var hasPendingTx = currentTxn.TransactionId > 0 || currentTxn.IsTransaction;
                    if (hasPendingTx)
                    {
                        throw new Exception(string.Format("there is already a pending multisig txn({0} for the same params({1}, {2})", currentTxn.TransactionId, destionationAddress, hexData));
                    }
                }
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));
                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction()
                    {
                        Destination = destionationAddress,
                        Data = string.IsNullOrEmpty(hexData) ? null : Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.HexToByteArray(hexData),
                    }, web3);
                var submitTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction>();
                var gas = await submitTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 2; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.SubmitTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> ConfirmMultiSigTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, BigInteger txnId, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));
                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.ConfirmTransactionFunction()
                    {
                        TransactionId = txnId
                    }, web3);
                var confirmTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.ConfirmTransactionFunction>();
                var gas = await confirmTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.ConfirmTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> RevokeMultiSigTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, BigInteger txnId, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));
                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));
                var txn = await multiSigWallet.TransactionsQueryAsync(txnId);
                if (txn.Executed) throw new Exception(string.Format("{0} of MultiSigWallet {1} has already been executed", txnId, multiSigWalletAddress));
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.RevokeConfirmationFunction()
                    {
                        TransactionId = txnId
                    }, web3);
                var revokeTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.RevokeConfirmationFunction>();
                var gas = await revokeTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.RevokeConfirmationRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> SubmitNewMultiSigTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, string destinationAddress, BigInteger value, string data, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));
                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction()
                    {
                        Destination = destinationAddress,
                        Value = value,
                        Data = string.IsNullOrEmpty(data) ? null : data.HexToByteArray()
                    }, web3);
                var submitTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction>();
                var gas = await submitTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.SubmitTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> AddMultiSigOwnerTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, string ownerAddress, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                byte[] txData = (new Contract.Contracts.MultiSigWallet.ContractDefinition.AddOwnerFunction() { Owner = ownerAddress }).GetCallData();
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));
                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction()
                    {
                        Destination = multiSigWalletAddress,
                        Value = 0,
                        Data = txData
                    }, web3);
                var submitTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction>();
                var gas = await submitTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.SubmitTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> RemoveMultiSigOwnerTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, string ownerAddress, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.RemoveOwnerFunction() { Owner = ownerAddress }.GetCallData();
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));
                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction()
                    {
                        Destination = multiSigWalletAddress,
                        Value = 0,
                        Data = txData
                    }, web3);
                var submitTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction>();
                var gas = await submitTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.SubmitTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> ReplaceMultiSigOwnerTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, string oldOwnerAddress, string newOwnerAddress, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.ReplaceOwnerFunction() { FromAddress = oldOwnerAddress, NewOwner = newOwnerAddress }.GetCallData();
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));
                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction()
                    {
                        Destination = multiSigWalletAddress,
                        Value = 0,
                        Data = txData
                    }, web3);
                var submitTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction>();
                var gas = await submitTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.SubmitTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> ChangeMultiSigRequirementTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, int requiredSigCount, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.ChangeRequirementFunction() { Required = requiredSigCount }.GetCallData();
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));
                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction()
                    {
                        Destination = multiSigWalletAddress,
                        Value = 0,
                        Data = txData
                    }, web3);
                var submitTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction>();
                var gas = await submitTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.SubmitTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> ExecuteMultiSigTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, BigInteger txnId, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));

                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));

                var txn = await multiSigWallet.TransactionsQueryAsync(txnId);
                if (txn.Executed) throw new Exception(string.Format("{0} of MultiSigWallet {1} has already been executed", txnId, multiSigWalletAddress));
                var isConfirmed = await multiSigWallet.IsConfirmedQueryAsync(txnId);
                var hasConfirmed = await multiSigWallet.ConfirmationsQueryAsync(txnId, fromAddress);
                if (!hasConfirmed) return await ConfirmMultiSigTx(web3, multiSigWalletAddress, txnId, txOptions);
                if (!isConfirmed)
                {
                    throw new Exception(string.Format("{0} of MultiSigWallet {1} need other signer(s) to confirm, {2} has already confirmed it", txnId, multiSigWalletAddress, fromAddress));
                }
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.ExecuteTransactionFunction()
                    {
                        TransactionId = txnId
                    }, web3);
                var confirmTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.ExecuteTransactionFunction>();
                var gas = await confirmTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.ExecuteTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> RunMultiSigTx(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, BigInteger txnId, EthTxOptions txOptions)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                var fromAddress = txOptions.fromAccount.myAddress;
                if (string.IsNullOrEmpty(fromAddress)) throw new Exception(string.Format("no from address specified for MultiSigWallet {1} tx", fromAddress, multiSigWalletAddress));

                var isOwner = await multiSigWallet.IsOwnerQueryAsync(fromAddress);
                if (!isOwner) throw new Exception(string.Format("{0} is not owner of MultiSigWallet {1}", fromAddress, multiSigWalletAddress));

                var txn = await multiSigWallet.TransactionsQueryAsync(txnId);
                if (txn.Executed) throw new Exception(string.Format("{0} of MultiSigWallet {1} has already been executed", txnId, multiSigWalletAddress));
                var isConfirmed = await multiSigWallet.IsConfirmedQueryAsync(txnId);
                var hasConfirmed = await multiSigWallet.ConfirmationsQueryAsync(txnId, fromAddress);
                if (!hasConfirmed) return await ConfirmMultiSigTx(web3, multiSigWalletAddress, txnId, txOptions);
                if (!isConfirmed)
                {
                    throw new Exception(string.Format("{0} of MultiSigWallet {1} need other signer(s) to confirm, {2} has already confirmed it", txnId, multiSigWalletAddress, fromAddress));
                }
                var callParams = txOptions.ApplyTo(
                    new Contract.Contracts.MultiSigWallet.ContractDefinition.RunTransactionFunction
                    {
                        TransactionId = txnId
                    }, web3);
                var confirmTransaction = web3.Eth.GetContractTransactionHandler<Contract.Contracts.MultiSigWallet.ContractDefinition.RunTransactionFunction>();
                var gas = await confirmTransaction.EstimateGasAsync(multiSigWalletAddress, callParams);
                callParams.Gas = gas.Value * 4; // estimate gas can be wrong due to the try/catch double that
                string txHash = await multiSigWallet.RunTransactionRequestAsync(callParams);
                return txHash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Tuple<string, BigInteger, string, bool> MakeMultiSigTxData(string multiSigWalletAddress, string functionName, string txParams)
        {
            string[] _txParams = txParams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

            Func<string, BigInteger, byte[], bool, Tuple<string, BigInteger, string, bool>> submitTrans = (destination, value, data, isCall) =>
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.SubmitTransactionFunction()
                {
                    Destination = destination,
                    Value = value,
                    Data = data
                }.GetCallData();
                return new Tuple<string, BigInteger, string, bool>(multiSigWalletAddress, value, txData.ToHex(true), isCall);
                //return Newtonsoft.Json.JsonConvert.SerializeObject(new
                //{
                //    from = from,
                //    to = multiSigWalletAddress,
                //    data = txData.ToHex(true),
                //    value = value
                //});
            };
            if (functionName == "submitTransaction")
            {
                return submitTrans(_txParams[0].ToString(), System.Numerics.BigInteger.Parse(_txParams[1]), _txParams[2].HexToByteArray(), false);
            }
            else if (functionName == "confirmTransaction")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.ConfirmTransactionFunction
                {
                    TransactionId = System.Numerics.BigInteger.Parse(_txParams[0])
                }.GetCallData();

                return submitTrans(multiSigWalletAddress, 0, txData, false);
            }
            else if (functionName == "revokeConfirmation")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.RevokeConfirmationFunction
                {
                    TransactionId = System.Numerics.BigInteger.Parse(_txParams[0])
                }.GetCallData();

                return submitTrans(multiSigWalletAddress, 0, txData, false);
            }
            else if (functionName == "runTransaction")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.RunTransactionFunction
                {
                    TransactionId = System.Numerics.BigInteger.Parse(_txParams[0])
                }.GetCallData();

                return submitTrans(multiSigWalletAddress, 0, txData, false);
            }
            else if (functionName == "addOwner")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.AddOwnerFunction
                {
                    Owner = _txParams[0]
                }.GetCallData();

                return submitTrans(multiSigWalletAddress, 0, txData, false);
            }
            else if (functionName == "removeOwner")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.RemoveOwnerFunction
                {
                    Owner = _txParams[0]
                }.GetCallData();
                return submitTrans(multiSigWalletAddress, 0, txData, false);
            }
            else if (functionName == "replaceOwner")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.ReplaceOwnerFunction
                {
                    Owner = _txParams[0],
                    NewOwner = _txParams[1]
                }.GetCallData();
                return submitTrans(multiSigWalletAddress, 0, txData, false);
            }
            else if (functionName == "changeRequirement")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.ChangeRequirementFunction
                {
                    Required = int.Parse(_txParams[0])
                }.GetCallData();
                return submitTrans(multiSigWalletAddress, 0, txData, false);
            }
            else if (functionName == "getOwners")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.GetOwnersFunction
                {
                }.GetCallData();
                return submitTrans(multiSigWalletAddress, 0, txData, true);
            }
            else if (functionName == "required")
            {
                byte[] txData = new Contract.Contracts.MultiSigWallet.ContractDefinition.RequiredFunction
                {
                }.GetCallData();
                return submitTrans(multiSigWalletAddress, 0, txData, true);
            }

            throw new Exception(string.Format("not a supported multi-sig transaction {0}", functionName));
        }
        public static async Task<Tuple<List<string>, int>> GetMultiSigSetup(Nethereum.Web3.Web3 web3, string multiSigWalletAddress)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                var owners = multiSigWallet.GetOwnersQueryAsync();
                var required = multiSigWallet.RequiredQueryAsync();
                await Task.WhenAll(new Task[] {
                    owners, required
                });
                return new Tuple<List<string>, int>(owners.Result, int.Parse(required.Result.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static async Task<List<string>> GetMultiSigConfirmedAddress(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, BigInteger txnId)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                return await multiSigWallet.GetConfirmationsQueryAsync(txnId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Tuple<string, BigInteger, string, bool>> GetMultiSigTransaction(Nethereum.Web3.Web3 web3, string multiSigWalletAddress, BigInteger txnId)
        {
            try
            {
                var multiSigWallet = new Contract.Contracts.MultiSigWallet.MultiSigWalletService(web3, multiSigWalletAddress);
                var tx = await multiSigWallet.TransactionsQueryAsync(txnId);
                return new Tuple<string, BigInteger, string, bool>(tx.Destination, tx.Value, tx.Data.ToHex(true), tx.Executed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Tuple<string, string> GetMultiSigContractInfo()
        {
            var byteCode = Contract.Contracts.MultiSigWallet.ContractDefinition.MultiSigWalletDeployment.BYTECODE;
            var abi = Contract.Contracts.MultiSigWallet.MultiSigWalletService.ABI;
            return new Tuple<string, string>(abi, byteCode);
        }

        #endregion 

    }


    [Event("Transfer")]
    public class TransferEventDTO : IEventDTO
    {
        [Parameter("address", "_from", 1, true)]
        public string From { get; set; }

        [Parameter("address", "_to", 2, true)]
        public string To { get; set; }

        [Parameter("uint256", "_value", 3, false)]
        public BigInteger Value { get; set; }
    }

}
