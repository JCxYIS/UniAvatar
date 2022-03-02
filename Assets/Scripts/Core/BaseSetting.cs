using System;
using System.Collections;
using System.Collections.Generic;
using Utopia;
using UnityEngine;

namespace UniAvatar
{

    public abstract class BaseSetting : ScriptableObject
    {
        public string GoogleSheetUrl;

        protected abstract TextAsset Csv { get; }

        public void LoadCsvFromGoogleSheet()
        {
#if UNITY_EDITOR
            string path = Csv == null ? UnityEditor.FileUtil.GetUniqueTempPathInProject() : UnityEditor.AssetDatabase.GetAssetPath(Csv);
            JC.Utilities.GSheetDownloader.DownloadCsv(path, GoogleSheetUrl);                
            // EditorUtility.SetDirty(CsvPath);
#endif
        }
    }
}