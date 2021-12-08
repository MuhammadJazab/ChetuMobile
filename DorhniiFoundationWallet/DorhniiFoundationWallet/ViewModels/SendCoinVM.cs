using DorhniiFoundationWallet.Helpers;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for sending coins transactions
    /// </summary>
    public class SendCoinVM : ObservableObject
    {
        #region Properties
        public ICommand BackButtonCommand { get; set; }

        #endregion

        #region Methods
        public SendCoinVM()
        {
            BackButtonCommand = new Command(BackButtonClick);
        }

        /// <summary>
        ///Method to click on Edit Button
        /// </summary>       
        public async void BackButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

    }
}
