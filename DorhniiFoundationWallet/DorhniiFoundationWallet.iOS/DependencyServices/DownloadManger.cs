using System;
using System.IO;
using DorhniiFoundationWallet.DependencyServices;
using DorhniiFoundationWallet.Helpers;
using DorhniiFoundationWallet.iOS.DependencyServices;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DownloadManger))]
namespace DorhniiFoundationWallet.iOS.DependencyServices
{
    public class DownloadManger: IDownloader
    {
        public void DownloadFile(object entrytext)
        {
            try
            {
                string file = entrytext.ToString();
                string fileName = StringConstant.SeedPhrase + Preferences.Get(StringConstant.SeedId, string.Empty) + StringConstant.Csv;
                string downloadDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                var libaarypath = Path.Combine(downloadDirectory, "", "");
                var iospath = Path.Combine(libaarypath, fileName);
                if (!File.Exists(iospath))
                {
                    var streamWriter = File.Create(iospath);
                    streamWriter.Close();
                    File.WriteAllText(iospath, file);
                    Application.Current.MainPage.DisplayAlert(string.Empty, "download successfully", "ok");
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}

