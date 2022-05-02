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
    /// This service class is used to get wallet lists.
    /// </summary>
    public class GetWalletService : IGetWalletService
    {
        ServiceHelpers serviceHelpers;
        public GetWalletService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }

        /// <summary>
        /// This task is used to get wallet lists.
        /// </summary>
        /// <param name="getWalletRequestModel"></param>
        /// <returns></returns>
        public async Task<GetWalletResponseModel> GetWallet(GetWalletRequestModel getWalletRequestModel)
        {
            GetWalletResponseModel responseModel = new GetWalletResponseModel();
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(getWalletRequestModel);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.WalletAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<GetWalletResponseModel>(jsonResponse.Data);
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
