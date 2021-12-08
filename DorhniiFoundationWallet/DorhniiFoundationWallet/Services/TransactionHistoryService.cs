using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.Services
{
    /// <summary>
    /// This services class is used to get all trasactions, send transactions and receive transactions history.
    /// </summary>
    public class TransactionHistoryService : ITransactionHistoryService
    {
        ServiceHelpers serviceHelpers;
        public TransactionHistoryService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }

        /// <summary>
        /// This task is used to get all trasactions history.
        /// </summary>
        /// <param name="transactionHistoryRequest"></param>
        /// <returns></returns>
        public async Task<TransactionHistoryResponseModel> GetAllTransaction(TransactionHistoryRequestModel transactionHistoryRequest)
        {
            TransactionHistoryResponseModel responseModel = new TransactionHistoryResponseModel();
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(transactionHistoryRequest);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.TransactionHistoryAllAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<TransactionHistoryResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }

        /// <summary>
        /// This task is used to get send transactions history.
        /// </summary>
        /// <param name="transactionHistoryRequest"></param>
        /// <returns></returns>
        public async Task<TransactionHistoryResponseModel> GetSendTransaction(TransactionHistoryRequestModel transactionHistoryRequest)
        {
            TransactionHistoryResponseModel responseModel = new TransactionHistoryResponseModel();
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(transactionHistoryRequest);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.TransactionHistoryAllAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<TransactionHistoryResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }

        /// <summary>
        /// This task is used to get receive transactions history.
        /// </summary>
        /// <param name="transactionHistoryRequest"></param>
        /// <returns></returns>
        public async Task<TransactionHistoryResponseModel> GetReceiveTransaction(TransactionHistoryRequestModel transactionHistoryRequest)
        {
            TransactionHistoryResponseModel responseModel = new TransactionHistoryResponseModel();
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(transactionHistoryRequest);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.TransactionHistoryAllAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<TransactionHistoryResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }
    }
}
