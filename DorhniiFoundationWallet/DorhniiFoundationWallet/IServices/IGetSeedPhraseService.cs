using DorhniiFoundationWallet.Models.APIResponseModels;
using System.Threading.Tasks;

namespace DorhniiFoundationWallet.IServices
{
    /// <summary>
    /// This interface is used to get seeds phrases.
    /// </summary>
    public interface IGetSeedPhraseService
    {
        /// <summary>
        /// This task is used to get seeds phrases.
        /// </summary>
        /// <returns></returns>
        Task<SeedPhraseViewResponseModel> GetSeedPhraseList();
    }
}
