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
    /// This services class is used to Add wallets.
    /// </summary>
    public class AddWalletService : IAddWalletService
    {
        ServiceHelpers serviceHelpers;
        public AddWalletService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }
        /// <summary>
        /// This task is used to Add wallets.
        /// </summary>
        /// <param name="addWalletRequest"></param>
        /// <returns></returns>
        public async Task<AddWalletResponseModel> AddWallet(AddWalletRequestModel addWalletRequest)
        {
            AddWalletResponseModel responseModel = new AddWalletResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(addWalletRequest);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.CreateWalletAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<AddWalletResponseModel>(jsonResponse.Data);
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
