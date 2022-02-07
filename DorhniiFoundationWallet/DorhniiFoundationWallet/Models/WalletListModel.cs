using DorhniiFoundationWallet.Helpers;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletListModel : ObservableObject
    {
        private string walletAdressEntered;
        private int amount;
        private bool isScannerPageVisible;
        private bool isWalletListPage;
        private bool isCoinDetailVisible;
        private bool isSendPageVisible;
        private bool isCoinVisible;
        private bool isReceivePageVisible;
        public string WalletAdressEntered
        {
            get
            {
                return walletAdressEntered;
            }
            set
            {
                if (walletAdressEntered != value)
                {
                    walletAdressEntered = value;
                    OnPropertyChanged(nameof(WalletAdressEntered));
                }
            }
        }
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }     
        public string Walletbalance { get; set; }
        public string CoinStandard { get; set; }
        public string CoinName { get; set; }
        public string CoinValue { get; set; }
        public string CoinIcon { get; set; }
        public string CoinShortName { get; set; }
        public string WalletAdress { get; set; }
        public string QrCode { get; set; }
        public string CoinUsdValue { get; set; }        
        public string ShareApp48Icon { get; set; } = StringConstant.ShareApp48Icon;
        public string SendAppIcon { get; set; } = StringConstant.SendAppIcon;
        public string ReceiveAppIcon { get; set; } = StringConstant.ReceiveAppIcon;
        public string CrossSign { get; set; } = StringConstant.CrossSign;
        public string CopyAppIcon { get; set; } = StringConstant.CopyAppIcon;
        public string ScanAppIcon { get; set; } = StringConstant.ScanAppIcon;
        public bool IsScannerPageVisible
        {
            get => isScannerPageVisible;
            set
            {
                if (isScannerPageVisible != value)
                {
                    isScannerPageVisible = value;
                    OnPropertyChanged(nameof(IsScannerPageVisible));
                }
            }
        }
        public bool IsWalletListPage
        {
            get => isWalletListPage;
            set
            {
                if (isWalletListPage != value)
                {
                    isWalletListPage = value;
                    OnPropertyChanged(nameof(IsWalletListPage));
                }
            }
        }
        public bool IsCoinVisible
        {
            get => isCoinVisible;
            set
            {
                if (isCoinVisible != value)
                {
                    isCoinVisible = value;
                    OnPropertyChanged(nameof(IsCoinVisible));
                }
            }
        }
        public bool IsCoinDetailVisible
        {
            get => isCoinDetailVisible;
            set
            {
                if (isCoinDetailVisible != value)
                {
                    isCoinDetailVisible = value;
                    OnPropertyChanged(nameof(IsCoinDetailVisible));
                }
            }
        }
        public bool IsSendPageVisible
        {
            get => isSendPageVisible;
            set
            {
                if (isSendPageVisible != value)
                {
                    isSendPageVisible = value;
                    OnPropertyChanged(nameof(IsSendPageVisible));
                }
            }
        }
        public bool IsReceivePageVisible
        {

            get => isReceivePageVisible;
            set
            {
                if (isReceivePageVisible != value)
                {
                    isReceivePageVisible = value;
                    OnPropertyChanged(nameof(IsReceivePageVisible));
                }
            }
        }

    }
}
