using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Resources;
using Microsoft.AppCenter.Crashes;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWalletPage : ContentPage
    {
        public AddWalletPage()
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


        private void RendererEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var isValid = Regex.IsMatch(e.NewTextValue, StringConstant.AddWalletEntryRegex);
                var isvalid2 = Regex.IsMatch(e.NewTextValue, StringConstant.AddWalletEntryRegex2);
                if (e.NewTextValue.Length <= 1)
                {
                    ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                }
                else
                {
                    ((Entry)sender).Text = isvalid2 ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
    }
}
