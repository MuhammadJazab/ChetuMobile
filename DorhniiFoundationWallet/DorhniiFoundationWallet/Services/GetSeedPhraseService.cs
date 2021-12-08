using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models.APIResponseModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.Services
{
    /// <summary>
    /// This service class is used to get seed phrases
    /// </summary>
    public class GetSeedPhraseService : IGetSeedPhraseService
    {
        ServiceHelpers serviceHelpers;
        public GetSeedPhraseService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }

        /// <summary>
        /// This task is used to get seed phrases
        /// </summary>
        /// <returns></returns>
        public async Task<SeedPhraseViewResponseModel> GetSeedPhraseList()
        {
            SeedPhraseViewResponseModel seedPhraseResponse = null;
            try
            {
                var response = await serviceHelpers.PostRequest(string.Empty, StringConstant.SaveSeedsAPI, false, string.Empty);
                if (response.IsSuccess)
                {
                    seedPhraseResponse = JsonConvert.DeserializeObject<SeedPhraseViewResponseModel>(response.Data);
                }
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return seedPhraseResponse;
        }
    }
}
