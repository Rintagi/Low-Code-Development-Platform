using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Contract.Contracts.MultiSigWallet.ContractDefinition;

namespace Contract.Contracts.MultiSigWallet
{
    public partial class MultiSigWalletService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, MultiSigWalletDeployment multiSigWalletDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<MultiSigWalletDeployment>().SendRequestAndWaitForReceiptAsync(multiSigWalletDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, MultiSigWalletDeployment multiSigWalletDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<MultiSigWalletDeployment>().SendRequestAsync(multiSigWalletDeployment);
        }

        public static async Task<MultiSigWalletService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, MultiSigWalletDeployment multiSigWalletDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, multiSigWalletDeployment, cancellationTokenSource);
            return new MultiSigWalletService(web3, receipt.ContractAddress);
        }

        public const string ABI = "[{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"owners\",\"outputs\":[{\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"removeOwner\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"revokeConfirmation\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"address\"}],\"name\":\"isOwner\",\"outputs\":[{\"name\":\"\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"},{\"name\":\"\",\"type\":\"address\"}],\"name\":\"confirmations\",\"outputs\":[{\"name\":\"\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"pending\",\"type\":\"bool\"},{\"name\":\"executed\",\"type\":\"bool\"}],\"name\":\"getTransactionCount\",\"outputs\":[{\"name\":\"count\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"runTransaction\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"addOwner\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"isConfirmed\",\"outputs\":[{\"name\":\"\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"bytes32\"}],\"name\":\"pendingTransactions\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"getConfirmationCount\",\"outputs\":[{\"name\":\"count\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"destination\",\"type\":\"address\"},{\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"calcTransactionHash\",\"outputs\":[{\"name\":\"\",\"type\":\"bytes32\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"transactions\",\"outputs\":[{\"name\":\"destination\",\"type\":\"address\"},{\"name\":\"value\",\"type\":\"uint256\"},{\"name\":\"data\",\"type\":\"bytes\"},{\"name\":\"executed\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"getOwners\",\"outputs\":[{\"name\":\"\",\"type\":\"address[]\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"from\",\"type\":\"uint256\"},{\"name\":\"to\",\"type\":\"uint256\"},{\"name\":\"pending\",\"type\":\"bool\"},{\"name\":\"executed\",\"type\":\"bool\"}],\"name\":\"getTransactionIds\",\"outputs\":[{\"name\":\"_transactionIds\",\"type\":\"uint256[]\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"destination\",\"type\":\"address\"},{\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"getTransactionId\",\"outputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"},{\"name\":\"isTransaction\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"getConfirmations\",\"outputs\":[{\"name\":\"_confirmations\",\"type\":\"address[]\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"transactionCount\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"_required\",\"type\":\"uint256\"}],\"name\":\"changeRequirement\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"confirmTransaction\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"destination\",\"type\":\"address\"},{\"name\":\"value\",\"type\":\"uint256\"},{\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"submitTransaction\",\"outputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"MAX_OWNER_COUNT\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"required\",\"outputs\":[{\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"owner\",\"type\":\"address\"},{\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"replaceOwner\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"executeTransaction\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"name\":\"_owners\",\"type\":\"address[]\"},{\"name\":\"_required\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"payable\":true,\"stateMutability\":\"payable\",\"type\":\"fallback\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":true,\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"Confirmation\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":true,\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"Revocation\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"Submission\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"Execution\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"transactionId\",\"type\":\"uint256\"}],\"name\":\"ExecutionFailure\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Deposit\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"OwnerAddition\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"OwnerRemoval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"name\":\"required\",\"type\":\"uint256\"}],\"name\":\"RequirementChange\",\"type\":\"event\"}]";

        protected Nethereum.Web3.Web3 Web3 { get; set; }

        public ContractHandler ContractHandler { get; set; }

        public MultiSigWalletService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> OwnersQueryAsync(OwnersFunction ownersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnersFunction, string>(ownersFunction, blockParameter);
        }

        
        public Task<string> OwnersQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var ownersFunction = new OwnersFunction();
                ownersFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<OwnersFunction, string>(ownersFunction, blockParameter);
        }

        public Task<string> RemoveOwnerRequestAsync(RemoveOwnerFunction removeOwnerFunction)
        {
             return ContractHandler.SendRequestAsync(removeOwnerFunction);
        }

        public Task<TransactionReceipt> RemoveOwnerRequestAndWaitForReceiptAsync(RemoveOwnerFunction removeOwnerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeOwnerFunction, cancellationToken);
        }

        public Task<string> RemoveOwnerRequestAsync(string owner)
        {
            var removeOwnerFunction = new RemoveOwnerFunction();
                removeOwnerFunction.Owner = owner;
            
             return ContractHandler.SendRequestAsync(removeOwnerFunction);
        }

        public Task<TransactionReceipt> RemoveOwnerRequestAndWaitForReceiptAsync(string owner, CancellationTokenSource cancellationToken = null)
        {
            var removeOwnerFunction = new RemoveOwnerFunction();
                removeOwnerFunction.Owner = owner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeOwnerFunction, cancellationToken);
        }

        public Task<string> RevokeConfirmationRequestAsync(RevokeConfirmationFunction revokeConfirmationFunction)
        {
             return ContractHandler.SendRequestAsync(revokeConfirmationFunction);
        }

        public Task<TransactionReceipt> RevokeConfirmationRequestAndWaitForReceiptAsync(RevokeConfirmationFunction revokeConfirmationFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeConfirmationFunction, cancellationToken);
        }

        public Task<string> RevokeConfirmationRequestAsync(BigInteger transactionId)
        {
            var revokeConfirmationFunction = new RevokeConfirmationFunction();
                revokeConfirmationFunction.TransactionId = transactionId;
            
             return ContractHandler.SendRequestAsync(revokeConfirmationFunction);
        }

        public Task<TransactionReceipt> RevokeConfirmationRequestAndWaitForReceiptAsync(BigInteger transactionId, CancellationTokenSource cancellationToken = null)
        {
            var revokeConfirmationFunction = new RevokeConfirmationFunction();
                revokeConfirmationFunction.TransactionId = transactionId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeConfirmationFunction, cancellationToken);
        }

        public Task<bool> IsOwnerQueryAsync(IsOwnerFunction isOwnerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsOwnerFunction, bool>(isOwnerFunction, blockParameter);
        }

        
        public Task<bool> IsOwnerQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var isOwnerFunction = new IsOwnerFunction();
                isOwnerFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<IsOwnerFunction, bool>(isOwnerFunction, blockParameter);
        }

        public Task<bool> ConfirmationsQueryAsync(ConfirmationsFunction confirmationsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ConfirmationsFunction, bool>(confirmationsFunction, blockParameter);
        }

        
        public Task<bool> ConfirmationsQueryAsync(BigInteger returnValue1, string returnValue2, BlockParameter blockParameter = null)
        {
            var confirmationsFunction = new ConfirmationsFunction();
                confirmationsFunction.ReturnValue1 = returnValue1;
                confirmationsFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryAsync<ConfirmationsFunction, bool>(confirmationsFunction, blockParameter);
        }

        public Task<BigInteger> GetTransactionCountQueryAsync(GetTransactionCountFunction getTransactionCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTransactionCountFunction, BigInteger>(getTransactionCountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetTransactionCountQueryAsync(bool pending, bool executed, BlockParameter blockParameter = null)
        {
            var getTransactionCountFunction = new GetTransactionCountFunction();
                getTransactionCountFunction.Pending = pending;
                getTransactionCountFunction.Executed = executed;
            
            return ContractHandler.QueryAsync<GetTransactionCountFunction, BigInteger>(getTransactionCountFunction, blockParameter);
        }

        public Task<string> RunTransactionRequestAsync(RunTransactionFunction runTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(runTransactionFunction);
        }

        public Task<TransactionReceipt> RunTransactionRequestAndWaitForReceiptAsync(RunTransactionFunction runTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(runTransactionFunction, cancellationToken);
        }

        public Task<string> RunTransactionRequestAsync(BigInteger transactionId)
        {
            var runTransactionFunction = new RunTransactionFunction();
                runTransactionFunction.TransactionId = transactionId;
            
             return ContractHandler.SendRequestAsync(runTransactionFunction);
        }

        public Task<TransactionReceipt> RunTransactionRequestAndWaitForReceiptAsync(BigInteger transactionId, CancellationTokenSource cancellationToken = null)
        {
            var runTransactionFunction = new RunTransactionFunction();
                runTransactionFunction.TransactionId = transactionId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(runTransactionFunction, cancellationToken);
        }

        public Task<string> AddOwnerRequestAsync(AddOwnerFunction addOwnerFunction)
        {
             return ContractHandler.SendRequestAsync(addOwnerFunction);
        }

        public Task<TransactionReceipt> AddOwnerRequestAndWaitForReceiptAsync(AddOwnerFunction addOwnerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addOwnerFunction, cancellationToken);
        }

        public Task<string> AddOwnerRequestAsync(string owner)
        {
            var addOwnerFunction = new AddOwnerFunction();
                addOwnerFunction.Owner = owner;
            
             return ContractHandler.SendRequestAsync(addOwnerFunction);
        }

        public Task<TransactionReceipt> AddOwnerRequestAndWaitForReceiptAsync(string owner, CancellationTokenSource cancellationToken = null)
        {
            var addOwnerFunction = new AddOwnerFunction();
                addOwnerFunction.Owner = owner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addOwnerFunction, cancellationToken);
        }

        public Task<bool> IsConfirmedQueryAsync(IsConfirmedFunction isConfirmedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsConfirmedFunction, bool>(isConfirmedFunction, blockParameter);
        }

        
        public Task<bool> IsConfirmedQueryAsync(BigInteger transactionId, BlockParameter blockParameter = null)
        {
            var isConfirmedFunction = new IsConfirmedFunction();
                isConfirmedFunction.TransactionId = transactionId;
            
            return ContractHandler.QueryAsync<IsConfirmedFunction, bool>(isConfirmedFunction, blockParameter);
        }

        public Task<BigInteger> PendingTransactionsQueryAsync(PendingTransactionsFunction pendingTransactionsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PendingTransactionsFunction, BigInteger>(pendingTransactionsFunction, blockParameter);
        }

        
        public Task<BigInteger> PendingTransactionsQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var pendingTransactionsFunction = new PendingTransactionsFunction();
                pendingTransactionsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<PendingTransactionsFunction, BigInteger>(pendingTransactionsFunction, blockParameter);
        }

        public Task<BigInteger> GetConfirmationCountQueryAsync(GetConfirmationCountFunction getConfirmationCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConfirmationCountFunction, BigInteger>(getConfirmationCountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetConfirmationCountQueryAsync(BigInteger transactionId, BlockParameter blockParameter = null)
        {
            var getConfirmationCountFunction = new GetConfirmationCountFunction();
                getConfirmationCountFunction.TransactionId = transactionId;
            
            return ContractHandler.QueryAsync<GetConfirmationCountFunction, BigInteger>(getConfirmationCountFunction, blockParameter);
        }

        public Task<byte[]> CalcTransactionHashQueryAsync(CalcTransactionHashFunction calcTransactionHashFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CalcTransactionHashFunction, byte[]>(calcTransactionHashFunction, blockParameter);
        }

        
        public Task<byte[]> CalcTransactionHashQueryAsync(string destination, byte[] data, BlockParameter blockParameter = null)
        {
            var calcTransactionHashFunction = new CalcTransactionHashFunction();
                calcTransactionHashFunction.Destination = destination;
                calcTransactionHashFunction.Data = data;
            
            return ContractHandler.QueryAsync<CalcTransactionHashFunction, byte[]>(calcTransactionHashFunction, blockParameter);
        }

        public Task<TransactionsOutputDTO> TransactionsQueryAsync(TransactionsFunction transactionsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<TransactionsFunction, TransactionsOutputDTO>(transactionsFunction, blockParameter);
        }

        public Task<TransactionsOutputDTO> TransactionsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var transactionsFunction = new TransactionsFunction();
                transactionsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<TransactionsFunction, TransactionsOutputDTO>(transactionsFunction, blockParameter);
        }

        public Task<List<string>> GetOwnersQueryAsync(GetOwnersFunction getOwnersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetOwnersFunction, List<string>>(getOwnersFunction, blockParameter);
        }

        
        public Task<List<string>> GetOwnersQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetOwnersFunction, List<string>>(null, blockParameter);
        }

        public Task<List<BigInteger>> GetTransactionIdsQueryAsync(GetTransactionIdsFunction getTransactionIdsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTransactionIdsFunction, List<BigInteger>>(getTransactionIdsFunction, blockParameter);
        }

        
        public Task<List<BigInteger>> GetTransactionIdsQueryAsync(BigInteger from, BigInteger to, bool pending, bool executed, BlockParameter blockParameter = null)
        {
            var getTransactionIdsFunction = new GetTransactionIdsFunction();
                getTransactionIdsFunction.From = from;
                getTransactionIdsFunction.To = to;
                getTransactionIdsFunction.Pending = pending;
                getTransactionIdsFunction.Executed = executed;
            
            return ContractHandler.QueryAsync<GetTransactionIdsFunction, List<BigInteger>>(getTransactionIdsFunction, blockParameter);
        }

        public Task<GetTransactionIdOutputDTO> GetTransactionIdQueryAsync(GetTransactionIdFunction getTransactionIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetTransactionIdFunction, GetTransactionIdOutputDTO>(getTransactionIdFunction, blockParameter);
        }

        public Task<GetTransactionIdOutputDTO> GetTransactionIdQueryAsync(string destination, byte[] data, BlockParameter blockParameter = null)
        {
            var getTransactionIdFunction = new GetTransactionIdFunction();
                getTransactionIdFunction.Destination = destination;
                getTransactionIdFunction.Data = data;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetTransactionIdFunction, GetTransactionIdOutputDTO>(getTransactionIdFunction, blockParameter);
        }

        public Task<List<string>> GetConfirmationsQueryAsync(GetConfirmationsFunction getConfirmationsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConfirmationsFunction, List<string>>(getConfirmationsFunction, blockParameter);
        }

        
        public Task<List<string>> GetConfirmationsQueryAsync(BigInteger transactionId, BlockParameter blockParameter = null)
        {
            var getConfirmationsFunction = new GetConfirmationsFunction();
                getConfirmationsFunction.TransactionId = transactionId;
            
            return ContractHandler.QueryAsync<GetConfirmationsFunction, List<string>>(getConfirmationsFunction, blockParameter);
        }

        public Task<BigInteger> TransactionCountQueryAsync(TransactionCountFunction transactionCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TransactionCountFunction, BigInteger>(transactionCountFunction, blockParameter);
        }

        
        public Task<BigInteger> TransactionCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TransactionCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> ChangeRequirementRequestAsync(ChangeRequirementFunction changeRequirementFunction)
        {
             return ContractHandler.SendRequestAsync(changeRequirementFunction);
        }

        public Task<TransactionReceipt> ChangeRequirementRequestAndWaitForReceiptAsync(ChangeRequirementFunction changeRequirementFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(changeRequirementFunction, cancellationToken);
        }

        public Task<string> ChangeRequirementRequestAsync(BigInteger required)
        {
            var changeRequirementFunction = new ChangeRequirementFunction();
                changeRequirementFunction.Required = required;
            
             return ContractHandler.SendRequestAsync(changeRequirementFunction);
        }

        public Task<TransactionReceipt> ChangeRequirementRequestAndWaitForReceiptAsync(BigInteger required, CancellationTokenSource cancellationToken = null)
        {
            var changeRequirementFunction = new ChangeRequirementFunction();
                changeRequirementFunction.Required = required;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(changeRequirementFunction, cancellationToken);
        }

        public Task<string> ConfirmTransactionRequestAsync(ConfirmTransactionFunction confirmTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(confirmTransactionFunction);
        }

        public Task<TransactionReceipt> ConfirmTransactionRequestAndWaitForReceiptAsync(ConfirmTransactionFunction confirmTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmTransactionFunction, cancellationToken);
        }

        public Task<string> ConfirmTransactionRequestAsync(BigInteger transactionId)
        {
            var confirmTransactionFunction = new ConfirmTransactionFunction();
                confirmTransactionFunction.TransactionId = transactionId;
            
             return ContractHandler.SendRequestAsync(confirmTransactionFunction);
        }

        public Task<TransactionReceipt> ConfirmTransactionRequestAndWaitForReceiptAsync(BigInteger transactionId, CancellationTokenSource cancellationToken = null)
        {
            var confirmTransactionFunction = new ConfirmTransactionFunction();
                confirmTransactionFunction.TransactionId = transactionId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmTransactionFunction, cancellationToken);
        }

        public Task<string> SubmitTransactionRequestAsync(SubmitTransactionFunction submitTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(submitTransactionFunction);
        }

        public Task<TransactionReceipt> SubmitTransactionRequestAndWaitForReceiptAsync(SubmitTransactionFunction submitTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(submitTransactionFunction, cancellationToken);
        }

        public Task<string> SubmitTransactionRequestAsync(string destination, BigInteger value, byte[] data)
        {
            var submitTransactionFunction = new SubmitTransactionFunction();
                submitTransactionFunction.Destination = destination;
                submitTransactionFunction.Value = value;
                submitTransactionFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(submitTransactionFunction);
        }

        public Task<TransactionReceipt> SubmitTransactionRequestAndWaitForReceiptAsync(string destination, BigInteger value, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var submitTransactionFunction = new SubmitTransactionFunction();
                submitTransactionFunction.Destination = destination;
                submitTransactionFunction.Value = value;
                submitTransactionFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(submitTransactionFunction, cancellationToken);
        }

        public Task<BigInteger> MAX_OWNER_COUNTQueryAsync(MAX_OWNER_COUNTFunction mAX_OWNER_COUNTFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MAX_OWNER_COUNTFunction, BigInteger>(mAX_OWNER_COUNTFunction, blockParameter);
        }

        
        public Task<BigInteger> MAX_OWNER_COUNTQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MAX_OWNER_COUNTFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> RequiredQueryAsync(RequiredFunction requiredFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RequiredFunction, BigInteger>(requiredFunction, blockParameter);
        }

        
        public Task<BigInteger> RequiredQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RequiredFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> ReplaceOwnerRequestAsync(ReplaceOwnerFunction replaceOwnerFunction)
        {
             return ContractHandler.SendRequestAsync(replaceOwnerFunction);
        }

        public Task<TransactionReceipt> ReplaceOwnerRequestAndWaitForReceiptAsync(ReplaceOwnerFunction replaceOwnerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(replaceOwnerFunction, cancellationToken);
        }

        public Task<string> ReplaceOwnerRequestAsync(string owner, string newOwner)
        {
            var replaceOwnerFunction = new ReplaceOwnerFunction();
                replaceOwnerFunction.Owner = owner;
                replaceOwnerFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAsync(replaceOwnerFunction);
        }

        public Task<TransactionReceipt> ReplaceOwnerRequestAndWaitForReceiptAsync(string owner, string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var replaceOwnerFunction = new ReplaceOwnerFunction();
                replaceOwnerFunction.Owner = owner;
                replaceOwnerFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(replaceOwnerFunction, cancellationToken);
        }

        public Task<string> ExecuteTransactionRequestAsync(ExecuteTransactionFunction executeTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(executeTransactionFunction);
        }

        public Task<TransactionReceipt> ExecuteTransactionRequestAndWaitForReceiptAsync(ExecuteTransactionFunction executeTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(executeTransactionFunction, cancellationToken);
        }

        public Task<string> ExecuteTransactionRequestAsync(BigInteger transactionId)
        {
            var executeTransactionFunction = new ExecuteTransactionFunction();
                executeTransactionFunction.TransactionId = transactionId;
            
             return ContractHandler.SendRequestAsync(executeTransactionFunction);
        }

        public Task<TransactionReceipt> ExecuteTransactionRequestAndWaitForReceiptAsync(BigInteger transactionId, CancellationTokenSource cancellationToken = null)
        {
            var executeTransactionFunction = new ExecuteTransactionFunction();
                executeTransactionFunction.TransactionId = transactionId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(executeTransactionFunction, cancellationToken);
        }
    }
}
