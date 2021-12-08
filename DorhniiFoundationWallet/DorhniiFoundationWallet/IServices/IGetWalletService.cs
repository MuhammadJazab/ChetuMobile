using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to get wallet.
    /// </summary>
    public interface IGetWalletService
    {
        /// <summary>
        /// This task is used to get wallet.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<GetWalletResponseModel> GetWallet(GetWalletRequestModel requestModel);
    }
}
