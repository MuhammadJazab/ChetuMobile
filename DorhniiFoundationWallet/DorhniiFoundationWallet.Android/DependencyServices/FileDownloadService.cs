//using Android.App;
//using dorhniifoundationwallet.dependencyservices;
//using DorhniiFoundationWallet.DependencyServices;
//using DorhniiFoundationWallet.Droid.DependencyServices;
//using System.IO;
//using System.Runtime.CompilerServices;

//[assembly: Dependency(typeof(FileDownloadService))]
//namespace DorhniiFoundationWallet.Droid.DependencyServices
//{
//    public class FileDownloadService : IFileDownload
//    {
//        public FileDownloadService()
//        {

//        }
//        public void ExportData(string exportData)
//        {
//            var file = exporteddata.ToString();
//            var fileName = SharedStrings.Export.ExportFileNamecsv;
//            var downloadDirectory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
//            var filePath = Path.Combine(downloadDirectory, fileName);
//            var streamWriter = File.Create(filePath);
//            streamWriter.Close();
//            File.WriteAllText(filePath, file);
//            var downloadManager = DownloadManager.FromContext(Android.App.Application.Context);
//            downloadManager.AddCompletedDownload(fileName, "csvfile", true, "text/csv", filePath, File.ReadAllBytes(filePath).Length, true);
//            Globals.ClosePopup();
//        }

//        public void exportdata(string exportdata)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}