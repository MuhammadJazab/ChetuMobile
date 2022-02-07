using Newtonsoft.Json;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class SeedPhraseVerifyRequestModel
    {
        [JsonProperty("seedId")]
        public string SeedId { get; set; }
        [JsonProperty("seedPhrases")]
        public List<SeedPhras> SeedPhrases { get; set; }
    }
    public class SeedPhras
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("val")]
        public string Val { get; set; }
    }
}
