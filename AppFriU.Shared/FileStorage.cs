using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Storage;
using System;
using Windows.ApplicationModel.Resources.Core;

namespace AppFriU
{
    public static class FileStorage
    {
        public static async Task<List<string>> ReadFile(string url)
        {
            var listOfWords = new List<string>();

            string line;
            var folder = Package.Current.InstalledLocation;

            using (Stream stream = await folder.OpenStreamForReadAsync(url))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        listOfWords.Add(line);
                    }
                    return listOfWords;
                    //   string content = reader.ReadToEnd();
                    //  return content;
                }
            }
        }


        public static string ReadAllText(string path1)
        {
            
            IAsyncOperation<StorageFile> file =
                      StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///" + path1));
            file.AsTask<StorageFile>().Wait();

            IAsyncOperation<string> result = FileIO.ReadTextAsync(file.GetResults());
            result.AsTask<string>().Wait();

            return result.GetResults();
        }

        public static async Task<StorageFile> Get(string relativePath)
        {
            //  string resourceKey = string.Format("Files/{0}", relativePath);
   
            string resourceKey = "ms-resource:///Resources/dict";
            var mainResourceMap = ResourceManager.Current.MainResourceMap;
            var dd = mainResourceMap["dict"];
            //    string s = _resourceloader.GetStringForUri(new Uri("ms-resource:///Resources/dict"));
            if (!mainResourceMap.ContainsKey("dict"))
                return null;
            else
            {
             
                IAsyncOperation<StorageFile> file =
                   StorageFile.GetFileFromApplicationUriAsync(new Uri(dd.Uri.AbsolutePath));
                IAsyncOperation<string> result = FileIO.ReadTextAsync(file.GetResults());
            }

            return await mainResourceMap[resourceKey].Resolve().GetValueAsFileAsync();
        }
    }

}
