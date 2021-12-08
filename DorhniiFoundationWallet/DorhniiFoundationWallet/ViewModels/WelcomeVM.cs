using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used during the welcome screen.
    /// </summary>
    public class WelcomeVM : ObservableObject
    {
        #region Properties

        #region Public Properties
        /// <summary>
        /// This property gets or sets the image of the wallet lists.
        /// </summary>
        public string AppIcon { get; set; } = StringConstant.AppIcon;

        /// <summary>
        /// This property gets or sets the command for creating accounts.
        /// </summary>
        public ICommand CreateButtonCommand { get; set; }

        /// <summary>
        /// This property gets or sets the command for restoring accounts.
        /// </summary>
        public ICommand RestoreButtonCommand { get; set; }
        #endregion

        #endregion

        #region Methods
        /// <summary>
        /// This method is used to create and restore accounts.
        /// </summary>
        public WelcomeVM()
        {
            CreateButtonCommand = new Command(CreateButtonClick);
            RestoreButtonCommand = new Command(RestoreButtonClick);
        }

        /// <summary>
        /// This method is used to click on Create Button
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public async void CreateButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new PasswordSetupPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This method is used to click on Restore Button
        /// </summary>
        public async void RestoreButtonClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SeedPhraseEntry());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
