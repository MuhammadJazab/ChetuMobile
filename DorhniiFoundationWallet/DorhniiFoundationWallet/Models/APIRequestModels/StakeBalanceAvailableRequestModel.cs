using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
   public class StakeBalanceAvailableRequestModel
    {
        [JsonProperty("walletAddress")]
        public string WalletAddress { get; set; }
    }
}
