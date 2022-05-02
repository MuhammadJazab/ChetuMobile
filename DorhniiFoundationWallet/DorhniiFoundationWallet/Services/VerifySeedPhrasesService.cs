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
    /// <summary>
    /// This services class is used to verify the seed phrases.
    /// </summary>
    public class VerifySeedPhrasesService : IVerifySeedPhrasesService
    {
        ServiceHelpers serviceHelpers;
        public VerifySeedPhrasesService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }

        /// <summary>
        /// This task is used to verify the seed phrases..
        /// </summary>
        /// <param name="seedPhraseVerifyRequestModel"></param>
        /// <returns></returns>
        public async Task<ResponseModel> VerifySeedPhrase(SeedPhraseVerifyRequestModel seedPhraseVerifyRequestModel)
        {
            ResponseModel aPIResponse = new ResponseModel();
            try
            {
                string serializedRequest = Newtonsoft.Json.JsonConvert.SerializeObject(seedPhraseVerifyRequestModel);
                ResponseModel response = await ServiceHelpers.Instance.PostRequest(serializedRequest, StringConstant.VerifySeedAPI, true, null);
                if (response != null)
                {
                    aPIResponse = JsonConvert.DeserializeObject<ResponseModel>(response.Data);

                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return aPIResponse;
        }
    }
}
