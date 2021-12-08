using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to get transaction history.
    /// </summary>
    public interface ITransactionHistoryService
    {
        /// <summary>
        /// This task is used to get all transaction history.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<TransactionHistoryResponseModel> GetAllTransaction(TransactionHistoryRequestModel requestModel);

        /// <summary>
        /// This task is used to get all send transaction history.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<TransactionHistoryResponseModel> GetSendTransaction(TransactionHistoryRequestModel requestModel);

        /// <summary>
        /// This task is used to get all receive transaction history.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<TransactionHistoryResponseModel> GetReceiveTransaction(TransactionHistoryRequestModel requestModel);
    }
}
