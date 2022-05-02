using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    public class TermsofUseVM : BaseViewModel
    {
        #region Public Class Properties
        public ICommand BackOnTermOfService { get; set; }
        #endregion

        /// <summary>
        /// This is a class constructor to initialize class property
        /// </summary>
        public TermsofUseVM()
        {
            BackOnTermOfService = new Command(BackOnTermOfServiceClick);
        }

        /// <summary>
        /// This Method is used too Come back on Terms of Service Page
        /// </summary>
        public async void BackOnTermOfServiceClick()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SettingPageNew());

        }
    }
}
