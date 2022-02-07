using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyPolicy : ContentPage
    {
        public PrivacyPolicy()
        {
            InitializeComponent();
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            VM.IsLoading = false;
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            VM.IsLoading = true;
        }

    }
}