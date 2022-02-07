using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.Models
{
    public class StakesListModel
    {
        public string StakeName { get; set; }
        public string StakeAmount { get; set; }
        public string StakePeriod { get; set; }
        public string StakePercentage { get; set; }
        public ImageSource Image { get; set; }
    }
}
