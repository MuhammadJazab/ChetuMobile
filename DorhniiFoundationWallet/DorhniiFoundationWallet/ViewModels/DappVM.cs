using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using WalletConnectSharp.Core;
using WalletConnectSharp.Core.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    public class DappVM : ObservableObject
    {
        #region Private Properties             
        private string urlText;
        private bool isBoxEnable;
        #endregion
        #region Image String Properties        
        public string SearchIcon { get; set; } = StringConstant.SearchIcon;

        #endregion
        #region Public Properties   
        public ICommand PancakeSwapCommand { get; set; }
        public ICommand GoCommand { get; set; }
        public ICommand UniSwapCommand { get; set; }
        public ICommand WCScanCommand { get; set; }

        public string UrlText
        {
            get => urlText;
            set
            {
                urlText = value;
                OnPropertyChanged(nameof(UrlText));
            }


        }

        #endregion
        #region Methods
        public DappVM()
        {

            try
            {
                WCScanCommand = new Command(WCScanClick);
                UniSwapCommand = new Command(UniSwapClick);
                GoCommand = new Command(GoPressed);
                PancakeSwapCommand = new Command(PancakeSwapClick);

                Application.Current.PageAppearing += Current_PageAppearing;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private void Current_PageAppearing(object sender, Page e)
        {
            UrlText = string.Empty;
        }

        /// <summary>
        /// Method use to navigate to pancakeswap
        /// </summary>
        public async void PancakeSwapClick()
        {
            try
            {
                Preferences.Set("SwapUrl", "https://pancakeswap.finance/swap");
                await Application.Current.MainPage.Navigation.PushModalAsync(new BrowserPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method use to navigate to typed url
        /// </summary>
        public async void GoPressed()
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlText))
                {
                    string originalUrl = UrlText;
                    if (UrlText.Length >= 5)
                    {
                        if (UrlText.ToLower().Substring(0, 5) != "https")
                        {
                            urlText = $"https://{UrlText}";
                        }
                    }
                    else
                    {
                        urlText = $"https://{UrlText}";
                    }

                    if (ValidateUrl(UrlText))
                    {
                        Preferences.Set("SwapUrl", UrlText);
                    }
                    else
                    {
                        UrlText = $"https://www.google.com/search?q={originalUrl}";
                        Preferences.Set("SwapUrl", UrlText);
                    }
                    await Application.Current.MainPage.Navigation.PushModalAsync(new BrowserPage());
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private bool ValidateUrl(string URL)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(URL);
        }

        /// <summary>
        /// Method use to navigate to UniSwap
        /// </summary>
        public async void UniSwapClick()
        {
            try
            {
                Preferences.Set("SwapUrl", "https://app.uniswap.org/#/swap");
                await Application.Current.MainPage.Navigation.PushModalAsync(new BrowserPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }

        public async void WCScanClick()
        {
            try
            {
                ZXingScannerPage scanPage = new ZXingScannerPage();
                scanPage.IsScanning = true;
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;
                    Web3 web3;
                    Device.BeginInvokeOnMainThread(async () =>
                   {
                       await Application.Current.MainPage.Navigation.PopModalAsync();

                       //Not sure what to do with the result here
                       await Application.Current.MainPage.DisplayAlert("barcode", result.Text, "Ok");

                       //Just testing out stuff here
                       var clientMeta = new ClientMeta()
                       {
                           Name = "Dohrnii Wallet",
                           Description = "Dohrnii wallet app",
                           Icons = new[] { "https://app.warriders.com/favicon.ico" },
                           URL = "https://app.warriders.com/",
                       };

                       var client = new WalletConnect(clientMeta);

                       Debug.WriteLine("Connect using the following URL");
                       Debug.WriteLine(client.URI);

                       //This is where am getting the error from
                       await client.Connect();

                       web3 = new Web3(client.CreateProvider(new Uri("https://mainnet.infura.io/v3/65e19a00a52a44fcb9b9f1f8aadbb4e3")));
                   });
                };
                await Application.Current.MainPage.Navigation.PushModalAsync(scanPage);

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        #endregion
    }
}
