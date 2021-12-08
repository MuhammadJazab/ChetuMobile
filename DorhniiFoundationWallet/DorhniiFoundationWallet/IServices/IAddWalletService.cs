using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to add new wallet to the application
    /// </summary>
    public interface IAddWalletService
    {
        /// <summary>
        /// This task is used to add new wallet to the application using the AddWallet request model
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<AddWalletResponseModel> AddWallet(AddWalletRequestModel requestModel);
    }
}
