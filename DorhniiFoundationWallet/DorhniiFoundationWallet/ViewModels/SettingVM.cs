using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used to display the settings page and its functionalities
    /// </summary>
    public class SettingVM : ObservableObject
    {
        #region Properties

        #region Boolean Properties
        private bool walletBlack;
        private bool walletgray;
        private bool stakesblack;
        private bool stakesgray;
        private bool transactionblack;
        private bool transactiongray;
        private bool settingsblack;
        private bool settingsgray;
        #endregion

        #region Image String Properties
        /// <summary>
        /// This property gets or sets the image of the support icon.
        /// </summary>
        public string SupportIcon { get; set; } = StringConstant.SupportIcon;

        /// <summary>
        /// This property gets or sets the image of the Privacy icon.
        /// </summary>
        public string PrivacyIcon { get; set; } = StringConstant.PrivacyIcon;

        /// <summary>
        /// This property gets or sets the image of the Terms icon.
        /// </summary>
        public string TermsIcon { get; set; } = StringConstant.TermsIcon;

        /// <summary>
        /// This property gets or sets the image of the Accounts icon.
        /// </summary>
        public string AccountsIcon { get; set; } = StringConstant.AccountsIcon;

        /// <summary>
        /// This property gets or sets the image of the Language icon.
        /// </summary>
        public string LanguageIcon { get; set; } = StringConstant.LanguageIcon;

        /// <summary>
        /// This property gets or sets the image of the Fingerprint icon.
        /// </summary>
        public string FingerprintIcon { get; set; } = StringConstant.FingerprintIcon;

        /// <summary>
        /// This property gets or sets the image of the ShowSeed icon.
        /// </summary>
        public string ShowSeedIcon { get; set; } = StringConstant.ShowSeedIcon;

        /// <summary>
        /// This property gets or sets the image of the Logout icon.
        /// </summary>
        public string LogoutIcon { get; set; } = StringConstant.LogoutIcon;
        #endregion

        /// <summary>
        /// This property gets or sets the command for support click event.
        /// </summary>
        public ICommand SupportCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// This method is used to display the settings page and its functionalities
        /// </summary>
        public SettingVM()
        {
            WalletBlackVisible = true;
            StakesBlackVisible = true;
            TransactionBlackVisible = true;
            SettingsBlackVisible = false;

            WalletGrayVisible = false;
            StakesGrayVisible = false;
            TransactionGrayVisible = false;
            SettingGrayVisible = true;
            SupportCommand = new Command(SupportClick);
        }

        /// <summary>
        /// Method to click on Support Button
        /// </summary>
        public async void SupportClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SupportPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool WalletBlackVisible
        {
            get
            {
                return walletBlack;
            }
            set
            {
                if (walletBlack != value)
                {
                    walletBlack = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Wallet tab Button
        /// </summary>
        public bool WalletGrayVisible
        {
            get
            {
                return walletgray;
            }
            set
            {
                if (walletgray != value)
                {
                    walletgray = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Stakes tab Button
        /// </summary>
        public bool StakesBlackVisible
        {
            get
            {
                return stakesblack;
            }
            set
            {
                if (stakesblack != value)
                {
                    stakesblack = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Stakes tab Button
        /// </summary>
        public bool StakesGrayVisible
        {
            get
            {
                return stakesgray;
            }
            set
            {
                if (stakesgray != value)
                {
                    stakesgray = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Transaction tab Button
        /// </summary>
        public bool TransactionBlackVisible
        {
            get
            {
                return transactionblack;
            }
            set
            {
                if (transactionblack != value)
                {
                    transactionblack = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Transaction tab Button
        /// </summary>
        public bool TransactionGrayVisible
        {
            get
            {
                return transactiongray;
            }
            set
            {
                if (transactiongray != value)
                {
                    transactiongray = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Setting tab Button
        /// </summary>
        public bool SettingsBlackVisible
        {
            get
            {
                return settingsblack;
            }
            set
            {
                if (settingsblack != value)
                {
                    settingsblack = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This property gets sets the visiblity of Setting tab Button
        /// </summary>
        public bool SettingGrayVisible
        {
            get
            {
                return settingsgray;
            }
            set
            {
                if (settingsgray != value)
                {
                    settingsgray = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
