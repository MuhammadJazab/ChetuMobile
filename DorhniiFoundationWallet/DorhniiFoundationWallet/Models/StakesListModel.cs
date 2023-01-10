using DorhniiFoundationWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.Models
{
    public class StakesListModel : BaseViewModel
    {
        public string StakeName { get; set; }
        public string StakeAmount { get; set; }
        public string StakePeriod { get; set; }
        public string StakePercentage { get; set; }
        public ImageSource Image { get; set; }

        public double CoinValue { get; set; }
        public double CoinUsdValue { get; set; }
        public string CoinShortName { get; set; }        
        private string tokenMainBalance;
        public string TokenMainBalance
        {
            get => tokenMainBalance;
            set
            {
                if (tokenMainBalance != value)
                {
                    tokenMainBalance = value;
                    OnPropertyChanged(nameof(TokenMainBalance));
                }
            }
        }
        private string percentageAPR;
        public string PercentageAPR
        {
            get => percentageAPR;
            set
            {
                if (percentageAPR != value)
                {
                    percentageAPR = value;
                    OnPropertyChanged(nameof(PercentageAPR));
                }
            }
        }
        private string aprForStakePAge;
        public string AprForStakePAge
        {
            get => aprForStakePAge;
            set
            {
                if (aprForStakePAge != value)
                {
                    aprForStakePAge = value;
                    OnPropertyChanged(nameof(AprForStakePAge));
                }
            }
        }
        private string monthCount;
        public string MonthCount
        {
            get => monthCount;
            set
            {
                if (monthCount != value)
                {
                    monthCount = value;
                    OnPropertyChanged(nameof(MonthCount));
                }
            }
        }
        //private string stakePeriod;
        //public string StakePeriod
        //{
        //    get => stakePeriod;
        //    set
        //    {
        //        if (stakePeriod != value)
        //        {
        //            stakePeriod = value;
        //            OnPropertyChanged(nameof(StakePeriod));
        //        }
        //    }
        //}
        private bool page3Month;
        public bool Page3Month
        {
            get => page3Month;
            set
            {
                if (page3Month != value)
                {
                    page3Month = value;
                    OnPropertyChanged(nameof(Page3Month));
                }
            }
        }
        private bool managePage3Month;
        public bool ManagePage3Month
        {
            get => managePage3Month;
            set
            {
                if (managePage3Month != value)
                {
                    managePage3Month = value;
                    OnPropertyChanged(nameof(ManagePage3Month));
                }
            }
        }

        private bool managePageMonth;
        public bool ManagePageMonth
        {
            get => managePageMonth;
            set
            {
                if (managePageMonth != value)
                {
                    managePageMonth = value;
                    OnPropertyChanged(nameof(ManagePageMonth));
                }
            }
        }
        private bool stakeMainPage;
        public bool StakeMainPage
        {
            get => stakeMainPage;
            set
            {
                if (stakeMainPage != value)
                {
                    stakeMainPage = value;
                    OnPropertyChanged(nameof(StakeMainPage));
                }
            }
        }

        private string stakedAmount;
        public string StakedAmount
        {
            get => stakedAmount;
            set
            {
                if (stakedAmount != value)
                {
                    stakedAmount = value;
                    OnPropertyChanged(nameof(StakedAmount));
                }
            }
        }
        private string endDate;
        public string EndDate
        {
            get => endDate;
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        //private bool stakeMatured;
        //public bool StakeMatured
        //{
        //    get => stakeMatured;
        //    set
        //    {
        //        if (stakeMatured != value)
        //        {
        //            stakeMatured = value;
        //            OnPropertyChanged(nameof(StakeMatured));
        //        }
        //    }
        //}
        private string stakeDbId;
        public string StakeDbId
        {
            get => stakeDbId;
            set
            {
                if (stakeDbId != value)
                {
                    stakeDbId = value;
                    OnPropertyChanged(nameof(StakeDbId));
                }
            }
        }

        private ObservableCollection<ManagePageDetails> managePageDetails;

        public ObservableCollection<ManagePageDetails> ManagePageDetailsList
        {
            get { return managePageDetails; }
            set { managePageDetails = value; OnPropertyChanged(nameof(ManagePageDetailsList)); }
        }

    }

    public class ManagePageDetails : BaseViewModel
    {
        private string stakedamount;

        public string StakedAmount
        {
            get { return stakedamount; }
            set { stakedamount = value; OnPropertyChanged(nameof(StakedAmount)); }
        }

       private string endDate;
       public string EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        //private bool stakeMatured;
        //public bool StakeMatured
        //{
        //    get { return stakeMatured; }
        //    set { StakeMatured = value; OnPropertyChanged(nameof(StakeMatured)); }
        //}
        private bool stakeMatured;
        public bool StakeMatured
        {
            get
            {
                return stakeMatured;
            }
            set
            {
                stakeMatured = value; OnPropertyChanged(nameof(StakeMatured));
            }
        }
        private string stakeDbId;
        public string StakeDbId
        {
            get
            {
                return stakeDbId;
            }
            set
            {
                stakeDbId = value; OnPropertyChanged(nameof(StakeDbId));
            }
        }
    }
}
