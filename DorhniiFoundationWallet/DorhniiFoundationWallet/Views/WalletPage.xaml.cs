using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Resources;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletPage : ContentPage
    {
        internal static bool Loader;

        public WalletPage()
        {
            InitializeComponent();
        }
        //public void QrCoderesult(ZXing.Result result)
        //{
        //    try
        //    {
        //        scanner.IsScanning = false;
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            scanresultText.Text = result.Text + "(type :" + result.BarcodeFormat.ToString() + ")";
        //            Utilities.Scannedtext = result.Text;
        //            //_ = vm.SendCommandAsync(result.Text);
        //            Application.Current.MainPage.DisplayAlert(Resource.txtWalletAddress, result.Text, Resource.txtOk);
        //            Application.Current.MainPage.Navigation.PopAsync();
        //        });
        //        scanner.IsScanning = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Crashes.TrackError(ex);
        //    }

        //}
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                Frame frame = sender as Frame;
                WalletListModel selectedItem = (WalletListModel)frame.BindingContext;
                Utilities.WalletList = selectedItem;
                WalletPageVM.CoinNameClick(selectedItem);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {
            try
            {
                Label label = sender as Label;
                WalletListModel selectedItem = (WalletListModel)label.BindingContext;
                WalletPageVM.CoiName2Click(selectedItem);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private void TapGestureRecognizer_Tapped_2(object sender, System.EventArgs e)
        {
            try
            {
                Image image = sender as Image;
                WalletListModel selectedItem = (WalletListModel)image.BindingContext;
                WalletPageVM.SendAppIconClick(selectedItem);              
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private void TapGestureRecognizer_Tapped_3(object sender, System.EventArgs e)
        {
            try
            {
                Image image = sender as Image;
                WalletListModel selectedItem = (WalletListModel)image.BindingContext;
                WalletPageVM.ReceiveAppIconlick(selectedItem);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public void TapGestureRecognizer_Tapped_6(object sender, System.EventArgs e)
        {
            try
            {
                Image image = sender as Image;
                WalletListModel selectedItem = (WalletListModel)image.BindingContext;
                WalletPageVM.CloseReceivPageCommandAsync(selectedItem);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
        /// <summary>
        /// back button clicked on send page
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        public async void BackBtn_Clicked(Object Sender, EventArgs args)
        {
            try
            {
                Utilities.WalletList.IsSendPageVisible = false;
                Utilities.WalletList.IsCoinDetailVisible = true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Send button clicked on send page
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        public async void SendBtn_Clicked(Object Sender, EventArgs args)
        {
            try
            {
                WalletListModel walletListModel = new WalletListModel();
                await WalletPageVM.SendCommandAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        ///
        /// method
        private async void TapGestureRecognizer_Tapped_7(object sender, System.EventArgs e)
        {
            try
            {
                Image image = sender as Image;
                WalletListModel selectedItem =(WalletListModel)image.BindingContext;
                //await WalletPageVM.ScanQR(selectedItem);
                await WalletPageVM.ScanQR(selectedItem);


            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
    }
}