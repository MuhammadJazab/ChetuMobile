using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to Create Password, Login and password change in the application application
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// This task is used to create password using PasswordSetuprequestModel
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<PasswordSetupResponseModel> CreatePasswrod(PasswordSetupRequestModel requestModel);

        /// <summary>
        /// This task is used to Login in application using LoginRequestModel
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<APIResponseModel> LoginFunction(LoginRequestModel requestModel);

        /// <summary>
        /// This task is chnage password in application using ChangePasswordRequestModel
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<APIResponseModel> ChangePassword(ChangePasswordRequestModel requestModel);
    }
}
