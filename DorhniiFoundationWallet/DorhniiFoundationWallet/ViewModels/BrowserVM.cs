using DorhniiFoundationWallet.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace DorhniiFoundationWallet.ViewModels
{
    public class BrowserVM : ObservableObject
    {
        private string swapUrl;

        public string SwapUrl
        {
            get => swapUrl;
            set
            {
                if (swapUrl != value)
                {
                    swapUrl = value;
                    OnPropertyChanged(nameof(SwapUrl));
                }
            }
        }

        public BrowserVM()
        {
            SwapUrl = Preferences.Get("SwapUrl", string.Empty);
        }
    }
}
