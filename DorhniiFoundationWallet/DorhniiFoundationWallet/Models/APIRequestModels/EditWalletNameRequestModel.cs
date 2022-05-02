using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class EditWalletNameRequestModel
    {
        [JsonProperty("walletAddress")]
        public string WalletAddress { get; set; }
        [JsonProperty("walletName")]
        public string WalletName { get; set; }
    }
}
