using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet
{
    public partial class App : Application
    {
       public App()
       {
            InitializeComponent();
            Preferences.Set(StringConstant.TabKeyId, 1);
            MainPage = new NavigationPage(new WalletPage());
            #region
            string password = Preferences.Get(StringConstant.DevicePassword, string.Empty);
            if (!string.IsNullOrEmpty(password))
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
            else
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
            #endregion
       }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);
            if (uri.Scheme != null)
            {
                Debug.WriteLine("Worked!");
            }
        }
    }
}
