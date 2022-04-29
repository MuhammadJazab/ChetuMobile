
using DorhniiFoundationWallet.DependencyServices;
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
            #region            
            bool IsAcountExist = Preferences.Get(StringConstant.AcountFlag, false);
            MainPage = IsAcountExist ? new NavigationPage(new LoginPage()) : new NavigationPage(new WelcomePage());
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
    }
}
