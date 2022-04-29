using DorhniiFoundationWallet.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwapPage : ContentPage
    {
        public SwapPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                bool res = await DisplayAlert(Resource.txtExitAlert, Resource.msgExit, Resource.txtYes, Resource.txtNo).ConfigureAwait(false);

                if (res)
                {
                    _ = System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        }

        private void SearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new BrowserPage());
        }
    }
}