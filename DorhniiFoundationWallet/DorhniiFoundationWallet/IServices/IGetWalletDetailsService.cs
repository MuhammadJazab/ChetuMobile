using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to get wallet details.
    /// </summary>
    interface IGetWalletDetailsService
    {
        /// <summary>
        /// This task is used to get wallet details.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<GetWalletDetailsResponseModel> GetWalletDetails(GetWalletsDetailsRequestModel requestModel);
    }
}
