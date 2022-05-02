using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class StakedDetailResponseModel
    {
        [JsonProperty("stakeDbId")]
        public string StakeDbId { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("stakeMatured")]
        public bool StakeMatured { get; set; }
        [JsonProperty("token")]
        public double Token { get; set; }
        [JsonProperty("stakePeriod")]
        public string StakePeriod { get; set; }
    }
}
