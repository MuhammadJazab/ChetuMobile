
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
using Android;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;

namespace DorhniiFoundationWallet.Droid
{
    [Activity(Label = "DohrniiWallet", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance;
        public string message = "You cannot take screenshot for this page";     
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                Instance = this;
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
                CheckAppPermissions();
                AppCenter.Start("android=67a7631f-e3bf-4014-bf07-7aa71c93e77a;" + "ios= 2feaa84d-4a81-404c-9c90-2b61c0a5eba9;", typeof(Analytics), typeof(Crashes));
                ZXing.Net.Mobile.Forms.Android.Platform.Init();
                LoadApplication(new App());
                Window.SetStatusBarColor(Android.Graphics.Color.Rgb(11, 13, 37));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
        private void CheckAppPermissions()
        {
            try
            {
                if ((int)Build.VERSION.SdkInt < 23)
                {
                    return;
                }
                else
                {
                    if (PackageManager.CheckPermission(Manifest.Permission.Camera, PackageName) != Android.Content.PM.Permission.Granted
                        && PackageManager.CheckPermission(Manifest.Permission.Camera, PackageName) != Android.Content.PM.Permission.Granted)
                    {
                        var permissions = new string[] {
                    Manifest.Permission.Camera, Manifest.Permission.ReadExternalStorage,
                    Manifest.Permission.WriteExternalStorage  };
                        ;
                        RequestPermissions(permissions, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            try
            {
                Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                global::ZXing.Net.Mobile.Forms.Android.Permissionhandler.OnRequestPermissionresult(requestCode, permissions, grantResults);
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
    }
}