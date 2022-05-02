using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class StakeTokenOverViewRequestModel
    {
        [JsonProperty("blockChain")]
        public string BlockChain { get; set; }
        [JsonProperty("walletAddress")]
        public string WalletAdress { get; set; }
    }
}
