using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class ChangePasswordRequestModel
    {
        [JsonProperty("oldPassword")]
        public string OldPassword { get; set; }
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }



    }
}
