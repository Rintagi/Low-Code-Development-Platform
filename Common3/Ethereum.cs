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
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.FunctionEncoding.AttributeEncoding;
using System.Reflection;
using System.Reflection.Emit;
using Nethereum.Hex.HexConvertors;
using Nethereum.Hex.HexTypes;
using Nethereum.Signer;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Infrastructure;
using Nethereum.RPC.Eth.DTOs;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

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
    #endregion

    #region web3 JSOONRPC custom DTO
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
    #endregion

    #region custom web3 JSONRPC calls
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
                            FieldInfo type = a.GetType().GetField("ElementType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                            Nethereum.ABI.TupleType elementType = type.GetValue(a) as Nethereum.ABI.TupleType;
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
            foreach (var parameterResult in parameterResults)
            {
                var parameter = (ParameterOutputProperty)parameterResult;
                var propertyInfo = parameter.PropertyInfo;
                var abiType = parameter.Parameter.Type;
                var fieldName = string.IsNullOrEmpty(parameter.Parameter.Name) || !useAbiFieldName ? "p" + (idx).ToString() : parameter.Parameter.Name;
                Func<List<Nethereum.ABI.FunctionEncoding.ParameterOutput>, Dictionary<string, object>> decodeTuple = null;
                decodeTuple = ((List<Nethereum.ABI.FunctionEncoding.ParameterOutput> results) =>
                {
                    Dictionary<string, object> o = new Dictionary<string, object>();
                    foreach (var v in results)
                    {
                        if (v.Parameter.ABIType is Nethereum.ABI.TupleType)
                        {
                            // FIXME, deep tuple is not handled
                            //o[v.Parameter.Name] = decodeTuple((v.Parameter.ABIType as Nethereum.ABI.TupleType).Components);
                            o[v.Parameter.Name] = v.Result;
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
        private static byte[] salt = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Replace("-", "")));
        private static Dictionary<string, string> cachedEthereumInfo = new Dictionary<string, string>();
        public int EthCallGas { get; set; }
        public EthereumAccount nethereumAccount { get; private set; }
        public BigInteger gasBoost(BigInteger gasNeeded, bool hardLimit = false)
        {
            // newer version of geth can be very wrong about estimate, double it
            var gasBoosted = gasNeeded + (hardLimit ? (0) : gasNeeded / 1);
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

            if (constant || payable) return abiJSON;

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
        public static string CreateEth2Validators(string depositEXE, string resultKeyFolder, string keyStorePassword, int validatorCount, string eth2Chain)
        {
            string mnemonic = "";
            bool waitingMnemonic = false;

            Action<string, Process, StreamWriter, string> depositPromptHandler = (v, proc, ws, src) =>
            {
                string x = v;
                if (v == "This is your seed phrase. Write it down and store it safely, it is the ONLY way to retrieve your deposit.")
                {
                    waitingMnemonic = true;
                }
                else if (waitingMnemonic && string.IsNullOrEmpty(mnemonic) && !string.IsNullOrEmpty(v))
                {
                    mnemonic = v;
                    waitingMnemonic = false;
                }
                else if (!string.IsNullOrEmpty(mnemonic) && !string.IsNullOrEmpty(v) && v == "Please type your mnemonic (separated by spaces) to confirm you have written it down")
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
                //ws.WriteLine("");
            };
            Action<object, EventArgs> exitHandler = (v, ws) =>
            {
                var x = v;
                //ws.WriteLine("");
            };

            var result = RO.Common3.Utils.WinProcEx(depositEXE, resultKeyFolder, null, "new-mnemonic --chain " + eth2Chain + " --keystore_password " + keyStorePassword + "  --mnemonic_language english --num_validators " + validatorCount.ToString() + " --folder .", depositPromptHandler, stdErrHandler, exitHandler);
            return mnemonic;
        }
        public static string CreateEth2Validators(string depositEXE, string resultKeyFolder, string mnemonic, string keyStorePassword, int startFrom, int validatorCount, string eth2Chain)
        {
            Action<StreamWriter, Process> initHandler = (ws, proc) =>
            {
                ws.WriteLine(mnemonic);
            };
            Action<string, Process, StreamWriter, string> depositPromptHandler = (v, proc, ws, src) =>
            {
                string x = v;
                if (v == "Please enter your mnemonic separated by spaces (\" \"):")
                {
                    //ws.WriteLine(mnemonic);
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
                //ws.WriteLine("");
            };
            Action<object, EventArgs> exitHandler = (v, ws) =>
            {
                var x = v;
                //ws.WriteLine("");
            };

            var result = RO.Common3.Utils.WinProcEx(depositEXE, resultKeyFolder, null, "existing-mnemonic --chain " + eth2Chain + " --keystore_password " + keyStorePassword + " --validator_start_index " + startFrom.ToString() + " --num_validators " + validatorCount.ToString() + " --folder .", depositPromptHandler, stdErrHandler, exitHandler, initHandler);
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

        public Ethereum(int callGas = 4000000, string from = null)
        {
            nethereumAccount = new EthereumAccount();
            EthCallGas = callGas;
            if (from != null) SetGethManagedAccount(from);
        }

        private bool IsIpcClient(Nethereum.Web3.Web3 web3)
        {
            return web3.Client.GetType() == typeof(Nethereum.JsonRpc.IpcClient.IpcClient);
        }
        public async Task<bool> UnlockAccount(Nethereum.Web3.Web3 web3, string address, string password, int? durationInSeconds = 300)
        {
            bool isUnLocked = false;
            Exception err = null;
            for (int ii = 0; ii < 2; ii = ii + 1)
            {
                try
                {
                    isUnLocked = await this.IsAccountUnlocked(web3, address);
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

        public async Task<bool> LockAccount(Nethereum.Web3.Web3 web3, string address)
        {

            return await web3.Personal.LockAccount.SendRequestAsync(address);
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
        public async Task<bool> IsAccountUnlocked(Nethereum.Web3.Web3 web3, string address)
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

        public async Task<bool> IsMining(Nethereum.Web3.Web3 web3)
        {
            var isMining = await web3.Eth.Mining.IsMining.SendRequestAsync();
            return isMining;
        }
        public async Task<bool> StartMiner(Nethereum.Web3.Web3 web3)
        {
            var gethWeb3 = new Nethereum.Geth.Web3Geth(web3.Client);
            return await gethWeb3.Miner.Start.SendRequestAsync();
        }

        public async Task<bool> StopMiner(Nethereum.Web3.Web3 web3)
        {
            var gethWeb3 = new Nethereum.Geth.Web3Geth(web3.Client);
            return await gethWeb3.Miner.Stop.SendRequestAsync();
        }

        public KeyValuePair<string, string> CreateEthereumWallet(string password)
        {
            var service = new KeyStoreService();

            //Creating a new key 
            var ecKey = EthECKey.GenerateKey();
            //We can use EthECKey to generate a new ECKey pair, this is using SecureRandom
            var privateKey = ecKey.GetPrivateKeyAsBytes();
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

        public static string[] CreateHDWallet()
        {
            // this should return a 12 word Mnemonic which is the 'seed' of HD wallet
            return new string[12];
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

        public string SignTransaction(string to, BigInteger ethInWei, BigInteger nounce, BigInteger gasPriceInWei, BigInteger gasLimit, string data, int? chainId = null)
        {
            var signer = new Nethereum.Signer.TransactionSigner();

            if (nethereumAccount == null || nethereumAccount.nethereumAccount == null) throw new Exception("No private key is available");

            if (chainId != null && chainId > 0)
                return signer.SignTransaction(nethereumAccount.nethereumAccount.PrivateKey, new BigInteger(chainId.Value), to, ethInWei, nounce, gasPriceInWei, gasLimit, data);
            else
                return signer.SignTransaction(nethereumAccount.nethereumAccount.PrivateKey, to, ethInWei, nounce, gasPriceInWei, gasLimit, data);
        }

        public async Task<string> SendTransactionAsync(Nethereum.Web3.Web3 web3, string to, BigInteger ethInWei, BigInteger gasPriceInWei, BigInteger gasLimit, string data, int? chainId = null)
        {
            var myAddress = string.IsNullOrEmpty(nethereumAccount.myAddress) ? await GetDefaultAccount(web3) : nethereumAccount.myAddress;
            Nethereum.Hex.HexTypes.HexBigInteger nounce = null;
            int round = 0;
            try
            {
                if (nethereumAccount.gethManagedAccount != null && !string.IsNullOrEmpty(nethereumAccount.gethManagedAccount.Address))
                {
                    var gethAccount = this.nethereumAccount.gethManagedAccount;
                    var txInput = new Nethereum.RPC.Eth.DTOs.TransactionInput(data, to, gethAccount.Address
                        , new Nethereum.Hex.HexTypes.HexBigInteger(gasLimit), new Nethereum.Hex.HexTypes.HexBigInteger(gasPriceInWei), new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei));
                    gethAccount.TransactionManager.Client = web3.Client;
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
                        nounce = await nonceService.GetNextNonceAsync();
                        var newNounce = BigInteger.Add(nounce.Value, round);
                        string signedDataHexString = SignTransaction(to, ethInWei, newNounce, gasPriceInWei, gasLimit, data, chainId);
                        return await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + signedDataHexString);
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
                        var txInput = new Nethereum.RPC.Eth.DTOs.TransactionInput(data, to, myAddress
                            , new Nethereum.Hex.HexTypes.HexBigInteger(gasLimit), new Nethereum.Hex.HexTypes.HexBigInteger(gasPriceInWei), new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei));
                        return await web3.TransactionManager.SendTransactionAsync(txInput);

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
                    throw new EthereumRPCException(x != null ? x.Message : ex.Message + " round(" + round.ToString() + ")" + (nounce != null ? "nounce " + nounce.Value.ToString() : ""), ex);
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

        public async Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, string byteCode, string abiJson, BigInteger ethInWei, BigInteger gasPriceInWei, BigInteger gasLimit, int? chainId, params object[] constructorParams)
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
            var callInput = contractBuilder.BuildTransaction(AbiJSONTranslate(abiJson), byteCode, myAddress, new Nethereum.Hex.HexTypes.HexBigInteger(0), new Nethereum.Hex.HexTypes.HexBigInteger(gasPriceInWei), new Nethereum.Hex.HexTypes.HexBigInteger(ethInWei), TranslateConstructorInput(abiJson, revisedParams));
            try
            {
                var gasNeeded = this.EstimateGasAsync(web3, callInput).Result + 1000; // slightly increment the estimate for better failed transaction diagnostic 
                gasNeeded = gasNeeded < gasLimit ? gasLimit : gasNeeded;
                return await SendTransactionAsync(web3, null, ethInWei, gasPriceInWei, gasNeeded, contractCreationData, chainId);
            }
            catch (Exception ex)
            {
                var x = ex.InnerException;
                if (x != null && x.Message == "authentication needed: password or unlock")
                {
                    throw new EthereumRPCException(x.Message + " " + myAddress);
                }
                else throw;
            }
        }
        public string DeployContract(string web3Endpoint, string byteCode, string abiJson, BigInteger ethInWei, BigInteger gasPriceInWei, BigInteger gasLimit, int? chainId, params object[] constructorParams)
        {
            try
            {
                var txResult = System.Threading.Tasks.Task.Run(async () =>
                {
                    var web3 = Ethereum.GetWeb3Client(web3Endpoint);

                    //"0x" + byteCode

                    var hash = await DeployContractAsync(web3, byteCode, abiJson, ethInWei, gasPriceInWei, gasLimit, chainId, constructorParams);
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
            return await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + signedDataHexString);
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
        public async Task<string> GetTransactionInfoAsync(string txHash, Nethereum.Web3.Web3 web3, bool includeContractByteCode = false)
        {
            var txReceiptRequest = GetTransactionReceiptAsync(txHash, web3);
            var txRequest = GetTransactionAsync(txHash, web3);
            var txReceipt = await txReceiptRequest;
            var tx = await txRequest;

            if (tx == null && txReceipt == null) return null;

            var txInfo = new
            {
                TransactionHash = tx.TransactionHash,
                To = tx.To,
                From = tx.From,
                Value = tx.Value,
                GasPrice = tx.GasPrice.Value,
                GasLimit = tx.Gas.Value,
                Nounce = tx.Nonce.Value,
                GasUsed = txReceipt != null ? txReceipt.GasUsed.Value : (BigInteger?)null,
                BlockNumber = txReceipt != null ? txReceipt.BlockNumber.Value : (BigInteger?)null,
                Status = txReceipt != null ? txReceipt.Status.Value : (BigInteger?)null,
                ContractAddress = txReceipt != null ? txReceipt.ContractAddress : null,
                Input = txReceipt != null && string.IsNullOrEmpty(txReceipt.ContractAddress) ? tx.Input : null,
            };
            var txInfoJson = Newtonsoft.Json.JsonConvert.SerializeObject(txInfo);
            return txInfoJson;
        }

        public string GetTransactionInfo(string txHash, Nethereum.Web3.Web3 web3)
        {
            var result = System.Threading.Tasks.Task.Run(async () =>
            {
                var ret = await GetTransactionInfoAsync(txHash, web3);
                return ret;
            }).Result;
            return result;
        }

        public DateTime GetBlockTimestamp(ulong blockNumber, Nethereum.Web3.Web3 web3)
        {
            var result = System.Threading.Tasks.Task.Run(async () =>
            {
                var ret = await GetBlockWithTransactionsAsync(web3, new BlockParameter(blockNumber));
                return UnixTimeToDateTime(ret.Timestamp.ToUlong());
            }).Result;
            return result;
        }

        public async Task<BigInteger> EstimateGasAsync(Nethereum.Web3.Web3 web3, Nethereum.RPC.Eth.DTOs.CallInput callInput)
        {
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
            var eventLog = await contractEvent.GetFilterChanges<TResult>(filterId);
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
            var contract = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), null);
            Dictionary<string, Nethereum.ABI.Model.EventABI> eventAbi = contract.ContractABI.Events.ToDictionary(abi => abi.Sha3Signature.EnsureHexPrefix(), x => x);
            var eventDecoder = new EventTopicDecoder();
            List<Dictionary<string, object>> output = new List<Dictionary<string, object>>();
            foreach (var log in eventLog)
            {
                var abi = eventAbi[log.Topics[0] as string];
                var memberTypeObject = memberTypeFactory(abi.Sha3Signature, abi.Name);
                var paramAttributes = abi.InputParameters.Select<Nethereum.ABI.Model.Parameter, Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute>(
                    x => new Nethereum.ABI.FunctionEncoding.Attributes.ParameterAttribute(x.ABIType.Name, x.Name, x.Order, x.Indexed));
                var result = eventDecoder.DecodeTopics(log.Topics, log.Data, memberTypeObject, paramAttributes, useAbiFieldName);
                result.Add("_LogInfo", new { EventName = abi.Name, EventSha3Sig = abi.Sha3Signature, BlockNumber = log.BlockNumber.Value, TransactionHash = log.TransactionHash, Address = log.Address });
                output.Add(result);
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
                               where fx.Name == functionName && fx.InputParameters.Length == functParam.Length
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

        public async Task<string> ExecuteAsync(string abiJson, string contractAddress, Nethereum.Web3.Web3 web3, string functionName, BigInteger gasLimit, BigInteger gasPriceInWei, BigInteger ethInWei, object[] functionInput, int? chainId = null, bool hardLimit = false)
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
            return await SendTransactionAsync(web3, contractAddress, ethInWei, gasPriceInWei, finalGasLimit, callData, chainId);
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
        public string ExecuteEthereumFunction(string web3EndPoint, string contractAddress, string contractAbi, string functionName, object[] functParam, BlockParameter block, BigInteger gasPriceInWei, string ethInWeiToSent = "0", int? chainid = null, bool hardLimit = false, int gasLimit = 0, bool useAbiFieldName = false)
        {

            var function = Ethereum.GetFunctionBuilder(contractAbi, functionName);
            var revisedFunctParam = NormalizedFunctParam(function.FunctionABI.InputParameters, functParam);

            string result = null;
            var ethToSent = Ethereum.FromWei(ethInWeiToSent);
            try
            {
                result = System.Threading.Tasks.Task.Run(async () =>
                {
                    var web3 = Ethereum.GetWeb3Client(web3EndPoint);
                    if (function.FunctionABI.Constant)
                    {
                        string ret = await CallAsync(contractAbi, contractAddress, web3, functionName, ethToSent, revisedFunctParam, block);
                        return ret;
                    }
                    else
                    {
                        var txHash = await ExecuteAsync(contractAbi, contractAddress, web3, functionName, new BigInteger(gasLimit), gasPriceInWei, ethToSent, revisedFunctParam, chainid, hardLimit);
                        return txHash;
                    }
                }).Result;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException) throw ex.InnerException;
                else throw;
            }

            if (function.FunctionABI.Constant)
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
        public string ExecuteEthereumFunction(string web3EndPoint, string contractAddress, string contractAbi, string functionName, object[] functParam, BigInteger gasPriceInWei, string ethInWeiToSent = "0", int? chainid = null, bool hardLimit = false, int gasLimit = 0, bool useAbiFieldName = false)
        {
            return ExecuteEthereumFunction(web3EndPoint, contractAddress, contractAbi, functionName, functParam, BlockParameter.CreateLatest(), gasPriceInWei, ethInWeiToSent, chainid, hardLimit, gasLimit, useAbiFieldName);
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

        public async Task<string> SendEtherAsync(string web3EndPoint, string toWalletAddress, BigInteger ethInWei, BigInteger gasPriceInWei, int? chainId = null)
        {
            var web3 = Ethereum.GetWeb3Client(web3EndPoint);
            string txHash = await SendTransactionAsync(web3, toWalletAddress, ethInWei, gasPriceInWei, new BigInteger(21000), null, chainId);
            return txHash;
        }
        public string SendEther(string web3EndPoint, string toWalletAddress, BigInteger ethInWei, BigInteger gasPriceInWei, int? chainId = null)
        {

            try
            {
                var result = System.Threading.Tasks.Task.Run(async () =>
                {
                    return await SendEtherAsync(web3EndPoint, toWalletAddress, ethInWei, gasPriceInWei, chainId);
                }).Result;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException) throw ex.InnerException;
                else throw;
            }

        }

        public static Nethereum.Web3.Web3 GetWeb3Client(string web3url)
        {
            if (!web3url.StartsWith("http:") && !web3url.StartsWith("https:") && !web3url.StartsWith("ws:") && !web3url.StartsWith("wss:"))
            {
                Nethereum.JsonRpc.IpcClient.IpcClient ipcClient = new Nethereum.JsonRpc.IpcClient.IpcClient((web3url??"").Trim());
                try
                {
                    var _pipeClient = new System.IO.Pipes.NamedPipeClientStream((web3url ?? "").Trim());
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
                Nethereum.JsonRpc.Client.RpcClient rpcClient = new Nethereum.JsonRpc.Client.RpcClient(new Uri((web3url ?? "").Trim()));
                var web3 = new Nethereum.Web3.Web3(rpcClient);
                return web3;
            }
        }

        public static Nethereum.Geth.Web3Geth GetGEthWeb3Client(string web3url)
        {
            if (!web3url.StartsWith("http:") && !web3url.StartsWith("https:") && !web3url.StartsWith("ws:") && !web3url.StartsWith("wss:"))
            {
                Nethereum.JsonRpc.IpcClient.IpcClient ipcClient = new Nethereum.JsonRpc.IpcClient.IpcClient((web3url ?? "").Trim());
                try
                {
                    var _pipeClient = new System.IO.Pipes.NamedPipeClientStream((web3url ?? "").Trim());
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
                var web3 = new Nethereum.Geth.Web3Geth((web3url ?? "").Trim());
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
        public static Dictionary<string, Nethereum.ABI.Model.FunctionABI> GetFunctionDef(string abiJson)
        {
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            //var sigEncoder = Ethereum.GetSignatureEncoder();
            //var xx = new Nethereum.Util.Sha3Keccack();
            var functionDef = contract.ContractABI.Functions.Select(abi =>
            {
                var sig = abi.Sha3Signature;
                var callParamName = string.Join(",", abi.InputParameters.Select(p => p.ABIType.CanonicalName).ToArray());
                var callInterface = string.Join(",", abi.InputParameters.Select(p => (p.ABIType.CanonicalName + ' ' + p.Name).Trim()).ToArray());
                var returnInterface = string.Join(",", abi.OutputParameters.Select(p =>
                        {
                            string typeName = p.InternalType.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                            Func<Nethereum.ABI.Model.Parameter[], string> decodeTuple = null;
                            decodeTuple = ((Nethereum.ABI.Model.Parameter[] tuple_params) =>
                            {
                                return string.Join(",", tuple_params.Select(pp =>
                                {
                                    if (pp.ABIType is Nethereum.ABI.TupleType)
                                    {
                                        string ppTypeName = pp.InternalType.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                                        Nethereum.ABI.TupleType cc = pp.ABIType as Nethereum.ABI.TupleType;
                                        return (typeName + "(" + decodeTuple(cc.Components) + ")" + ' ' + p.Name).Trim();
                                    }
                                    else
                                        return (pp.ABIType.CanonicalName + ' ' + pp.Name).Trim();
                                }).ToArray());
                            });
                            if (p.ABIType is Nethereum.ABI.TupleType)
                            {
                                Nethereum.ABI.TupleType c = p.ABIType as Nethereum.ABI.TupleType;
                                return (typeName + "(" + decodeTuple(c.Components) + ")" + ' ' + p.Name).Trim();
                            }
                            else if (p.ABIType.Name.StartsWith("tuple["))
                            {
                                // tuple array
                                Nethereum.ABI.ArrayType a = p.ABIType as Nethereum.ABI.ArrayType;
                                FieldInfo type = a.GetType().GetField("ElementType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                                Nethereum.ABI.TupleType elementType = type.GetValue(a) as Nethereum.ABI.TupleType;
                                return (typeName + "(" + decodeTuple(elementType.Components) + ")" + ' ' + p.Name).Trim();
                            }
                            else
                                return (p.ABIType.CanonicalName + ' ' + p.Name).Trim();
                        }
                        ).ToArray());
                /* name format used to calc function sig(i.e. abi.Sha3Signature, keccak256(name).left(4bytes)), per ethereum spec */
                var name = abi.Name + "(" + callParamName + ")";
                /* function definition, for human */
                var docName = abi.Name + "(" + callInterface + ")" + (!string.IsNullOrEmpty(returnInterface) ? " returns " + "(" + returnInterface + ")" : "");
                //var yy = xx.CalculateHash(name).Substring(0, 4 * 2);
                return new
                {
                    functionName = docName,
                    functionAbi = abi,
                };
            }).ToDictionary(f => f.functionName, f => f.functionAbi);
            return functionDef;
        }

        public static Dictionary<string, Nethereum.ABI.Model.ConstructorABI> GetConstructorDef(string className, string abiJson)
        {
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            var abi = contract.ContractABI.Constructor;
            if (abi != null)
            {
                var contractClassName = className;
                var callParamName = string.Join(",", abi.InputParameters.Select(p => p.ABIType.CanonicalName).ToArray());
                var callInterface = string.Join(",", abi.InputParameters.Select(p => (p.ABIType.CanonicalName + ' ' + p.Name).Trim()).ToArray());
                var returnInterface = string.Join(",", abi.InputParameters.Select(p => (p.ABIType.CanonicalName + ' ' + p.Name).Trim()).ToArray());
                var name = contractClassName + "(" + callParamName + ")";
                var docName = contractClassName + "(" + callInterface + ")";
                return new Dictionary<string, Nethereum.ABI.Model.ConstructorABI>() {
                    {docName,abi}
                };
            }
            else
                return new Dictionary<string, Nethereum.ABI.Model.ConstructorABI>() {
                    {className + "()", null}
                };
        }

        public static Dictionary<string, Nethereum.ABI.Model.EventABI> GetEventDef(string abiJson)
        {
            var contract = Ethereum.GetContractBuilder(AbiJSONTranslate(abiJson));
            //var sigEncoder = Ethereum.GetSignatureEncoder();
            //var xx = new Nethereum.Util.Sha3Keccack();
            var eventDef = contract.ContractABI.Events.Select(abi =>
            {
                var sig = abi.Sha3Signature;
                var callParamName = string.Join(",", abi.InputParameters.Select(p => p.ABIType.CanonicalName).ToArray());
                var callInterface = string.Join(",", abi.InputParameters.Select(p => (p.ABIType.CanonicalName + ' ' + (p.Indexed ? "indexed " : "") + p.Name).Trim()).ToArray());
                var returnInterface = string.Join(",", abi.InputParameters.Select(p => (p.ABIType.CanonicalName + ' ' + (p.Indexed ? "indexed" : "") + p.Name).Trim()).ToArray());
                var name = abi.Name + "(" + callParamName + ")";
                var docName = abi.Name + "(" + callInterface + ")";
                //var yy = xx.CalculateHash(name);
                return new
                {
                    eventName = docName,
                    eventAbi = abi
                };
            }).ToDictionary(e => e.eventName, e => e.eventAbi);
            return eventDef;
        }

        public static dynamic GetFunctionInputDTO(string abiJson, string functionName)
        {
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

        public static string decodeFunctionOutput(string contractAbi, string functionName, List<object> functionParams, string result, bool useAbiFieldName)
        {
            dynamic output = GetFunctionOutputDTO(contractAbi, functionName);
            var retVal = DecodeFunctionOutput(contractAbi, result, functionName, functionParams.ToArray(), () => output, useAbiFieldName);
            var retValJson = Newtonsoft.Json.JsonConvert.SerializeObject(retVal);
            return retValJson;
        }

        public static dynamic GetFunctionOutputDTO(string abiJson, string functionName)
        {
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.GetFunctionBuilder(functionName);
            dynamic functionOutputDTO = new System.Dynamic.ExpandoObject();
            var outputParams = functionBuilder.FunctionABI.OutputParameters;
            IDictionary<string, object> eo = functionOutputDTO as IDictionary<string, object>;
            int idx = 0;
            foreach (var p in outputParams)
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
                                        if (f == null) v = null;
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
                else if (inputs[idx] is string
                    && (inputs[idx] as string).StartsWith("0x")
                    && (p.ABIType.IsDynamic() && (p.ABIType.Name == "bytes" || p.ABIType.Name == "bytes1[]"))
                        || p.ABIType.Name == "bytes32"
                        || p.ABIType.Name.StartsWith("byte")
                    )
                {
                    translated.Add(Utils.HexToByteArray(inputs[idx] as string));
                }
                else if (inputs[idx] is string && (p.ABIType.Name.StartsWith("uint") || p.ABIType.Name.StartsWith("int") || p.ABIType.Name.StartsWith("ufixed") || p.ABIType.Name.StartsWith("fixed") || p.ABIType.Name.StartsWith("bytes")))
                {
                    // force uint* into BigInteger if supplied as string
                    System.Numerics.BigInteger x = BigInteger.Parse((p.ABIType.Name.StartsWith("uint") || p.ABIType.Name.StartsWith("ufixed") || p.ABIType.Name.StartsWith("bool") ? "00" : "") + (inputs[idx] as string).Replace("0x", ""), (inputs[idx] as string).StartsWith("0x") ? System.Globalization.NumberStyles.AllowHexSpecifier : 0);

                    translated.Add(x);
                }
                else if (p.ABIType.Name.StartsWith("address") && inputs[idx] is string)
                {
                    translated.Add(string.IsNullOrEmpty(inputs[idx] as string) ? null : inputs[idx]);
                }
                else if (p.ABIType.Name.StartsWith("string"))
                {
                    translated.Add(inputs[idx] == null ? "" : inputs[idx].ToString());
                }
                else if (p.ABIType.Name.StartsWith("bool"))
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
            var contractBuilder = new Nethereum.Contracts.ContractBuilder(AbiJSONTranslate(abiJson), "0x0");
            var functionBuilder = contractBuilder.GetFunctionBuilder(functionName);
            var callData = functionBuilder.GetData(TranslateCallInput(AbiJSONTranslate(abiJson), functionName, functionInput));
            return callData;
        }
        public static Tuple<int, string, string> CompileContract(string codeOrPath, string compilerPath)
        {
            bool isCodePath = File.Exists(codeOrPath);
            string workDirectory = isCodePath ? new FileInfo(codeOrPath).Directory.FullName : "";
            string cmd_arg = "--optimize --combined-json bin,bin-runtime,abi,hashes,interface,metadata,userdoc,devdoc " + (isCodePath ? codeOrPath : "-");
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

        public static DateTime UnixTimeToDateTime(int unixTime)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dateTime.AddSeconds(unixTime);
        }

        public static DateTime UnixTimeToDateTime(ulong unixTime)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dateTime.AddSeconds(unixTime);
        }

        public static int DateTimeToUnixTime(DateTime time)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (time - dateTime).Seconds;
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
                return string.Format("ChainI {4}: Last block {0} - block time {1}, lag by {2} minutes, syncing : {3}, gas Price: {5}",
                    (int)latestBlock.Number.Value,
                    blockTime.ToLocalTime(),
                    (int)timeDiff.TotalMinutes,
                    syncingBlock == null ? "false" : (
                        string.Format("block {0}, block time {1}", syncingBlock[0].Number.Value, UnixTimeToDateTime(int.Parse(syncingBlock[0].Timestamp.Value.ToString())).ToLocalTime()
                    )),
                    chainId,
                    gasPrice
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

    }
}
