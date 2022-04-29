using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class StakeTokenOverViewDatailResponseModel
    {
        [JsonProperty("stakePeriod")]
        public string StakePeriod { get; set; }
        [JsonProperty("token")]
        public double Token { get; set; }
        [JsonProperty("rewardPercentage")]
        public int RewardPercentage { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
