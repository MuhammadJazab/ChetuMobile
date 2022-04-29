using System;
using System.Windows.Input;
using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Views;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    public class ComingSoonVM : BaseViewModel
    {
        public string BackWardArrowImage { get; set; } = StringConstant.BackwardPasswordPage;
        public ICommand BackButtonCommand { get; set; }
        public ComingSoonVM()
        {
            BackButtonCommand = new Command(BackButtonCommandClick);
        }
       public async void BackButtonCommandClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SettingPageNew());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
