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
    /// This service class is used to Send The support mail.
    /// </summary>
    public class SupportPageService : ISuportPageService
    {
        ServiceHelpers serviceHelpers;
        public SupportPageService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }

        /// <summary>
        /// This task is used to get the wallet details.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> SendSupportmail(SupportPageRequestModel requestModel)
        {
            APIResponseModel responseModel = new APIResponseModel();
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(requestModel);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.SupportPageAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<APIResponseModel>(jsonResponse.Data);
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

