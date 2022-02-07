using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.IServices;
using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Models.APIRequestModels;
using DorhniiFoundationWallet.Models.APIResponseModels;
using DorhniiFoundationWallet.Resources;
using DorhniiFoundationWallet.Services;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    /// <summary>
    /// This class is used for displaying all transactions.
    /// </summary>
    public class TransactionVM : ObservableObject
    {
        ITransactionHistoryService apiService;
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
        #region Methods
        /// <summary>
        /// This Constructor method is used to initialise the transactions method.
        /// </summary>
        public TransactionVM()
        {
            apiService = new TransactionHistoryService();
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
                        WalletAddress = Preferences.Get("WalletAdress", string.Empty),
                        TransactionType = StringConstant.TransactionType,
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
                                        TransactionAmount = item.Amount.ToString(),
                                        TransactionDateTime = item.Date.ToString(),
                                        CoinName = item.CoinName,
                                        FeeDetails = "(Fee : 1 DHN)"
                                    };
                                    transaction.TransactionTypeImage = transaction.TransactionType == "Send" ? SendTransactionIcon : ReceiveTransactionIcon;
                                    TransactionList.Add(transaction);
                                }
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
        #endregion
    }
}
