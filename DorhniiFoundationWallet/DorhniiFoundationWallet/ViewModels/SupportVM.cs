using DorhniiFoundationWallet.Helpers;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for support functionality.
    /// </summary>
    public class SupportVM : ObservableObject
    {
        #region Properties
        /// <summary>
        /// This property gets and sets the command for Back Button.
        /// </summary>
        public ICommand BackButtonCommand { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// This method is used for support assistance for the user.
        /// </summary>
        public SupportVM()
        {
            BackButtonCommand = new Command(BackButtonClick);
        }

        /// <summary>
        /// This method is used to click on Back Button
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
