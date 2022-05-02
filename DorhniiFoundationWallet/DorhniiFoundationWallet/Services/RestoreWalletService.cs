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
    /// This service class is used to Restore the Wallet  .
    /// </summary>
    public class RestoreWalletService : IRestoreWalletService
    {
        ServiceHelpers serviceHelpers;

        public RestoreWalletService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }
        /// <summary>
        /// This task is used to Add wallets.
        /// </summary>
        /// <param name="addWalletRequest"></param>
        /// <returns></returns>
        public async Task<RestoreWalletResponseModel> RestoreWallet(RestoreWalletRequestModel addWalletRequest)
        {
            RestoreWalletResponseModel responseModel = new RestoreWalletResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(addWalletRequest);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.RestoreWalletAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<RestoreWalletResponseModel>(jsonResponse.Data);
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
