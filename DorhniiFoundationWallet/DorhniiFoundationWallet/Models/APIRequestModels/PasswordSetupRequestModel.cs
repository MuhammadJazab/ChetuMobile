using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class PasswordSetupRequestModel
    {
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }
        [JsonProperty("restore")]
        public bool Restore { get; set; }
    }
}
