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
        
        public string LoadCsvFromGoogleSheet()
        {
#if UNITY_EDITOR
            string path = Csv == null ? UnityEditor.FileUtil.GetUniqueTempPathInProject() : UnityEditor.AssetDatabase.GetAssetPath(Csv);
            JC.Utilities.GSheetDownloader.DownloadCsv(path, GoogleSheetUrl);                
            // EditorUtility.SetDirty(CsvPath);
            return path;
#endif
            throw new NotSupportedException("This finc should only be executed inside Unity Editor");
        }
    }
}