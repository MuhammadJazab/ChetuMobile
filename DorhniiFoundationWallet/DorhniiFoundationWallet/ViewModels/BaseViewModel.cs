using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DorhniiFoundationWallet.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        /// <summary>
        /// This is base class 
        /// </summary>
        public BaseViewModel()
        {
            try
            {
                VersionNumber = VersionTracking.CurrentVersion;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
        }
        private string versionNumber;
        public string VersionNumber
        {
            get => versionNumber;
            set
            {
                versionNumber = value;
                OnPropertyChanged(nameof(VersionNumber));
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        //private bool isPopup;
        //public bool IsPopup
        //{
        //    get => isPopup;
        //    set
        //    {
        //        isPopup = value;
        //        OnPropertyChanged(nameof(IsPopup));
        //    }
        //}
        
        //private string trasactionlink;
        //public string Trasactionlink
        //{
        //    get => trasactionlink;
        //    set
        //    {
        //        trasactionlink = value;
        //        OnPropertyChanged(nameof(Trasactionlink));
        //    }
        //}


    }

}
