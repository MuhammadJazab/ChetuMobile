using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for tabbed navigation and tab icons.
    /// </summary>
    public class TabbedViewVM : ObservableObject
    {
        #region Properties
        #region Private Properties
        private string walletIcon;
        private string stakesIcon;
        private string transactionIcon;
        private string settingsIcon;
        #endregion

        #region Public Properties
        /// <summary>
        /// This property gets and sets the image icon of Wallet Tab Icon.
        /// </summary>
        public string WalletBlack
        {
            get { return walletIcon; }
            set
            {
                walletIcon = value;
                OnPropertyChanged(nameof(walletIcon));
            }
        }

        /// <summary>
        /// This property gets and sets the image icon of Stakes Tab Icon.
        /// </summary>
        public string StakesBlack
        {
            get { return stakesIcon; }
            set
            {
                stakesIcon = value;
                OnPropertyChanged(nameof(stakesIcon));
            }
        }

        /// <summary>
        /// This property gets and sets the image icon of Transaction Tab Icon.
        /// </summary>
        public string TransactionBlack
        {
            get { return transactionIcon; }
            set
            {
                transactionIcon = value;
                OnPropertyChanged(nameof(transactionIcon));
            }
        }

        /// <summary>
        /// This property gets and sets the image icon of Settings Tab Icon.
        /// </summary>
        public string SettingsBlack
        {
            get { return settingsIcon; }
            set
            {
                settingsIcon = value;
                OnPropertyChanged(nameof(settingsIcon));
            }
        }
        #endregion

        #region Command Properties
        /// <summary>
        /// This property gets and sets the command for Wallet Tab Icon.
        /// </summary>
        public ICommand WalletBlackCommand { get; set; }

        /// <summary>
        /// This property gets and sets the command for Stakes Tab Icon.
        /// </summary>
        public ICommand StakesBlackCommand { get; set; }

        /// <summary>
        /// This property gets and sets the command for Transaction Tab Icon.
        /// </summary>
        public ICommand TransactionBlackCommand { get; set; }

        /// <summary>
        /// This property gets and sets the command for Settings Tab Icon.
        /// </summary>
        public ICommand SettingsBlackCommand { get; set; }
        #endregion
        #endregion

        #region Methods
        /// <summary>
        /// This method is used for tabbed navigation and tab icons.
        /// </summary>
        public TabbedViewVM()
        {
            WalletBlack = StringConstant.WalletBlack;
            StakesBlack = StringConstant.StakesBlack;
            TransactionBlack = StringConstant.TransactionBlack;
            SettingsBlack = StringConstant.SettingsBlack;
            var tabKeyId = Preferences.Get(StringConstant.TabKeyId, 0);
            switch (tabKeyId)
            {
                case 0:
                case 1:
                    WalletBlack = StringConstant.WalletGray;
                    StakesBlack = StringConstant.StakesBlack;
                    TransactionBlack = StringConstant.TransactionBlack;
                    SettingsBlack = StringConstant.SettingsBlack;
                    break;
                case 2:
                    WalletBlack = StringConstant.WalletBlack;
                    StakesBlack = StringConstant.StakesGray;
                    TransactionBlack = StringConstant.TransactionBlack;
                    SettingsBlack = StringConstant.SettingsBlack;
                    break;
                case 3:
                    WalletBlack = StringConstant.WalletBlack;
                    StakesBlack = StringConstant.StakesBlack;
                    TransactionBlack = StringConstant.TransactionGray;
                    SettingsBlack = StringConstant.SettingsBlack;
                    break;
                case 4:
                    WalletBlack = StringConstant.WalletBlack;
                    StakesBlack = StringConstant.StakesBlack;
                    TransactionBlack = StringConstant.TransactionBlack;
                    SettingsBlack = StringConstant.SettingsGray;
                    break;
            }
            WalletBlackCommand = new Command(WalletBlackButtonClick);
            StakesBlackCommand = new Command(StakesBlackButtonClick);
            TransactionBlackCommand = new Command(TransactionBlackButtonClick);
            SettingsBlackCommand = new Command(SettingsBlackButtonClick);
        }

        /// <summary>
        /// This method is used to navigate to Wallet Tab page.
        /// </summary>
        public async void WalletBlackButtonClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 1);
            Application.Current.MainPage = new NavigationPage(new WalletPage());
        }

        /// <summary>
        /// This method is used to navigate to Stakes Tab page.
        /// </summary>
        public async void StakesBlackButtonClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 2);
            Application.Current.MainPage = new NavigationPage(new StakesPage());
        }

        /// <summary>
        /// This method is used to navigate to Transaction Tab page.
        /// </summary>
        public async void TransactionBlackButtonClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 3);
            Application.Current.MainPage = new NavigationPage(new TransactionPage());
        }

        /// <summary>
        /// This method is used to navigate to Settings Tab page.
        /// </summary>
        public async void SettingsBlackButtonClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 4);
            Application.Current.MainPage = new NavigationPage(new SettingsPage());
        }
        #endregion
    }
}
