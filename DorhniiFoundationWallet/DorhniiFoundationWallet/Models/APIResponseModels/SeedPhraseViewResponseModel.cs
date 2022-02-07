using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class SeedPhraseViewResponseModel : APIResponseModel
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("seedPhrases")]
        public List<SeedPhraseModel> SeedPhrases { get; set; }
        [JsonProperty("isVerified")]
        public string IsVerified { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
    public class SeedPhraseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("val")]
        public string Val { get; set; }
    }
}
