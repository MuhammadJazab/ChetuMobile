using Xamarin.Forms;

namespace DorhniiFoundationWallet.Models
{
    /// <summary>
    /// This class use to define class properties for stake 
    /// </summary>
    public class StakesListModel
    {
        public string StakeName { get; set; }
        public string StakeAmount { get; set; }
        public string StakePeriod { get; set; }
        public string StakePercentage { get; set; }
        public ImageSource Image { get; set; }
    }
}
