using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
   public class GetStakedResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public List<StakedDetailResponseModel> Data { get; set; }
    }
   
}
