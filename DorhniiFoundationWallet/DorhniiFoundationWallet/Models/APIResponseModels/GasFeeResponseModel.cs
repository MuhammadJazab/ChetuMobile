namespace DorhniiFoundationWallet.Models.APIResponseModels
{
    public class GasFeeResponseModel
    {
        public bool Result { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string GasConsumed { get; set; }
        public string GasUnit { get; set; }    
    }
}
