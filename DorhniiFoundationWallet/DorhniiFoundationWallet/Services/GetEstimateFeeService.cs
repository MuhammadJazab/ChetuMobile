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
    /// This service class is used to get the transaction fee.
    /// </summary>
    public class GetEstimateFeeService : IGasEstimateFeeService
    {
        ServiceHelpers serviceHelpers;
        public GetEstimateFeeService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }
        /// <summary>
        /// This task is used to Add wallets.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>

        public async Task<GasFeeResponseModel> CalculateGasFee(GasEstimateRequestModel requestModel)
        {
            GasFeeResponseModel responseModel = new GasFeeResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(requestModel);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.GasFeeAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<GasFeeResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }

        public Task<GasFeeResponseModel> CalculateGasFee(TransferTokenRequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
