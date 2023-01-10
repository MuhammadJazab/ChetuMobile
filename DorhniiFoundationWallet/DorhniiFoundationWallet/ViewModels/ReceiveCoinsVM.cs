using DorhniiFoundationWallet.Helpers;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for receiving coins functionality.
    /// </summary>
    public class ReceiveCoinsVM : ObservableObject
    {
        #region public Properties        
        public ICommand BackButtonCommand { get; set; }
        #endregion
        #region Methods
        /// <summary>
        ///class constructor Method to click on Back Button
        /// </summary>       
        public ReceiveCoinsVM()
        {
            try
            {
                BackButtonCommand = new Command(BackButtonClick);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
            
        }

        /// <summary>
        ///Method to click on Back Button
        /// </summary>       
        public async void BackButtonClick()
        {
            try
            {
                _ = await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

    }
}
