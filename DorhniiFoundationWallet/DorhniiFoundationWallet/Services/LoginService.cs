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
    public class LoginService : ILoginService
    {
        ServiceHelpers serviceHelpers;
        public LoginService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }
        /// <summary>
        /// Service Method for Change Password
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> ChangePassword(ChangePasswordRequestModel requestModel)
        {

            APIResponseModel changePasswordResponseModel = null;
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(requestModel);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.ChangePasswordApi, true, null);
                if (jsonResponse.IsSuccess)
                {
                    changePasswordResponseModel = JsonConvert.DeserializeObject<APIResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return changePasswordResponseModel;
        }

        /// <summary>
        /// Service Method for Create Password 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<PasswordSetupResponseModel> CreatePasswrod(PasswordSetupRequestModel requestModel)
        {
            PasswordSetupResponseModel responseModel = null;
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(requestModel);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.CreatePasswordAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<PasswordSetupResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }      

        /// <summary>
        /// Service method for Login Function
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        async Task<APIResponseModel> ILoginService.LoginFunction(LoginRequestModel requestModel)
        {
            APIResponseModel loginResponseModel = null;
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(requestModel);
                var jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.LoginApi, true, null);
                if (jsonResponse.IsSuccess)
                {
                    loginResponseModel = JsonConvert.DeserializeObject<APIResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return loginResponseModel;
        }
    }
}
