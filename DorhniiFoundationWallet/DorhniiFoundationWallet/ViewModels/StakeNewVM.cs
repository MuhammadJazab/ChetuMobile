using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is related to Stake features
    /// </summary>
    public class StakeNewVM : BaseViewModel
    {
        #region private properties
        IStakeService apiservice;
        StakeTokenOverviewResponseModel stakeTokenOverviewResponseModel = null;
        StakeBalanceAvailableResponseModel stakeBalanceAvailableResponseModel = null;
        APIResponseModel aPIResponseModel = null;
        GetStakedResponseModel getStakedResponseModel = null;
        APIResponseModel unstakeResponseModel = null;
        private bool managePageFlexi;
        private bool managePage3Month;
        private bool isStakeOptionVisible;
        private bool isScrollViewVissible;
        private string stakeAmountEntry;
        public StakesListModel SelectedStakeMonth { get; set; }
        public string WalletAddress { get; set; } = Preferences.Get(StringConstant.walletAddress, string.Empty);
        private ObservableCollection<StakesListModel> stakedDetailList;
        public ObservableCollection<StakesListModel> StakedDetailList
        {
            get => stakedDetailList ?? (stakedDetailList = new ObservableCollection<StakesListModel>());
            set
            {
                stakedDetailList = value;
                OnPropertyChanged(nameof(StakedDetailList));
            }
        }
        public ICommand BackOptionClick { get; set; }
        public ICommand TakeStakeCommand { get; set; }
        public string StakeAmountEntry
        {
            get => stakeAmountEntry;
            set
            {
                if (stakeAmountEntry != value)
                {
                    stakeAmountEntry = value;
                    OnPropertyChanged(nameof(StakeAmountEntry));
                }
            }
        }
        #endregion
        #region public properties

        private string aprRate;
        public string AprRate
        {
            get => aprRate;
            set
            {
                if (aprRate != value)
                {
                    aprRate = value;
                    OnPropertyChanged(nameof(AprRate));
                }
            }
        }
        private string monthDuration;
        public string MonthDuration
        {
            get => monthDuration;
            set
            {
                if (monthDuration != value)
                {
                    monthDuration = value;
                    OnPropertyChanged(nameof(MonthDuration));
                }
            }
        }
        private string unstakemonthDuration;
        public string UnstakemonthDuration
        {
            get => unstakemonthDuration;
            set
            {
                if (unstakemonthDuration != value)
                {
                    unstakemonthDuration = value;
                    OnPropertyChanged(nameof(UnstakemonthDuration));
                }
            }
        }
        private string inputDuration;
        public string InputDuration
        {
            get
            {
               
                return inputDuration;
            }
            set
            {
                if (inputDuration != value)
                {
                    inputDuration = value;
                    OnPropertyChanged(nameof(InputDuration));
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
        public bool ManagePageFlexi
        {
            get => managePageFlexi;
            set
            {
                if (managePageFlexi != value)
                {
                    managePageFlexi = value;
                    OnPropertyChanged(nameof(ManagePageFlexi));
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
        private string usdValue;
        public string UsdValue
        {
            get => usdValue;
            set
            {
                if (usdValue != value)
                {
                    usdValue = value;
                    OnPropertyChanged(nameof(UsdValue));
                }
            }
        }
        private string formattedAvailableToken;
        public string FormattedAvailableToken
        {
            get => formattedAvailableToken;
            set
            {
                if (formattedAvailableToken != value)
                {
                    formattedAvailableToken = value;
                    OnPropertyChanged(nameof(FormattedAvailableToken));
                }
            }
        }

        private string coinShortName;
        public string CoinShortName
        {
            get => coinShortName;
            set
            {
                if (coinShortName != value)
                {
                    coinShortName = value;
                    OnPropertyChanged(nameof(CoinShortName));
                }
            }
        }
        public ObservableCollection<StakesListModel> StakeList { get; set; }       
        /// <summary>
        /// This command is used for the unstake token.
        /// </summary>
        private Command unstakeCommand;
        public Command UnstakeCommand
        {
            get
            {
                if (unstakeCommand == null)
                {
                    unstakeCommand = new Command(async (selectedItem) =>
                    {
                        if (selectedItem != null)
                        {
                            try
                            {                                
                                ManagePageDetails item = selectedItem as ManagePageDetails;
                                selectedItem = item;
                               
                                if (item.StakeMatured)
                                {
                                    var IsStatus = await App.Current.MainPage.DisplayAlert("Unstake Option", string.Empty, "Unstake", "Cancel");
                                    if (IsStatus)
                                    {
                                        UnStakeToken(item.StakeDbId);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    });
                }
                return unstakeCommand;
            }
        }
        #endregion
        #region public method 
        /// <summary>
        /// Constructor of This Class 
        /// </summary>
        public StakeNewVM()
        {
            try
            {
                apiservice = new StakeService();                
                BackOptionClick = new Command(BackOptionClickCommand);
                TakeStakeCommand = new Command(TakeStakeCommandClickAsync);
                IsScrollViewVissible = true;               
                IsStakeOptionVisible = false;
                TokenOverView();
                GetStakAvailableBalanceAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Back Button Command Method is used to come on stake main page 
        /// </summary>
        public void BackOptionClickCommand()
        {
            try
            {
                IsStakeOptionVisible = false;
                IsScrollViewVissible = true;               
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This Method is used to find available token for staking 
        /// </summary>
        public async void GetStakAvailableBalanceAsync()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    IsLoading = true;
                    StakeBalanceAvailableRequestModel getStakerequest = new StakeBalanceAvailableRequestModel
                    {
                        WalletAddress = Preferences.Get(StringConstant.walletAddress, string.Empty),
                    };
                    stakeBalanceAvailableResponseModel = await apiservice.GetAvailableStakeToken(getStakerequest);
                    if (stakeBalanceAvailableResponseModel != null)
                    {
                        if (stakeBalanceAvailableResponseModel.Result && stakeBalanceAvailableResponseModel.Status == 200)
                        {
                            UsdValue = Resource.txtDollar + stakeBalanceAvailableResponseModel.CoinUsdValue.ToString("n2");
                            FormattedAvailableToken = stakeBalanceAvailableResponseModel.CoinValue.ToString("n2");
                            CoinShortName = stakeBalanceAvailableResponseModel.CoinShortName;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, stakeBalanceAvailableResponseModel.Message, Resource.txtOk);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// This method is used to perform take stake function
        /// </summary>
        public async void TakeStakeCommandClickAsync()
        {
            try
            {
                if (Convert.ToDouble(StakeAmountEntry) > 0)
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        IsLoading = true;
                        TakeStakeRequestModel takeStakerequest = new TakeStakeRequestModel
                        {
                            BlockChain = "VECHAIN",
                            WalletAdress = Preferences.Get(StringConstant.walletAddress, string.Empty),
                            UserId = Preferences.Get(StringConstant.UserID, string.Empty),
                            StakePeriod = InputDuration,
                            EncryptedPrivateKey = Preferences.Get(StringConstant.EncryptedPrivateKey, string.Empty),
                            Amount = Convert.ToDouble(StakeAmountEntry),
                        };
                        aPIResponseModel = await apiservice.TakeStake(takeStakerequest);
                        if (aPIResponseModel != null)
                        {
                            if (aPIResponseModel.Result && aPIResponseModel.Status == 200)
                            {
                                await Application.Current.MainPage.DisplayAlert(Resource.txtSuccessful, aPIResponseModel.Message, Resource.txtOk);
                                await Application.Current.MainPage.Navigation.PushModalAsync(new StakePageNew());
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, aPIResponseModel.Message, Resource.txtOk);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.msgNetworkIssueMessage, Resource.txtOk);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(Resource.txtAlert, Resource.txtPleaseEnterAmount, Resource.txtOk);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// This method is used for the Get stack details.
        /// </summary>
        public async void GetStakedDetail(string stakeperiod)
        {
            try
            {
                if (Utilities.IsNetworkAvailable())
                {
                    IsLoading = true;

                    var internallist = new ObservableCollection<ManagePageDetails>();

                    GetStakedDetailRequestModel getStakedDetailRequestModel = new GetStakedDetailRequestModel
                    {
                        BlockChain = "VECHAIN",
                        WalletAdress = Preferences.Get(StringConstant.walletAddress, string.Empty),
                    };
                    getStakedDetailRequestModel.StakePeriod = stakeperiod;
                    getStakedResponseModel = await apiservice.GetStakedDetail(getStakedDetailRequestModel);
                    if (getStakedResponseModel != null)
                    {
                        if (getStakedResponseModel.Result && getStakedResponseModel.Status == 200)
                        {
                            if (getStakedResponseModel.Data != null)
                            {
                                foreach (StakedDetailResponseModel item in getStakedResponseModel.Data)
                                {
                                    ManagePageDetails wallet = new ManagePageDetails
                                    {
                                        StakedAmount = item.Token.ToString("n2"),
                                        EndDate = item.EndDate,
                                        StakeMatured = item.StakeMatured,
                                       StakeDbId = item.StakeDbId,                                                                                
                                    };
                                    internallist.Add(wallet);

                                }
                                SelectedListItem.ManagePageDetailsList = internallist;
                            }
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }

      
        /// <summary>
        /// This method is used for the unstake token.
        /// </summary>
        /// <param name="StakeDbId"></param>
        public async void UnStakeToken(string StakeDbId)
        {
            try
            {
                if (Utilities.IsNetworkAvailable())
                {
                    IsLoading = true;
                    UnStakeRequestModel unStakeRequestModel = new UnStakeRequestModel
                    {
                        BlockChain = "VECHAIN",
                        WalletAdress = Preferences.Get(StringConstant.walletAddress, string.Empty),
                        UserId = Preferences.Get(StringConstant.UserID, string.Empty),
                        StakePeriod = UnstakemonthDuration,
                        EncryptedPrivateKey = Preferences.Get(StringConstant.EncryptedPrivateKey, string.Empty),
                        StakeDbId = StakeDbId,
                    };
                    unstakeResponseModel = await apiservice.UnStakeToken(unStakeRequestModel);
                    if (unstakeResponseModel != null)
                    {
                        if (unstakeResponseModel.Result && unstakeResponseModel.Status == 200)
                        {
                            await Application.Current.MainPage.DisplayAlert(Resource.txtSuccessful, unstakeResponseModel.Message, Resource.txtOk);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, unstakeResponseModel.Message, Resource.txtOk);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }

        /// <summary>
        /// This method is used for the token overview.
        /// </summary>
        public async void  TokenOverView()
        {
            try
            {
                if (Utilities.IsNetworkAvailable())
                {
                    IsLoading = true;
                    StakeTokenOverViewRequestModel stakeTokenOverViewRequestModel = new StakeTokenOverViewRequestModel
                    {
                        BlockChain = "VECHAIN",
                        WalletAdress = Preferences.Get(StringConstant.walletAddress, string.Empty),                      
                    };
                    stakeTokenOverviewResponseModel = await apiservice.GetTokenOverView(stakeTokenOverViewRequestModel);
                    if (stakeTokenOverviewResponseModel != null)
                    {
                        if (stakeTokenOverviewResponseModel.Result && stakeTokenOverviewResponseModel.Status == 200)
                        {
                            foreach (StakeTokenOverViewDatailResponseModel item in stakeTokenOverviewResponseModel.Data)
                            {
                                InputDuration = item.StakePeriod;
                                StakesListModel wallet = new StakesListModel
                                {
                                    TokenMainBalance = item.Token.ToString("n2"),
                                    AprForStakePAge= item.RewardPercentage.ToString() + "%",
                                    PercentageAPR = item.RewardPercentage.ToString() + "%" + "  APR",
                                    MonthCount = item.StakePeriod,
                                    StakePeriod = item.StakePeriod+"ONTHS",
                                };
                                StakedDetailList.Add(wallet);
                            }
                            foreach (StakesListModel walletItem in StakedDetailList)
                            {
                                walletItem.StakeMainPage = true;
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, stakeTokenOverviewResponseModel.Message, Resource.txtOk);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgNetworkIssueMessage, Resource.txtOk);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }

        StakesListModel SelectedListItem { get; set; }
        private Command manageCommand;
        public Command ManageCommand
        {
            get
            {
                if (manageCommand == null)
                {
                    manageCommand = new Command(async (selectedItem) =>
                    {
                        try
                        {
                            if (selectedItem != null)
                            {
                                IsLoading = true;
                                if (StakedDetailList != null)
                                {
                                    if (StakedDetailList.Count > 0)
                                    {
                                        StakedDetailList.ToList().ForEach(x =>
                                        {
                                            x.StakeMainPage = true;
                                            x.ManagePageMonth = false;
                                        });
                                    }
                                }
                                StakesListModel item = selectedItem as StakesListModel;
                                SelectedListItem = item;
                                SelectedStakeMonth = item;
                                if (item != null)
                                {
                                    GetStakedDetail(item.MonthCount);
                                    item.StakeMainPage = false;
                                    item.ManagePageMonth = true;                                                                       
                                }
                                AprRate = item.PercentageAPR;
                                MonthDuration = item.StakePeriod;
                                UnstakemonthDuration = item.MonthCount;
                                IsLoading = false;
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    });
                }
                return manageCommand;
            }
        }
        private Command stakeCommandMainPage;
        public Command StakeCommandMainPage
        {
            get
            {
                if (stakeCommandMainPage == null)
                {
                    stakeCommandMainPage = new Command(async (selectedItem) =>
                    {
                        if (selectedItem != null)
                        {
                            try
                            {
                                StakesListModel item = selectedItem as StakesListModel;
                                SelectedListItem = item;
                                SelectedStakeMonth = item;
                                if (item != null)
                                {                                                                   
                                    IsLoading = false;
                                }
                                AprRate = item.AprForStakePAge;
                                MonthDuration = item.StakePeriod;
                                InputDuration = item.MonthCount;
                                IsStakeOptionVisible = true;
                                IsScrollViewVissible = false;
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    });
                }
                return stakeCommandMainPage;
            }
        }
        private Command stakeCommandManagePage;
        public Command StakeCommandManagePage
        {
            get
            {
                if (stakeCommandManagePage == null)
                {
                    stakeCommandManagePage = new Command(async (selectedItem) =>
                    {
                        if (selectedItem != null)
                        {
                            try
                            {
                                StakesListModel item = selectedItem as StakesListModel;
                                SelectedListItem = item;
                                SelectedStakeMonth = item;
                                if (item != null)
                                {

                                    IsLoading = false;
                                }
                                AprRate = item.AprForStakePAge;
                                MonthDuration = item.StakePeriod;
                                InputDuration = item.MonthCount;
                                IsStakeOptionVisible = true;
                                IsScrollViewVissible = false;
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                        }
                    });
                }
                return stakeCommandManagePage;
            }
        }
        //private Command unstakeCommand;
        //public Command UnstakeCommand
        //{

        //}

        #endregion public method
    }
}
