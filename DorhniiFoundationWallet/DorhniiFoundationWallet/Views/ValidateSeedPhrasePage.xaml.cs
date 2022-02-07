using DorhniiFoundationWallet.Models;
using DorhniiFoundationWallet.Resources;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidateSeedPhrasePage : ContentPage
    {
        public ValidateSeedPhrasePage()
        {
            InitializeComponent();          
        }

        public ValidateSeedPhrasePage(ObservableCollection<SeedPhraseListModel> seedList)
        {
            try
            {
                InitializeComponent();
                vm.RandomMethod(seedList);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Mobile back button press method to Exit app
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool res = await DisplayAlert(Resource.txtExitAlert, Resource.msgExit, Resource.txtYes, Resource.txtNo).ConfigureAwait(false);

                if (res)
                {
                    _ = System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        }
    }
}