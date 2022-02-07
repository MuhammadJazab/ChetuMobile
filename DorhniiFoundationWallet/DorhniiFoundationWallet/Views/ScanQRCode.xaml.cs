using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.ViewModels;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanQRCode : ContentPage
    {       
        public ScanQRCode()
        {
            InitializeComponent();
        }
        /// <summary>
        /// QR code Scanner method 
        /// </summary>
        /// <param name="result"></param>
        public void QrCoderesult(ZXing.Result result)
        {
            try
            {
                scanner.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    scanresultText.Text = result.Text + "(type :" + result.BarcodeFormat.ToString() + ")";
                    Utilities.Scannedtext = result.Text;
                    //_ = vm.SendCommandAsync(result.Text);
                    Application.Current.MainPage.DisplayAlert(Resource.txtWalletAddress, result.Text, Resource.txtOk);
                    Application.Current.MainPage.Navigation.PopModalAsync();                  
                });
                scanner.IsScanning = true;
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
                    
        }
    }
}