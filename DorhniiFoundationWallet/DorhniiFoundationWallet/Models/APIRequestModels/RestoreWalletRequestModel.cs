using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public  class RestoreWalletRequestModel
    {
        [JsonProperty("restoreType")]
        public string RestoreType { get; set; }
        [JsonProperty("restorePayload")]
        public string RestorePayload { get; set; }
    }
}
