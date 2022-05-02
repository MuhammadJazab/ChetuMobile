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
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for displaying all transactions.
    /// </summary>
    public class TransactionVM : BaseViewModel
    {
        ITransactionHistoryService apiService;
        private Command transactionDetailCommand;
        #region List Properties
        private ObservableCollection<TransactionListModel> transactionList;        
        public ObservableCollection<TransactionListModel> TransactionList
        {
            get { return transactionList ?? (transactionList = new ObservableCollection<TransactionListModel>()); }
            set
            {
                transactionList = value;
                OnPropertyChanged(nameof(TransactionList));
            }
        }
        #endregion
        #region Image Properties       
        public string SendTransactionIcon { get; set; } = StringConstant.SendTransactionIcon;        
        public string ReceiveTransactionIcon { get; set; } = StringConstant.ReceiveTransactionIcon;
        #endregion
        public ICommand BackCommand { get; set; }
        private string blockChainLink;
        public string BlockChainLink
        {
            get => blockChainLink;
            set
            {
                blockChainLink = value;
                OnPropertyChanged(nameof(BlockChainLink));
            }
        }

        #region Methods
        /// <summary>
        /// This Constructor method is used to initialise the transactions method.
        /// </summary>
        public TransactionVM()
        {
            apiService = new TransactionHistoryService();
            BackCommand = new Command(BackCommandClick);
            GetTransactionList();
        }

        /// <summary>
        /// This method is used to get the list of all trancsactions.
        /// </summary>
        private async void GetTransactionList()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });
                    TransactionHistoryRequestModel transactionHistoryRequest = new TransactionHistoryRequestModel
                    {
                        WalletAddress = Preferences.Get(StringConstant.walletAddress, string.Empty),
                        TransactionType = StringConstant.TransactionTypeAll,                       
                    };
                    TransactionHistoryResponseModel transactionResponse = await apiService.GetAllTransaction(transactionHistoryRequest);
                    if (transactionResponse != null)
                    {
                        if (transactionResponse.Result && transactionResponse.Status == 200)
                        {
                            if (transactionResponse.data != null)
                            {
                                foreach (TransactionHistory item in transactionResponse.data)
                                {
                                    TransactionListModel transaction = new TransactionListModel
                                    {
                                        TransactionType = item.TransactionType,
                                        TransactionAmount = item.Amount.ToString("n2"),
                                        TransactionDateTime = item.Date.ToString(),
                                        CoinName = item.CoinName,
                                        Fee = item.Fee,
                                        FeeCoinShortName = item.FeeCoinShortName,
                                        _id = item._id,
                                        TxID=item.TxId,
                                    };
                                    transaction.TransactionTypeImage = transaction.TransactionType == "Send" ? SendTransactionIcon : ReceiveTransactionIcon;
                                    TransactionList.Add(transaction);
                                }
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, transactionResponse.Message, Resource.txtOk);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(string.Empty, transactionResponse.Message, Resource.txtOk);
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
        /// This method is used to get the Detail of perticular transaction on VeChain Exploler.
        /// </summary>
        public Command TransactionDetailCommand
        {
            get
            {
                try
                {
                    if (transactionDetailCommand == null)
                    {
                        transactionDetailCommand = new Command(async (selectedItem) =>
                        {
                            if (selectedItem != null)
                            {
                                //await Application.Current.MainPage.DisplayAlert(string.Empty, "COMING SOON", Resource.txtOk);
                                TransactionListModel item = selectedItem as TransactionListModel;
                                BlockChainLink = StringConstant.VeChainStatLink + item.TxID;
                                if(item.TxID != null)
                                {
                                    await Application.Current.MainPage.Navigation.PushModalAsync(new TransactionDetailPage(BlockChainLink));
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(string.Empty, "Details not available", Resource.txtOk);
                                }
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(string.Empty, Resource.msgTechnicalErrorOccurred, Resource.txtOk);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return transactionDetailCommand;
            }

        }

        /// <summary>
        /// This method is used to Go back on Transaction history page.
        /// </summary>
        public async void BackCommandClick()
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
