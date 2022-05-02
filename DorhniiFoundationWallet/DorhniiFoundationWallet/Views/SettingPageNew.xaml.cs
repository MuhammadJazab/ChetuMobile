using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.Resources;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DorhniiFoundationWallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPageNew : ContentPage
    {
        public SettingPageNew()
        {
            InitializeComponent();
        }
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

        private void Switch_Toggled_1(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            try
            {
                Preferences.Set(StringConstant.KEY_IS_TOUCH_FACE_ID_ENABLED, e.Value);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}