using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{/// <summary>
/// This Claas is used to swap token functionality
/// </summary>
    public class SwapViewModel : BaseViewModel
    {
        #region private Properties
        private string userEnteredUrl;
        #endregion
        #region public properties
        public string Vexchange { get; set; } = StringConstant.Vexchange;
        public string VeRocket { get; set; } = StringConstant.VeRocket;
        public string UserEnteredUrl
        {
            get => userEnteredUrl;
            set
            {
                userEnteredUrl = value;
                OnPropertyChanged(nameof(UserEnteredUrl));
               Preferences.Set(StringConstant.UserEnteredUrl, UserEnteredUrl);
            }
        }
        #endregion
        #region Command Properties
        public ICommand VexchangeCommand { get; set; }
        public ICommand VeRocketCommand { get; set; }
        #endregion
        #region Public Method
        /// <summary>
        /// Class constructor Method
        /// </summary>
        public SwapViewModel()
        {
            VexchangeCommand = new Command(VexchnageClickAsync);
            VeRocketCommand = new Command(VeRocketClick);
        }

        /// <summary>
        /// This Method is used to Switch to Vexchange dapp
        /// </summary>
        public async void VexchnageClickAsync()
        {
            UserEnteredUrl = StringConstant.VexchanageLink;            
            await Application.Current.MainPage.Navigation.PushModalAsync( new BrowserPage());
        }
        /// <summary>
        /// This Method is used to Switch to VeRocket dapp
        /// </summary>
        public async void VeRocketClick()
        {
            UserEnteredUrl = StringConstant.VeRocketLink;            
            await Application.Current.MainPage.Navigation.PushModalAsync(new BrowserPage());
        }
        #endregion
    }
}
