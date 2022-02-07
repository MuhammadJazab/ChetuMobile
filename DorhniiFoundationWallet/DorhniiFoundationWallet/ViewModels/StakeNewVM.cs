using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Models;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is related to Stake features
    /// </summary>
    public class StakeNewVM : ObservableObject
    {
        #region private properties
        private bool isRequestWithdrawl;
        private bool isStakeOptionVisible;
        private bool isScrollViewVissible;
        private bool isTakingStakeVisible;
        #endregion
        #region public properties
        public bool IsTakingStakeVisible
        {
            get => isTakingStakeVisible;
            set
            {
                if (isTakingStakeVisible != value)
                {
                    isTakingStakeVisible = value;
                    OnPropertyChanged(nameof(IsTakingStakeVisible));
                }
            }
        }
        public bool IsScrollViewVissible
        {
            get => isScrollViewVissible;
            set
            {
                if (isScrollViewVissible != value)
                {
                    isScrollViewVissible = value;
                    OnPropertyChanged(nameof(IsScrollViewVissible));
                }
            }
        }
        public bool IsRequestWithdrawl
        {
            get => isRequestWithdrawl;
            set
            {
                if (isRequestWithdrawl != value)
                {
                    isRequestWithdrawl = value;
                    OnPropertyChanged(nameof(IsRequestWithdrawl));
                }
            }
        }
        public bool IsStakeOptionVisible
        {
            get => isStakeOptionVisible;
            set
            {
                if (isStakeOptionVisible != value)
                {
                    isStakeOptionVisible = value;
                    OnPropertyChanged(nameof(IsStakeOptionVisible));
                }
            }
        }
        public ObservableCollection<StakesListModel> StakeList { get; set; }
        public string CopyAppIcon { get; set; } = StringConstant.CopyAppIcon;
        public ICommand StakeCommand { get; set; }
        public ICommand MangeCommand { get; set; }
        public ICommand TakeStakeCommand { get; set; }
        public ICommand BackOptionClick { get; set; }
        #endregion
        /// <summary>
        /// Constructor of This Class 
        /// </summary>
        public StakeNewVM()
        {
            try
            {
                StakeCommand = new Command(StakeCommandClick);
                MangeCommand = new Command(MangeCommandClick);
                TakeStakeCommand = new Command(TakeStakeCommandClick);
                BackOptionClick = new Command(BackOptionClickCommand);
                IsRequestWithdrawl = false;
                IsStakeOptionVisible = false;
                IsScrollViewVissible = true;
                IsTakingStakeVisible = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Back Button Command Method
        /// </summary>
        public void BackOptionClickCommand()
        {
            try
            {
                IsStakeOptionVisible = false;
                IsScrollViewVissible = true;
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }

           
        }

        /// <summary>
        /// Stake Button Command Method
        /// </summary>
        public void StakeCommandClick()
        {
            try
            {
                IsStakeOptionVisible = true;
                IsScrollViewVissible = false;
                IsRequestWithdrawl = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// manage button command Method
        /// </summary>
        public void MangeCommandClick()
        {
            try
            {
                IsRequestWithdrawl = true;
            }
            catch (Exception ex) { Crashes.TrackError(ex); }
        }

        /// <summary>
        /// Take stake command method
        /// </summary>
        public void TakeStakeCommandClick()
        {
            try
            {
                IsTakingStakeVisible = true;
            }

            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
