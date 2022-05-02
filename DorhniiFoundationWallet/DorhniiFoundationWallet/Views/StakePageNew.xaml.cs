using DorhniiFoundationWallet.CustomRenderers;
using DorhniiFoundationWallet.Resources;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StakePageNew : ContentPage
    {
        public StakePageNew()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

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
        /// <summary>
        /// Method used to Fix the Provide number format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StakeEntry_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                var entry = sender as RendererEntry;
                var amnt = Convert.ToDouble(entry.Text).ToString("n2");
                StakePageVM.StakeAmountEntry = (amnt);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private void StakeEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            double parsedValue;
            var entry = sender as RendererEntry;

            if (!double.TryParse(entry.Text, out parsedValue))
            {
                entry.Text = "";
            }

        }
    }
}