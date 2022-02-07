using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used to display the stakes list and stakes added list
    /// </summary>
    public class StakesVM : ObservableObject
    {
        #region Properties
        #region Command Properties       
        public ICommand WalletBlackCommand { get; set; }      
        public ICommand StakesBlackCommand { get; set; }       
        public ICommand TransactionBlackCommand { get; set; }
        /// <summary>
        /// This property gets and sets the command for Settings Tab Icon.
        /// </summary>
        public ICommand SettingsBlackCommand { get; set; }
        #endregion

        #region Private Boolean Properties
        private bool walletBlack;
        private bool walletgray;
        private bool stakesblack;
        private bool stakesgray;
        private bool transactionblack;
        private bool transactiongray;
        private bool settingsblack;
        private bool settingsgray;
        #endregion

        #region Bindable Properties
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
            get => stakesblack;
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
            get => stakesgray;
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
            get => transactiongray;
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
        /// This property gets sets the visiblity of Settings tab Button
        /// </summary>
        public bool SettingsBlackVisible
        {
            get => settingsblack;
            set
            {
                if (settingsblack != value)
                {
                    settingsblack = value;
                    OnPropertyChanged();
                }
            }
        }       
        public bool SettingGrayVisible
        {
            get => settingsgray;
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
        #region List Properties        
        public ObservableCollection<StakesListModel> StakesAddedList { get; set; }       
        public ObservableCollection<StakesListModel> StakesList { get; set; }
        #endregion
        #endregion
        #region Methods
        /// <summary>
        /// This method is used to display the stakes list and stakes added list
        /// </summary>
        public StakesVM()
        {
            try
            {
                WalletBlackVisible = true;
                StakesBlackVisible = false;
                TransactionBlackVisible = true;
                SettingsBlackVisible = true;
                WalletGrayVisible = false;
                StakesGrayVisible = true;
                TransactionGrayVisible = false;
                SettingGrayVisible = false;
                WalletBlackCommand = new Command(WalletBlackButtonClick);
                StakesBlackCommand = new Command(StakesBlackButtonClick);
                TransactionBlackCommand = new Command(TransactionBlackButtonClick);
                SettingsBlackCommand = new Command(SettingBlackButtonClick);
                StakesAddedList = new ObservableCollection<StakesListModel>();
                StakesList = new ObservableCollection<StakesListModel>();
                StakesAddedList.Add(new StakesListModel() { StakeName = "My Stake 1", StakeAmount = "12,345 $ DHN", StakePeriod = "Open", StakePercentage = "5%", Image = ImageSource.FromFile("AppIconImage.png") });
                StakesAddedList.Add(new StakesListModel() { StakeName = "My Stake 1", StakeAmount = "12,345 $ DHN", StakePeriod = "Open", StakePercentage = "5%", Image = ImageSource.FromFile("AppIconImage.png") });
                StakesAddedList.Add(new StakesListModel() { StakeName = "My Stake 1", StakeAmount = "12,345 $ DHN", StakePeriod = "Open", StakePercentage = "5%", Image = ImageSource.FromFile("AppIconImage.png") });
                StakesList.Add(new StakesListModel() { StakeName = "My Stake 1", StakeAmount = "12,345 $ DHN", StakePeriod = "3 Months", StakePercentage = "10 %", Image = ImageSource.FromFile("AppIconImage.png") });
                StakesList.Add(new StakesListModel() { StakeName = "My Stake 2", StakeAmount = "67,890 $ DHN", StakePeriod = "6 Months", StakePercentage = "17 %", Image = ImageSource.FromFile("AppIconImage.png") });
                StakesList.Add(new StakesListModel() { StakeName = "My Stake 3", StakeAmount = "54,321 $ DHN", StakePeriod = "1 Year", StakePercentage = "24 %", Image = ImageSource.FromFile("AppIconImage.png") });
                StakesList.Add(new StakesListModel() { StakeName = "My Stake 4", StakeAmount = "99,876 $ DHN", StakePeriod = "2 Years", StakePercentage = "31 %", Image = ImageSource.FromFile("AppIconImage.png") });
                StakesList.Add(new StakesListModel() { StakeName = "My Stake 5", StakeAmount = "12,345 $ DHN", StakePeriod = "4 Years", StakePercentage = "38 %", Image = ImageSource.FromFile("AppIconImage.png") });
                StakesList.Add(new StakesListModel() { StakeName = "My Stake 6", StakeAmount = "54,321 $ DHN", StakePeriod = "8 Years", StakePercentage = "45 %", Image = ImageSource.FromFile("AppIconImage.png") });
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Wallet Tab Button
        /// </summary>
        public async void WalletBlackButtonClick()
        {
            try
            {
                WalletBlackVisible = false;
                StakesBlackVisible = true;
                TransactionBlackVisible = true;
                SettingsBlackVisible = true;

                WalletGrayVisible = true;
                StakesGrayVisible = false;
                TransactionGrayVisible = false;
                SettingGrayVisible = false;
                await Application.Current.MainPage.Navigation.PushModalAsync(new WalletPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Stakes Tab Button
        /// </summary>
        public async void StakesBlackButtonClick()
        {
            try
            {
                WalletBlackVisible = true;
                StakesBlackVisible = false;
                TransactionBlackVisible = true;
                SettingsBlackVisible = true;

                WalletGrayVisible = false;
                StakesGrayVisible = true;
                TransactionGrayVisible = false;
                SettingGrayVisible = false;
                await Application.Current.MainPage.Navigation.PushModalAsync(new StakesPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Transaction Tab Button
        /// </summary>
        public async void TransactionBlackButtonClick()
        {
            try
            {
                WalletBlackVisible = true;
                StakesBlackVisible = true;
                TransactionBlackVisible = false;
                SettingsBlackVisible = true;

                WalletGrayVisible = false;
                StakesGrayVisible = false;
                TransactionGrayVisible = true;
                SettingGrayVisible = false;
                await Application.Current.MainPage.Navigation.PushModalAsync(new TransactionPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to click on Settings Tab Button
        /// </summary>
        public async void SettingBlackButtonClick()
        {
            try
            {
                WalletBlackVisible = true;
                StakesBlackVisible = true;
                TransactionBlackVisible = true;
                SettingsBlackVisible = false;
                WalletGrayVisible = false;
                StakesGrayVisible = false;
                TransactionGrayVisible = false;
                SettingGrayVisible = true;
                await Application.Current.MainPage.Navigation.PushModalAsync(new SettingPageNew());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
