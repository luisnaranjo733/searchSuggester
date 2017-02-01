using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Web.Services;
using WebRole1.models;

namespace WebRole1
{
    /// <summary>
    /// Summary description for SuggestionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SuggestionService : System.Web.Services.WebService
    {
        private string seedFilePath; // path to seed file
        private PerformanceCounter memProcess; // perf counter for checking mb available in memory


        private const string SEED_FILE_NAME = "seed_short.txt"; // 1000 lines only for testing
        //private const string SEED_FILE_NAME = "seed.txt"; // full file

        public SuggestionService()
        {
            string fileDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString(); // special dir to store files even in cloud
            seedFilePath = Path.Combine(fileDir, SEED_FILE_NAME); // resolve expected path to seed file

            memProcess = new PerformanceCounter("Memory", "Available MBytes");

        }

        [WebMethod]
        public bool downloadWiki()
        {

            if (File.Exists(seedFilePath))
            {
                return true; // if file has already been downloaded, return and don't do anything
            }

            // else download the file
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]
            );
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("info344");

            if (container.Exists())
            {
                CloudBlockBlob blob = container.GetBlockBlobReference(SEED_FILE_NAME);
                if (blob.Exists())
                {
                    // Save blob contents to a file.
                    using (var fileStream = System.IO.File.OpenWrite(seedFilePath))
                    {
                        blob.DownloadToStream(fileStream);
                        return true;
                    }
                }
 
            }
            return false;
        }

        [WebMethod]
        public float GetAvailableMBytes()
        {
            float memUsage = memProcess.NextValue();
            return memUsage;
        }


        [WebMethod]
        public string buildTrie()
        {
            if (!File.Exists(seedFilePath))
            {
                return "Sorry, you need to manually download the seed file first";
            }

            foreach(string title in File.ReadLines(seedFilePath))
            {
                if (GetAvailableMBytes() > 20)
                {
                    //trie.AddTitle(title);
                }

            }

            return "trie built";
        }

        [WebMethod]
        public string[] searchTrie(string query)
        {
            string[] temp = new string[1];
            return temp;
            //return trie.SearchForPrefix(query);

        }

        [WebMethod]
        public bool deleteWiki(string fileName)
        {
            if (File.Exists(seedFilePath))
            {
                File.Delete(seedFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
