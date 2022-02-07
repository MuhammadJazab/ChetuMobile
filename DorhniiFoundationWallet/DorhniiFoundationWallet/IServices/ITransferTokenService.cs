using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System.Threading.Tasks;
namespace DorhniiFoundationWallet.IServices
{
    public interface ITransferTokenService
    {
        Task<ResponseModel> TranferToken(TransferTokenRequestModel requestModel);
    }
}
