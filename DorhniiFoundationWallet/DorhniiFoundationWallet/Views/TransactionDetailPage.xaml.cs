
using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetailPage : ContentPage
    {
        public TransactionDetailPage(string url)
        {
            InitializeComponent();
            TxDetailWeb.Source = url;
        }

        private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                await Task.Delay(2000);
                VM.IsLoading = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            try
            {
                VM.IsLoading = true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}