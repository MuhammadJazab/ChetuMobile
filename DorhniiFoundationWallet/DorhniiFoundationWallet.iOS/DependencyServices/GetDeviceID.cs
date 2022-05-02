using DorhniiFoundationWallet.DependencyServices;
using DorhniiFoundationWallet.iOS.DependencyServices;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms.PlatformConfiguration;
[assembly: Xamarin.Forms.Dependency(typeof(GetDeviceID))]
namespace DorhniiFoundationWallet.iOS.DependencyServices
{
    public class GetDeviceID : IDeviceIdGetter
    {
        public string GetDeviceIDDetail()
        {
           string id = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
           return id;
        }
    }
}