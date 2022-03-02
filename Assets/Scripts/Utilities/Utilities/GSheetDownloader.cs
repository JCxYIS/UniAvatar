using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using System.Text.RegularExpressions;

namespace JC.Utilities
{
    public static class GSheetDownloader
    {
        public static void DownloadCsv(string downloadPath, string gsheetUrl)
        {            
            var groups = new Regex(@"spreadsheets\/d\/([a-zA-Z0-9_]+)\/edit#gid=([0-9]+)").Match(gsheetUrl).Groups;
            string gsheetKey = groups[1].Value;
            string gsheetGid = groups[2].Value;
            if(string.IsNullOrWhiteSpace(gsheetKey) || string.IsNullOrWhiteSpace(gsheetGid))
            {
                throw new System.Exception("Cannot parse Google sheet url "+gsheetUrl);
            }
            DownloadCsv(downloadPath, gsheetKey, gsheetGid);
        }

        public static void DownloadCsv(string downloadPath, string gsheetKey, string gsheetGid)
        {
            string url = $"https://docs.google.com/spreadsheets/d/{gsheetKey}/export?format=csv&id={gsheetKey}&gid={gsheetGid}";
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                Debug.Log("[GSheetDownloader] Download Start From="+url);
                www.SendWebRequest();

                while (!www.isDone)
                {
                    // do something, or nothing while blocking
                }
                if (www.isNetworkError)
                {
                    Debug.LogError("[GSheetDownloader]" + www.error);
                    return;
                }
                
                Debug.Log("[GSheetDownloader] Write file...");
                File.WriteAllText(downloadPath, www.downloadHandler.text);
                Debug.Log("[GSheetDownloader] OK DownloadedBytes="+www.downloadedBytes);
            }
        }
    }
}