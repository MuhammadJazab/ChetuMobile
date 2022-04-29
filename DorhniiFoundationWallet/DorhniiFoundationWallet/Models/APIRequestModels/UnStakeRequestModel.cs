using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIRequestModels
{
    public class UnStakeRequestModel
    {
        [JsonProperty("blockChain")]
        public string BlockChain { get; set; }
        [JsonProperty("walletAddress")]
        public string WalletAdress { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("stakePeriod")]
        public string StakePeriod { get; set; }
        [JsonProperty("encryptedPrivateKey")]
        public string EncryptedPrivateKey { get; set; }
        [JsonProperty("stakeDbId")]
        public string StakeDbId { get; set; }
    }
}
