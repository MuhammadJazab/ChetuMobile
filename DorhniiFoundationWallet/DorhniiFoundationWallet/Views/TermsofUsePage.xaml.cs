
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsofUsePage : ContentPage
    {
        public TermsofUsePage()
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