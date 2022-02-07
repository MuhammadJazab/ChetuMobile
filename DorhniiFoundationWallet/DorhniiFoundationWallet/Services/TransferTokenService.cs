using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.Services
{
    public class TransferTokenService : ITransferTokenService
    {
        ServiceHelpers serviceHelpers;
        public TransferTokenService()
        {
            serviceHelpers = ServiceHelpers.Instance;

        }

        public async Task<ResponseModel> TranferToken(TransferTokenRequestModel transferTokenRequestModel)
        {
            ResponseModel transferTokenStatus = new ResponseModel();
            try
            {
                string serializedRequest = Newtonsoft.Json.JsonConvert.SerializeObject(transferTokenRequestModel);
                ResponseModel response = await ServiceHelpers.Instance.PostRequest(serializedRequest, StringConstant.TransferTokenAPI, true, null);
                if (response != null)
                {
                    transferTokenStatus = JsonConvert.DeserializeObject<ResponseModel>(response.Data);

                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return transferTokenStatus;
        }
    }

}
