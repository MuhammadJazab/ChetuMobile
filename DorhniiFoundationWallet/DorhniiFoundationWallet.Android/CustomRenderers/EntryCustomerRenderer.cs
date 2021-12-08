using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DorhniiFoundationWallet.CustomRenderers;
using DorhniiFoundationWallet.Droid.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RendererEntry), typeof(EntryCustomerRenderer))]
namespace DorhniiFoundationWallet.Droid.CustomRenderers
{
    class EntryCustomerRenderer : EntryRenderer
    {
        public EntryCustomerRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control.Background = null;
        }
    }
}