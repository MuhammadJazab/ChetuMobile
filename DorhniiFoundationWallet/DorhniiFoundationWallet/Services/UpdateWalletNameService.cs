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

    public class UpdateWalletNameService : IUpdatedateWalletName
    {
        public async Task<EditWalletNameResponseModel> UpdateWalletName(EditWalletNameRequestModel requestModel)
        {
            EditWalletNameResponseModel responseModel = new EditWalletNameResponseModel();
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(requestModel);
                ResponseModel jsonResponse = await ServiceHelpers.Instance.PostRequest(jsonRequest, StringConstant.UpdateWalletNameAPI, true, null);
                if (jsonResponse.IsSuccess)
                {
                    responseModel = JsonConvert.DeserializeObject<EditWalletNameResponseModel>(jsonResponse.Data);
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
