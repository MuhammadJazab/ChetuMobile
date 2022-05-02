using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System.Threading.Tasks;
namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to Transfer the Token
    /// </summary>
    public interface ITransferTokenService
    {
        /// <summary>
        /// This task is used to transfer the Token to the application using the TranferToken request model
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<TransferTokenResponseModel> TranferToken(TransferTokenRequestModel requestModel);
    }
}
