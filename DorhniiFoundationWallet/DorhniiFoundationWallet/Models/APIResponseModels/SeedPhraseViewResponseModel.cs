using System;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for view seed phrase
    /// </summary>
    public class SeedPhraseViewResponseModel : APIResponseModel
    {
        public string _id { get; set; }
        public List<SeedPhraseModel> seedPhrases { get; set; }
        public string isVerified { get; set; }
        public DateTime date { get; set; }
    }
    public class SeedPhraseModel
    {
        public int id { get; set; }
        public string val { get; set; }

    }
}
