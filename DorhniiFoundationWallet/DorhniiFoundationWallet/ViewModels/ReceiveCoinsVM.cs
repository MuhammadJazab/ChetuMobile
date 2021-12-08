using DorhniiFoundationWallet.Helpers;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for receiving coins functionality.
    /// </summary>
    public class ReceiveCoinsVM : ObservableObject
    {
        #region Properties
        /// <summary>
        /// This command property is used for back button click event.
        /// </summary>
        public ICommand BackButtonCommand { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// This method is used for receiving coins functionality.
        /// </summary>
        public ReceiveCoinsVM()
        {
            BackButtonCommand = new Command(BackButtonClick);
        }

        /// <summary>
        ///Method to click on Back Button
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
