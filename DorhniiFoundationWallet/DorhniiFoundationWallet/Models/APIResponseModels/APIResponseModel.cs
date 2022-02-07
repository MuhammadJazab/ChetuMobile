using Newtonsoft.Json;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for api response
    /// </summary>
    public class APIResponseModel
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
    /// <summary>
    /// Common class for all response from APIs
    /// </summary>
    public class ResponseModel : APIResponseModel
    {
        public bool IsSuccess { get; set; }        
        public string Data { get; set; }
        public int Code { get; set; }
    }
}
