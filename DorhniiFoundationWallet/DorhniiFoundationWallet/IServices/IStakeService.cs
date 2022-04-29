using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to Staking functionality
    /// </summary>
    public interface IStakeService
    {
        Task<StakeTokenOverviewResponseModel> GetTokenOverView(StakeTokenOverViewRequestModel requestModel);
        /// <summary>
        /// This task is used to get the available token for staking s the Token to the application using the TranferToken request model
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<StakeBalanceAvailableResponseModel> GetAvailableStakeToken(StakeBalanceAvailableRequestModel requestModel);
        Task<APIResponseModel> TakeStake(TakeStakeRequestModel requestModel);
        Task<GetStakedResponseModel> GetStakedDetail(GetStakedDetailRequestModel requestModel);
        Task<APIResponseModel> UnStakeToken(UnStakeRequestModel requestModel);
    }
}
