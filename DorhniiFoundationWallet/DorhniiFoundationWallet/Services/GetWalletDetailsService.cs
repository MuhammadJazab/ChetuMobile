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
    /// This service class is used to get wallet details
    /// </summary>
    public class GetWalletDetailsService : IGetWalletDetailsService
    {
        ServiceHelpers serviceHelpers;
        public GetWalletDetailsService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }

        /// <summary>
        /// This task is used to get the wallet details.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<GetWalletDetailsResponseModel> GetWalletDetails(GetWalletsDetailsRequestModel requestModel)
        {
            GetWalletDetailsResponseModel responseModel = new GetWalletDetailsResponseModel();
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(requestModel);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.WalletDetailsAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<GetWalletDetailsResponseModel>(jsonResponse.Data);
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
