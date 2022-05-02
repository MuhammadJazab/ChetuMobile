using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to Support  
    /// </summary>
    public interface ISuportPageService
    {
        /// <summary>
        /// This task is used to send support mail using SendSupportmail request model.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<APIResponseModel> SendSupportmail(SupportPageRequestModel requestModel);
    }
}
