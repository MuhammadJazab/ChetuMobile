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
      
        #region Private Properties
        private string walletIcon;
        private string stakesIcon;
        private string transactionIcon;
        private string settingsIcon;
        #endregion
        
        #region Public Properties
        public string WalletIcon { get; set; }        
        public string StakesIcon { get; set; }
        public string SwapIcon { get; set; }
        public string TransactionIcon { get; set; }
        public string SettingsIcon { get; set; }       
        #endregion
        #region Command Properties
        public Color TabWalletColour { get; set; }
        public Color TabStakeColour { get; set; }
        public Color TabSwapColour { get; set; }
        public Color TabTransactionColour { get; set; }
        public Color TabSettingColour { get; set; }

        public ICommand SwapCommand { get; set; }        
        public ICommand WalletCommand { get; set; }      
        public ICommand StakesCommand { get; set; }       
        public ICommand TransactionCommand { get; set; }        
        public ICommand SettingsCommand { get; set; }
        #endregion
        

        #region Methods
        /// <summary>
        /// This Constructor method is used for tabbed navigation and tab icons.
        /// </summary>
        public TabbedViewVM()
        {
            TabWalletColour = Color.Transparent;
            TabStakeColour = Color.Transparent;
            TabSwapColour = Color.Transparent;
            TabTransactionColour = Color.Transparent;
            TabSettingColour = Color.Transparent;
            WalletIcon = StringConstant.WalletBlack;
            StakesIcon = StringConstant.StakesBlack;
            SwapIcon = StringConstant.SwapIcon;
            TransactionIcon = StringConstant.TransactionBlack;
            SettingsIcon = StringConstant.SettingsBlack;
            var tabKeyId = Preferences.Get(StringConstant.TabKeyId, 0);
            switch (tabKeyId)
            {
                case 0:
                case 1:
                    TabWalletColour = Color.FromHex("#552DF2");
                    TabStakeColour = Color.Transparent;
                    TabSwapColour = Color.Transparent;
                    TabTransactionColour = Color.Transparent;
                    TabSettingColour = Color.Transparent;                    
                    break;
                case 2:
                    TabWalletColour = Color.Transparent;
                    TabStakeColour = Color.FromHex("#552DF2");
                    TabSwapColour = Color.Transparent;
                    TabTransactionColour = Color.Transparent;
                    TabSettingColour = Color.Transparent;                    
                    break;
                case 3:
                    TabWalletColour = Color.Transparent;
                    TabStakeColour = Color.Transparent;
                    TabSwapColour = Color.FromHex("#552DF2");
                    TabTransactionColour = Color.Transparent;
                    TabSettingColour = Color.Transparent;                    
                    break;
                case 4:
                    TabWalletColour = Color.Transparent;
                    TabStakeColour = Color.Transparent;
                    TabSwapColour = Color.Transparent;
                    TabTransactionColour = Color.FromHex("#552DF2");
                    TabSettingColour = Color.Transparent;                   
                    break;
                case 5:
                    TabWalletColour = Color.Transparent;
                    TabStakeColour = Color.Transparent;
                    TabSwapColour = Color.Transparent;
                    TabTransactionColour = Color.Transparent;
                    TabSettingColour = Color.FromHex("552DF2");
                    break;
            }
            WalletCommand = new Command(WalletButtonClick);
            StakesCommand = new Command(StakesButtonClick);
            TransactionCommand = new Command(TransactionButtonClick);
            SettingsCommand = new Command(SettingButtonClick);
            SwapCommand = new Command(SwapCommandClick);
        }        
        /// <summary>
        /// This method is used to navigate to Wallet Tab page.
        /// </summary>
        public void WalletButtonClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 1);
            Application.Current.MainPage = new NavigationPage(new WalletPage());
        }

        /// <summary>
        /// This method is used to navigate to Stakes Tab page.
        /// </summary>
        public void StakesButtonClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 2);
            Application.Current.MainPage = new NavigationPage(new StakePageNew());
        }

        /// <summary>
        /// this method used to navigate swap page
        /// </summary>
        public void SwapCommandClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 3);
            Application.Current.MainPage = new NavigationPage(new DappPage());
        }


        /// <summary>
        /// This method is used to navigate to Transaction Tab page.
        /// </summary>
        public void TransactionButtonClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 4);
            Application.Current.MainPage = new NavigationPage(new TransactionPage());
        }

        /// <summary>
        /// This method is used to navigate to Settings Tab page.
        /// </summary>
        public void SettingButtonClick()
        {
            Preferences.Set(StringConstant.TabKeyId, 5);
            Application.Current.MainPage = new NavigationPage(new SettingPageNew());
        }
        #endregion
    }
}
