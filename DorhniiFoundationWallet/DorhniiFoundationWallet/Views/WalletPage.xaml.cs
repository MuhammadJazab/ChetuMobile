using DorhniiFoundationWallet.CustomRenderers;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletPage : ContentPage
    {
        public WalletPage()
        {
            InitializeComponent();
        }

        private void AmountEntry_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                var entry = sender as RendererEntry;
                if (WalletPageVM.SelectedWallet.ListId == Convert.ToInt32(entry.ClassId))
                {
                    var amnt = Convert.ToDouble(entry.Text).ToString("n2");
                    WalletPageVM.SelectedWallet.Amount = (amnt);
                }
            }
            catch (Exception ex)
            {
               Crashes.TrackError(ex);
            }
        }

        //Method to accept only numeric value
        private void AmountEntry_TextChanged(object sender, TextChangedEventArgs e)
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
