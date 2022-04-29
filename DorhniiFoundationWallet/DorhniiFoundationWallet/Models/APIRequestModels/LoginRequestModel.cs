using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class LoginRequestModel
    {
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("userId")]
        public string UserID { get; set; }
    }
}
