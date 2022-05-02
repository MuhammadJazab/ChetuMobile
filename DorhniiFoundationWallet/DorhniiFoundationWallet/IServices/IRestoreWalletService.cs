using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to Restore wallet 
    /// </summary>
    public interface IRestoreWalletService
    {
        /// <summary>
        /// This Task is used to Restore wallet using Restore request model
        /// </summary>
        Task<RestoreWalletResponseModel> RestoreWallet(RestoreWalletRequestModel requestModel);
    }
}
