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
    public class TransferTokenService : ITransferTokenService
    {
        /// <summary>
        /// This service class is used to transfer the Token.
        /// </summary>
        ServiceHelpers serviceHelpers;
        public TransferTokenService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }        

        /// <summary>
        /// this task is used to transfer token
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        async Task<TransferTokenResponseModel> ITransferTokenService.TranferToken(TransferTokenRequestModel requestModel)
        {
            TransferTokenResponseModel responseModel = new TransferTokenResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(requestModel);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.TransferTokenAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<TransferTokenResponseModel>(jsonResponse.Data);
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
