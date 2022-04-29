using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public  class SupportPageRequestModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("walletAddress")]
        public string WalletAddress { get; set; }
        [JsonProperty("usermailId")]
        public string UsermailId { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }

    }
}
