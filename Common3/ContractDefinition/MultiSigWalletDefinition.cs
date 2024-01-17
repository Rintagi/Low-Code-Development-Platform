using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Contract.Contracts.MultiSigWallet.ContractDefinition
{


    public partial class MultiSigWalletDeployment : MultiSigWalletDeploymentBase
    {
        public MultiSigWalletDeployment() : base(BYTECODE) { }
        public MultiSigWalletDeployment(string byteCode) : base(byteCode) { }
    }

    public class MultiSigWalletDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x60806040523480156200001157600080fd5b5060405162001d9c38038062001d9c83398101604052805160208201519101805190919060009082603282118015906200004b5750818111155b80156200005757508015155b80156200006357508115155b15156200006f57600080fd5b600092505b845183101562000147576002600086858151811015156200009157fe5b6020908102909101810151600160a060020a031682528101919091526040016000205460ff16158015620000e757508483815181101515620000cf57fe5b90602001906020020151600160a060020a0316600014155b1515620000f357600080fd5b60016002600087868151811015156200010857fe5b602090810291909101810151600160a060020a03168252810191909152604001600020805460ff19169115159190911790556001929092019162000074565b84516200015c9060049060208801906200016e565b50505060059190915550620002029050565b828054828255906000526020600020908101928215620001c6579160200282015b82811115620001c65782518254600160a060020a031916600160a060020a039091161782556020909201916001909101906200018f565b50620001d4929150620001d8565b5090565b620001ff91905b80821115620001d4578054600160a060020a0319168155600101620001df565b90565b611b8a80620002126000396000f3006080604052600436106101485763ffffffff7c0100000000000000000000000000000000000000000000000000000000600035041663025e7c278114610193578063173825d9146101c757806320ea8d86146101e85780632f54bf6e146102005780633411c81c1461023557806354741525146102595780635fc89b901461028a5780637065cb48146102a2578063784547a7146102c35780637dca77a1146102db5780638b51d13f146102f357806399e26e691461030b5780639ace38c214610372578063a0e67e2b1461042d578063a8abe69a14610492578063af8fbed1146104b7578063b5dc40c314610537578063b77bf6001461054f578063ba51a6df14610564578063c01a8c841461057c578063c642747414610594578063d74f8edd146105fd578063dc8452cd14610612578063e20056e614610627578063ee22610b1461064e575b600034111561019157604080513481529051600160a060020a033316917fe1fffcc4923d04b559f4d29a8bfc6cda04eb5b0d3c460751c2402c5c5cc9109c919081900360200190a25b005b34801561019f57600080fd5b506101ab600435610666565b60408051600160a060020a039092168252519081900360200190f35b3480156101d357600080fd5b50610191600160a060020a036004351661068e565b3480156101f457600080fd5b50610191600435610819565b34801561020c57600080fd5b50610221600160a060020a03600435166108ef565b604080519115158252519081900360200190f35b34801561024157600080fd5b50610221600435600160a060020a0360243516610904565b34801561026557600080fd5b5061027860043515156024351515610924565b60408051918252519081900360200190f35b34801561029657600080fd5b50610191600435610990565b3480156102ae57600080fd5b50610191600160a060020a0360043516610b8b565b3480156102cf57600080fd5b50610221600435610cc4565b3480156102e757600080fd5b50610278600435610d48565b3480156102ff57600080fd5b50610278600435610d5a565b34801561031757600080fd5b5060408051602060046024803582810135601f8101859004850286018501909652858552610278958335600160a060020a0316953695604494919390910191908190840183828082843750949750610dc99650505050505050565b34801561037e57600080fd5b5061038a600435610ebd565b6040518085600160a060020a0316600160a060020a031681526020018481526020018060200183151515158152602001828103825284818151815260200191508051906020019080838360005b838110156103ef5781810151838201526020016103d7565b50505050905090810190601f16801561041c5780820380516001836020036101000a031916815260200191505b509550505050505060405180910390f35b34801561043957600080fd5b50610442610f7b565b60408051602080825283518183015283519192839290830191858101910280838360005b8381101561047e578181015183820152602001610466565b505050509050019250505060405180910390f35b34801561049e57600080fd5b5061044260043560243560443515156064351515610fde565b3480156104c357600080fd5b5060408051602060046024803582810135601f810185900485028601850190965285855261051e958335600160a060020a03169536956044949193909101919081908401838280828437509497506111179650505050505050565b6040805192835290151560208301528051918290030190f35b34801561054357600080fd5b50610442600435611250565b34801561055b57600080fd5b506102786113c9565b34801561057057600080fd5b506101916004356113cf565b34801561058857600080fd5b50610191600435611462565b3480156105a057600080fd5b50604080516020600460443581810135601f8101849004840285018401909552848452610278948235600160a060020a03169460248035953695946064949201919081908401838280828437509497506115499650505050505050565b34801561060957600080fd5b50610278611588565b34801561061e57600080fd5b5061027861158d565b34801561063357600080fd5b50610191600160a060020a0360043581169060243516611593565b34801561065a57600080fd5b50610191600435611731565b600480548290811061067457fe5b600091825260209091200154600160a060020a0316905081565b600030600160a060020a031633600160a060020a03161415156106b057600080fd5b600160a060020a038216600090815260026020526040902054829060ff1615156106d957600080fd5b600160a060020a0383166000908152600260205260408120805460ff1916905591505b600454600019018210156107b45782600160a060020a031660048381548110151561072357fe5b600091825260209091200154600160a060020a031614156107a95760048054600019810190811061075057fe5b60009182526020909120015460048054600160a060020a03909216918490811061077657fe5b9060005260206000200160006101000a815481600160a060020a030219169083600160a060020a031602179055506107b4565b6001909101906106fc565b6004805460001901906107c79082611a69565b5060045460055411156107e0576004546107e0906113cf565b604051600160a060020a038416907f8001553a916ef2f495d26a907cc54d96ed840d7bda71e73194bf5a9df7a76b9090600090a2505050565b33600160a060020a03811660009081526002602052604090205460ff16151561084157600080fd5b600082815260016020908152604080832033600160a060020a038116855292529091205483919060ff16151561087657600080fd5b600084815260208190526040902060030154849060ff161561089757600080fd5b6000858152600160209081526040808320600160a060020a0333168085529252808320805460ff191690555187927ff6a317157440607f36269043eb55f1287a5a19ba2216afeab88cd46cbcfb88e991a35050505050565b60026020526000908152604090205460ff1681565b600160209081526000928352604080842090915290825290205460ff1681565b6000805b60065481101561098957838015610951575060008181526020819052604090206003015460ff16155b806109755750828015610975575060008181526020819052604090206003015460ff165b15610981576001820191505b600101610928565b5092915050565b600081815260208190526040812060030154829060ff16156109b157600080fd5b826109bb81610cc4565b15156109c657600080fd5b60008481526020818152604080832060038082018054600160ff19909116811790915582546002808501805487516101009582161595909502600019011691909104601f8101889004880284018801909652858352939950919594610a8f94600160a060020a0390931693919290830182828015610a855780601f10610a5a57610100808354040283529160200191610a85565b820191906000526020600020905b815481529060010190602001808311610a6857829003601f168201915b5050505050610dc9565b815260208082019290925260409081016000908120558454600180870154600280890180548651601f95821615610100026000190190911692909204938401879004870282018701909552828152610b4f95600160a060020a039094169491939091908390830182828015610b455780601f10610b1a57610100808354040283529160200191610b45565b820191906000526020600020905b815481529060010190602001808311610b2857829003601f168201915b5050505050611956565b1515610b5a57600080fd5b60405184907f33e13ecb54c3076d8e8bb8c2881800a4d972b792045ffae98fdf46df365fed7590600090a250505050565b30600160a060020a031633600160a060020a0316141515610bab57600080fd5b600160a060020a038116600090815260026020526040902054819060ff1615610bd357600080fd5b81600160a060020a0381161515610be957600080fd5b60048054905060010160055460328211158015610c065750818111155b8015610c1157508015155b8015610c1c57508115155b1515610c2757600080fd5b600160a060020a038516600081815260026020526040808220805460ff1916600190811790915560048054918201815583527f8a35acfbc15ff81a39ae7d344fd709f28e8600b4aa8c65c6b64bfe7fe36bd19b01805473ffffffffffffffffffffffffffffffffffffffff191684179055517ff39e6e1eb0edcf53c221607b54b00cd28f3196fed0a24994dc308b8f611b682d9190a25050505050565b600080805b600454811015610d415760008481526001602052604081206004805491929184908110610cf257fe5b6000918252602080832090910154600160a060020a0316835282019290925260400190205460ff1615610d26576001820191505b600554821415610d395760019250610d41565b600101610cc9565b5050919050565b60036020526000908152604090205481565b6000805b600454811015610dc35760008381526001602052604081206004805491929184908110610d8757fe5b6000918252602080832090910154600160a060020a0316835282019290925260400190205460ff1615610dbb576001820191505b600101610d5e565b50919050565b600082826040516020018083600160a060020a0316600160a060020a03166c0100000000000000000000000002815260140182805190602001908083835b60208310610e265780518252601f199092019160209182019101610e07565b6001836020036101000a038019825116818451168082178552505050505050905001925050506040516020818303038152906040526040518082805190602001908083835b60208310610e8a5780518252601f199092019160209182019101610e6b565b5181516020939093036101000a600019018019909116921691909117905260405192018290039091209695505050505050565b6000602081815291815260409081902080546001808301546002808501805487516101009582161595909502600019011691909104601f8101889004880284018801909652858352600160a060020a0390931695909491929190830182828015610f685780601f10610f3d57610100808354040283529160200191610f68565b820191906000526020600020905b815481529060010190602001808311610f4b57829003601f168201915b5050506003909301549192505060ff1684565b60606004805480602002602001604051908101604052809291908181526020018280548015610fd357602002820191906000526020600020905b8154600160a060020a03168152600190910190602001808311610fb5575b505050505090505b90565b606080600080600654604051908082528060200260200182016040528015611010578160200160208202803883390190505b50925060009150600090505b60065481101561109757858015611045575060008181526020819052604090206003015460ff16155b806110695750848015611069575060008181526020819052604090206003015460ff165b1561108f5780838381518110151561107d57fe5b60209081029091010152600191909101905b60010161101c565b8787036040519080825280602002602001820160405280156110c3578160200160208202803883390190505b5093508790505b8681101561110c5782818151811015156110e057fe5b90602001906020020151848983038151811015156110fa57fe5b602090810290910101526001016110ca565b505050949350505050565b6000806000611124611a92565b60065415156111395760009350839250611247565b6111438686610dc9565b6000818152600360209081526040808320548084528383529281902081516080810183528154600160a060020a0316815260018083015482860152600280840180548651601f948216156101000260001901909116929092049283018790048702820187018652828252969b50969850909591949286019391929183018282801561120f5780601f106111e45761010080835404028352916020019161120f565b820191906000526020600020905b8154815290600101906020018083116111f257829003601f168201915b50505091835250506003919091015460ff161515602090910152805160408201519192508591849161124091610dc9565b9195501492505b50509250929050565b606080600080600480549050604051908082528060200260200182016040528015611285578160200160208202803883390190505b50925060009150600090505b60045481101561134257600085815260016020526040812060048054919291849081106112ba57fe5b6000918252602080832090910154600160a060020a0316835282019290925260400190205460ff161561133a5760048054829081106112f557fe5b6000918252602090912001548351600160a060020a039091169084908490811061131b57fe5b600160a060020a03909216602092830290910190910152600191909101905b600101611291565b8160405190808252806020026020018201604052801561136c578160200160208202803883390190505b509350600090505b818110156113c157828181518110151561138a57fe5b9060200190602002015184828151811015156113a257fe5b600160a060020a03909216602092830290910190910152600101611374565b505050919050565b60065481565b30600160a060020a031633600160a060020a03161415156113ef57600080fd5b60045481603282118015906114045750818111155b801561140f57508015155b801561141a57508115155b151561142557600080fd5b60058390556040805184815290517fa3f1ee9126a074d9326c682f561767f710e927faa811f7a99829d49dc421797a9181900360200190a1505050565b33600160a060020a03811660009081526002602052604090205460ff16151561148a57600080fd5b6000828152602081905260409020548290600160a060020a031615156114af57600080fd5b600083815260016020908152604080832033600160a060020a038116855292529091205484919060ff16156114e357600080fd5b6000858152600160208181526040808420600160a060020a0333168086529252808420805460ff1916909317909255905187927f4a504a94899432a9846e1aa406dceb1bcfd538bb839071d49d1e5e23f5be30ef91a361154285611731565b5050505050565b6000611556848484611979565b905080600360006115678786610dc9565b815260208101919091526040016000205561158181611462565b9392505050565b603281565b60055481565b600030600160a060020a031633600160a060020a03161415156115b557600080fd5b600160a060020a038316600090815260026020526040902054839060ff1615156115de57600080fd5b600160a060020a038316600090815260026020526040902054839060ff161561160657600080fd5b600092505b6004548310156116975784600160a060020a031660048481548110151561162e57fe5b600091825260209091200154600160a060020a0316141561168c578360048481548110151561165957fe5b9060005260206000200160006101000a815481600160a060020a030219169083600160a060020a03160217905550611697565b60019092019161160b565b600160a060020a03808616600081815260026020526040808220805460ff1990811690915593881682528082208054909416600117909355915190917f8001553a916ef2f495d26a907cc54d96ed840d7bda71e73194bf5a9df7a76b9091a2604051600160a060020a038516907ff39e6e1eb0edcf53c221607b54b00cd28f3196fed0a24994dc308b8f611b682d90600090a25050505050565b33600160a060020a03811660009081526002602052604081205490919060ff16151561175c57600080fd5b600083815260016020908152604080832033600160a060020a038116855292529091205484919060ff16151561179157600080fd5b600085815260208190526040902060030154859060ff16156117b257600080fd5b6117bb86610cc4565b1561194e576000868152602081815260409182902060038101805460ff19166001908117909155815481830154600280850180548851601f60001997831615610100029790970190911692909204948501879004870282018701909752838152939a5061185a95600160a060020a0390921694909391908390830182828015610b455780601f10610b1a57610100808354040283529160200191610b45565b156119165760405186907f33e13ecb54c3076d8e8bb8c2881800a4d972b792045ffae98fdf46df365fed7590600090a2845460028087018054604080516020601f60001960018616156101000201909416959095049283018590048502810185019091528181526003946000946118fe94600160a060020a039092169390830182828015610a855780601f10610a5a57610100808354040283529160200191610a85565b8152602081019190915260400160009081205561194e565b60405186907f526441bb6c1aba3c9a4a6ca1d6545da9c2333c8c48343ef398eb858d72b7923690600090a260038501805460ff191690555b505050505050565b6000806040516020840160008287838a8c6187965a03f198975050505050505050565b600083600160a060020a038116151561199157600080fd5b60065460408051608081018252600160a060020a0388811682526020808301898152838501898152600060608601819052878152808452959095208451815473ffffffffffffffffffffffffffffffffffffffff191694169390931783555160018301559251805194965091939092611a11926002850192910190611ac6565b50606091909101516003909101805460ff191691151591909117905560068054600101905560405182907fc0ba8fe4b176c1714197d43b9cc6bcf797a4a7461c5fe8d0ef6e184ae7601e5190600090a2509392505050565b815481835581811115611a8d57600083815260209020611a8d918101908301611b44565b505050565b6080604051908101604052806000600160a060020a0316815260200160008152602001606081526020016000151581525090565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f10611b0757805160ff1916838001178555611b34565b82800160010185558215611b34579182015b82811115611b34578251825591602001919060010190611b19565b50611b40929150611b44565b5090565b610fdb91905b80821115611b405760008155600101611b4a5600a165627a7a7230582063ed013ffd45c326dac2f35e47b6e996fa22b83a9a7235f13706d8ad38eb310d0029";
        public MultiSigWalletDeploymentBase() : base(BYTECODE) { }
        public MultiSigWalletDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address[]", "_owners", 1)]
        public virtual List<string> Owners { get; set; }
        [Parameter("uint256", "_required", 2)]
        public virtual BigInteger Required { get; set; }
    }

    public partial class OwnersFunction : OwnersFunctionBase { }

    [Function("owners", "address")]
    public class OwnersFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class RemoveOwnerFunction : RemoveOwnerFunctionBase { }

    [Function("removeOwner")]
    public class RemoveOwnerFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class RevokeConfirmationFunction : RevokeConfirmationFunctionBase { }

    [Function("revokeConfirmation")]
    public class RevokeConfirmationFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "transactionId", 1)]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class IsOwnerFunction : IsOwnerFunctionBase { }

    [Function("isOwner", "bool")]
    public class IsOwnerFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class ConfirmationsFunction : ConfirmationsFunctionBase { }

    [Function("confirmations", "bool")]
    public class ConfirmationsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    public partial class GetTransactionCountFunction : GetTransactionCountFunctionBase { }

    [Function("getTransactionCount", "uint256")]
    public class GetTransactionCountFunctionBase : FunctionMessage
    {
        [Parameter("bool", "pending", 1)]
        public virtual bool Pending { get; set; }
        [Parameter("bool", "executed", 2)]
        public virtual bool Executed { get; set; }
    }

    public partial class RunTransactionFunction : RunTransactionFunctionBase { }

    [Function("runTransaction")]
    public class RunTransactionFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "transactionId", 1)]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class AddOwnerFunction : AddOwnerFunctionBase { }

    [Function("addOwner")]
    public class AddOwnerFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class IsConfirmedFunction : IsConfirmedFunctionBase { }

    [Function("isConfirmed", "bool")]
    public class IsConfirmedFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "transactionId", 1)]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class PendingTransactionsFunction : PendingTransactionsFunctionBase { }

    [Function("pendingTransactions", "uint256")]
    public class PendingTransactionsFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class GetConfirmationCountFunction : GetConfirmationCountFunctionBase { }

    [Function("getConfirmationCount", "uint256")]
    public class GetConfirmationCountFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "transactionId", 1)]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class CalcTransactionHashFunction : CalcTransactionHashFunctionBase { }

    [Function("calcTransactionHash", "bytes32")]
    public class CalcTransactionHashFunctionBase : FunctionMessage
    {
        [Parameter("address", "destination", 1)]
        public virtual string Destination { get; set; }
        [Parameter("bytes", "data", 2)]
        public virtual byte[] Data { get; set; }
    }

    public partial class TransactionsFunction : TransactionsFunctionBase { }

    [Function("transactions", typeof(TransactionsOutputDTO))]
    public class TransactionsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetOwnersFunction : GetOwnersFunctionBase { }

    [Function("getOwners", "address[]")]
    public class GetOwnersFunctionBase : FunctionMessage
    {

    }

    public partial class GetTransactionIdsFunction : GetTransactionIdsFunctionBase { }

    [Function("getTransactionIds", "uint256[]")]
    public class GetTransactionIdsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "from", 1)]
        public virtual BigInteger From { get; set; }
        [Parameter("uint256", "to", 2)]
        public virtual BigInteger To { get; set; }
        [Parameter("bool", "pending", 3)]
        public virtual bool Pending { get; set; }
        [Parameter("bool", "executed", 4)]
        public virtual bool Executed { get; set; }
    }

    public partial class GetTransactionIdFunction : GetTransactionIdFunctionBase { }

    [Function("getTransactionId", typeof(GetTransactionIdOutputDTO))]
    public class GetTransactionIdFunctionBase : FunctionMessage
    {
        [Parameter("address", "destination", 1)]
        public virtual string Destination { get; set; }
        [Parameter("bytes", "data", 2)]
        public virtual byte[] Data { get; set; }
    }

    public partial class GetConfirmationsFunction : GetConfirmationsFunctionBase { }

    [Function("getConfirmations", "address[]")]
    public class GetConfirmationsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "transactionId", 1)]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class TransactionCountFunction : TransactionCountFunctionBase { }

    [Function("transactionCount", "uint256")]
    public class TransactionCountFunctionBase : FunctionMessage
    {

    }

    public partial class ChangeRequirementFunction : ChangeRequirementFunctionBase { }

    [Function("changeRequirement")]
    public class ChangeRequirementFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_required", 1)]
        public virtual BigInteger Required { get; set; }
    }

    public partial class ConfirmTransactionFunction : ConfirmTransactionFunctionBase { }

    [Function("confirmTransaction")]
    public class ConfirmTransactionFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "transactionId", 1)]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class SubmitTransactionFunction : SubmitTransactionFunctionBase { }

    [Function("submitTransaction", "uint256")]
    public class SubmitTransactionFunctionBase : FunctionMessage
    {
        [Parameter("address", "destination", 1)]
        public virtual string Destination { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes", "data", 3)]
        public virtual byte[] Data { get; set; }
    }

    public partial class MAX_OWNER_COUNTFunction : MAX_OWNER_COUNTFunctionBase { }

    [Function("MAX_OWNER_COUNT", "uint256")]
    public class MAX_OWNER_COUNTFunctionBase : FunctionMessage
    {

    }

    public partial class RequiredFunction : RequiredFunctionBase { }

    [Function("required", "uint256")]
    public class RequiredFunctionBase : FunctionMessage
    {

    }

    public partial class ReplaceOwnerFunction : ReplaceOwnerFunctionBase { }

    [Function("replaceOwner")]
    public class ReplaceOwnerFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "newOwner", 2)]
        public virtual string NewOwner { get; set; }
    }

    public partial class ExecuteTransactionFunction : ExecuteTransactionFunctionBase { }

    [Function("executeTransaction")]
    public class ExecuteTransactionFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "transactionId", 1)]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class ConfirmationEventDTO : ConfirmationEventDTOBase { }

    [Event("Confirmation")]
    public class ConfirmationEventDTOBase : IEventDTO
    {
        [Parameter("address", "sender", 1, true )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "transactionId", 2, true )]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class RevocationEventDTO : RevocationEventDTOBase { }

    [Event("Revocation")]
    public class RevocationEventDTOBase : IEventDTO
    {
        [Parameter("address", "sender", 1, true )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "transactionId", 2, true )]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class SubmissionEventDTO : SubmissionEventDTOBase { }

    [Event("Submission")]
    public class SubmissionEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "transactionId", 1, true )]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class ExecutionEventDTO : ExecutionEventDTOBase { }

    [Event("Execution")]
    public class ExecutionEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "transactionId", 1, true )]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class ExecutionFailureEventDTO : ExecutionFailureEventDTOBase { }

    [Event("ExecutionFailure")]
    public class ExecutionFailureEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "transactionId", 1, true )]
        public virtual BigInteger TransactionId { get; set; }
    }

    public partial class DepositEventDTO : DepositEventDTOBase { }

    [Event("Deposit")]
    public class DepositEventDTOBase : IEventDTO
    {
        [Parameter("address", "sender", 1, true )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "value", 2, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class OwnerAdditionEventDTO : OwnerAdditionEventDTOBase { }

    [Event("OwnerAddition")]
    public class OwnerAdditionEventDTOBase : IEventDTO
    {
        [Parameter("address", "owner", 1, true )]
        public virtual string Owner { get; set; }
    }

    public partial class OwnerRemovalEventDTO : OwnerRemovalEventDTOBase { }

    [Event("OwnerRemoval")]
    public class OwnerRemovalEventDTOBase : IEventDTO
    {
        [Parameter("address", "owner", 1, true )]
        public virtual string Owner { get; set; }
    }

    public partial class RequirementChangeEventDTO : RequirementChangeEventDTOBase { }

    [Event("RequirementChange")]
    public class RequirementChangeEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "required", 1, false )]
        public virtual BigInteger Required { get; set; }
    }

    public partial class OwnersOutputDTO : OwnersOutputDTOBase { }

    [FunctionOutput]
    public class OwnersOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }





    public partial class IsOwnerOutputDTO : IsOwnerOutputDTOBase { }

    [FunctionOutput]
    public class IsOwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class ConfirmationsOutputDTO : ConfirmationsOutputDTOBase { }

    [FunctionOutput]
    public class ConfirmationsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GetTransactionCountOutputDTO : GetTransactionCountOutputDTOBase { }

    [FunctionOutput]
    public class GetTransactionCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "count", 1)]
        public virtual BigInteger Count { get; set; }
    }





    public partial class IsConfirmedOutputDTO : IsConfirmedOutputDTOBase { }

    [FunctionOutput]
    public class IsConfirmedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class PendingTransactionsOutputDTO : PendingTransactionsOutputDTOBase { }

    [FunctionOutput]
    public class PendingTransactionsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetConfirmationCountOutputDTO : GetConfirmationCountOutputDTOBase { }

    [FunctionOutput]
    public class GetConfirmationCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "count", 1)]
        public virtual BigInteger Count { get; set; }
    }

    public partial class CalcTransactionHashOutputDTO : CalcTransactionHashOutputDTOBase { }

    [FunctionOutput]
    public class CalcTransactionHashOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class TransactionsOutputDTO : TransactionsOutputDTOBase { }

    [FunctionOutput]
    public class TransactionsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "destination", 1)]
        public virtual string Destination { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes", "data", 3)]
        public virtual byte[] Data { get; set; }
        [Parameter("bool", "executed", 4)]
        public virtual bool Executed { get; set; }
    }

    public partial class GetOwnersOutputDTO : GetOwnersOutputDTOBase { }

    [FunctionOutput]
    public class GetOwnersOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }

    public partial class GetTransactionIdsOutputDTO : GetTransactionIdsOutputDTOBase { }

    [FunctionOutput]
    public class GetTransactionIdsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256[]", "_transactionIds", 1)]
        public virtual List<BigInteger> TransactionIds { get; set; }
    }

    public partial class GetTransactionIdOutputDTO : GetTransactionIdOutputDTOBase { }

    [FunctionOutput]
    public class GetTransactionIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "transactionId", 1)]
        public virtual BigInteger TransactionId { get; set; }
        [Parameter("bool", "isTransaction", 2)]
        public virtual bool IsTransaction { get; set; }
    }

    public partial class GetConfirmationsOutputDTO : GetConfirmationsOutputDTOBase { }

    [FunctionOutput]
    public class GetConfirmationsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address[]", "_confirmations", 1)]
        public virtual List<string> Confirmations { get; set; }
    }

    public partial class TransactionCountOutputDTO : TransactionCountOutputDTOBase { }

    [FunctionOutput]
    public class TransactionCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }







    public partial class MAX_OWNER_COUNTOutputDTO : MAX_OWNER_COUNTOutputDTOBase { }

    [FunctionOutput]
    public class MAX_OWNER_COUNTOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class RequiredOutputDTO : RequiredOutputDTOBase { }

    [FunctionOutput]
    public class RequiredOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
