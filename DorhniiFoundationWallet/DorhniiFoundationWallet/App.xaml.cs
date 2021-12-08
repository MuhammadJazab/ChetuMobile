using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
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
            MainPage = new NavigationPage(new WelcomePage());
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
    }
}
