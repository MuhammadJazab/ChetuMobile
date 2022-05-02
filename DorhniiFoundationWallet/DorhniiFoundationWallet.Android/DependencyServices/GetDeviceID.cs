using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DorhniiFoundationWallet.DependencyServices;
using DorhniiFoundationWallet.Droid.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Provider.Settings;

[assembly: Xamarin.Forms.Dependency(typeof(GetDeviceID))]
namespace DorhniiFoundationWallet.Droid.DependencyServices
{
    public class GetDeviceID : IDeviceIdGetter
    {
        public string GetDeviceIDDetail()
        {
            var context = Android.App.Application.Context;
            string id = Android.Provider.Settings.Secure.GetString(context.ContentResolver, Secure.AndroidId);
            return id;
        }
    }
}