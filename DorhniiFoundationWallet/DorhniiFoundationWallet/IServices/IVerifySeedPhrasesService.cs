using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to verify the seed phrases.
    /// </summary>
    interface IVerifySeedPhrasesService
    {
        /// <summary>
        /// This task is used to verify the seed phrases.
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns></returns>
        Task<ResponseModel> VerifySeedPhrase(SeedPhraseVerifyRequestModel loginRequestModel);
    }
}
