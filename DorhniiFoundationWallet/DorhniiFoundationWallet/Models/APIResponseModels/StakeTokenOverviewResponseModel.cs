using Newtonsoft.Json;
using System.Collections.Generic;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public  class StakeTokenOverviewResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public List<StakeTokenOverViewDatailResponseModel> Data { get; set; }
    }
}
