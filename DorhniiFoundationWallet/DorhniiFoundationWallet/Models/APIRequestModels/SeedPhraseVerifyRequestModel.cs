using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    /// <summary>
    /// This class use to define class properties for validate seed phrase
    /// </summary>
    public class SeedPhraseVerifyRequestModel
    {
        public string seedId { get; set; }
        public List<SeedPhras> seedPhrases { get; set; }
    }
    public class SeedPhras
    {
        public int id { get; set; }
        public string val { get; set; }
    }
}
