using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to GetGas Fee for the transaction
    /// </summary>
    public interface IGasEstimateFeeService
    {
        /// <summary>
        /// This task is used to Get Gas fee by using Transfer Token request model
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<GasFeeResponseModel> CalculateGasFee(GasEstimateRequestModel requestModel);
    }
}
