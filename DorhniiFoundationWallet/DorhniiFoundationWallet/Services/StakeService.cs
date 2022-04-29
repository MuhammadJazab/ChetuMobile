using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.Services
{
    public class StakeService : IStakeService
    {
        ServiceHelpers serviceHelpers;
        public StakeService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }
        /// <summary>
        /// This Task is used to Get token Overview on stake page
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<StakeTokenOverviewResponseModel> GetTokenOverView(StakeTokenOverViewRequestModel requestModel)
        {
            StakeTokenOverviewResponseModel responseModel = new StakeTokenOverviewResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(requestModel);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.StakeTokenOverViewAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<StakeTokenOverviewResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }

        /// <summary>
        /// This Task is used to get the detail about staked coin.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<GetStakedResponseModel> GetStakedDetail(GetStakedDetailRequestModel requestModel)
        {
            GetStakedResponseModel responseModel = new GetStakedResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(requestModel);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.GetStakedDetailAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<GetStakedResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }

        /// <summary>
        /// This task is used to unstake the  matured staked token
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> UnStakeToken(UnStakeRequestModel requestModel)
        {
            APIResponseModel responseModel = new APIResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(requestModel);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.UnStakeTokenAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<GetStakedResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }

        /// <summary>
        /// This task is used to get the availble amount of token for staking
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<StakeBalanceAvailableResponseModel> GetAvailableStakeToken(StakeBalanceAvailableRequestModel requestModel)
        {
            StakeBalanceAvailableResponseModel responseModel = new StakeBalanceAvailableResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(requestModel);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.StakingAvailableBalanceAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<StakeBalanceAvailableResponseModel>(jsonResponse.Data);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }

        /// <summary>
        /// This Task is used to take stake of token
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> TakeStake(TakeStakeRequestModel requestModel)
        {
            APIResponseModel responseModel = new APIResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(requestModel);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.TakeStakeAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<APIResponseModel>(jsonResponse.Data); 
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return responseModel;
        }   
    }
}
