using System;

namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for api response
    /// </summary>
    public class APIResponseModel
    {
        public bool result { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public string error { get; set; }        
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
