using System;
using System.Collections;
using System.Collections.Generic;
using Utopia;
using UnityEngine;
using UnityEditor;

namespace UniAvatar
{

    public abstract class BaseSetting : ScriptableObject
    {
        public string GoogleSheetUrl;

        protected abstract TextAsset Csv { get; }

        public void LoadCsvFromGoogleSheet()
        {
#if UNITY_EDITOR
            JC.Utilities.GSheetDownloader.DownloadCsv(AssetDatabase.GetAssetPath(Csv), GoogleSheetUrl);
            // EditorUtility.SetDirty(CsvPath);
#endif
        }
    }
}