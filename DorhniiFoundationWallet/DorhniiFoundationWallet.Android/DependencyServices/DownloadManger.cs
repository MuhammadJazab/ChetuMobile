using Android.App;
using DorhniiFoundationWallet.DependencyServices;
using DorhniiFoundationWallet.Droid.DependencyServices;
using DorhniiFoundationWallet.Helpers;
using Microsoft.AppCenter.Crashes;
using System;
using System.IO;
using Xamarin.Essentials;
[assembly: Xamarin.Forms.Dependency(typeof(DownloadManger))]
namespace DorhniiFoundationWallet.Droid.DependencyServices
{
    class DownloadManger : IDownloader
    {
        [Obsolete]
        public void DownloadFile(object entrytext)
        {
            try
            {
                string file = entrytext.ToString();
                string fileName = StringConstant.SeedPhrase + Preferences.Get(StringConstant.SeedId, string.Empty) + StringConstant.Csv;
                string downloadDirectory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
                string filePath = Path.Combine(downloadDirectory, fileName);
                if (!File.Exists(filePath))
                {
                    var streamWriter = File.Create(filePath);
                    streamWriter.Close();
                    File.WriteAllText(filePath, file);
                    var downloadManager = DownloadManager.FromContext(Android.App.Application.Context);
                    _ = downloadManager.AddCompletedDownload(fileName, StringConstant.CsvFile, true, StringConstant.TextCsv, filePath, File.ReadAllBytes(filePath).Length, true);
                }

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}