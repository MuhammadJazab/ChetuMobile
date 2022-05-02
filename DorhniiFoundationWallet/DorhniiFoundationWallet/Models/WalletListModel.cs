using DorhniiFoundationWallet.Helpers;
namespace DorhniiFoundationWallet.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletListModel : InotifyPropertyChanged
    {
        public int CoinId { get; set; }                                                                         
        private int listId;
        public int ListId
        {
            get => listId;
            set
            {
                if (listId != value)
                {
                    listId = value;
                    OnPropertyChanged(nameof(ListId));
                }
            }
        }
        private string amount;
        public string Amount
        {
            get => amount;
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }
        private string gasAmount;
        public string GasAmount
        {
            get => gasAmount;
            set
            {
                if (gasAmount != value)
                {
                    gasAmount = value;
                    OnPropertyChanged(nameof(GasAmount));
                }
            }
        }
        private string gasUnit;
        public string GasUnit
        {
            get => gasUnit;
            set
            {
                if (gasUnit != value)
                {
                    gasUnit = value;
                    OnPropertyChanged(nameof(GasUnit));
                }
            }
        }
        private string walletAdressEntered;
        public string WalletAdressEntered
        {
            get => walletAdressEntered;
            set
            {
                if (walletAdressEntered != value)
                {
                    walletAdressEntered = value;
                    OnPropertyChanged(nameof(WalletAdressEntered));
                }
            }
        }
        public double Walletbalance { get; set; }
        public string CoinStandard { get; set; }
        public string CoinName { get; set; }
        public double CoinValue { get; set; }
        public string CoinValueString { get; set; }
        private string coinIcon;
        public string CoinIcon
        {
            get => coinIcon;
            set
            {
                if (coinIcon != value)
                {
                    coinIcon = value;
                    OnPropertyChanged(nameof(CoinIcon));
                }
            }
        }
        public string CoinShortName { get; set; }
        public string BlockChain { get; set; }
        public string WalletName { get; set; }
        public string WalletAdress { get; set; }
        public string QrCode { get; set; }
        public double CoinUsdValue { get; set; }
        public string CoinUsdValueString { get; set; }
        public string ShareApp48Icon { get; set; } = StringConstant.ShareApp48Icon;
        public string SendAppIcon { get; set; } = StringConstant.SendAppIcon;
        public string ReceiveAppIcon { get; set; } = StringConstant.ReceiveAppIcon;
        public string CrossSign { get; set; } = StringConstant.CrossSign;
        public string CopyAppIcon { get; set; } = StringConstant.CopyAppIcon;
        public string ScanAppIcon { get; set; } = StringConstant.ScanAppIcon;
        public string ScanImageButton { get; set; } = StringConstant.ScanImageButton; 
        private bool isCoinVisible;
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
        private bool isCoinDetailVisible;
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
        private bool isSendPageVisible;
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
        private bool isReceivePageVisible;
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
