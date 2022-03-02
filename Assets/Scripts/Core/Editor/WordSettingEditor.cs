using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UniAvatar;

[CustomEditor(typeof(WordSetting))]
public class WordSettingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Load CSV from Google Sheet"))
        {
            var wordSetting = (WordSetting)target;
            wordSetting.LoadCsvFromGoogleSheet();
        }

        bool clickBtn = GUILayout.Button("Setup Word Setting");
        if (clickBtn)
        {
            var wordSetting = (WordSetting)target;
            wordSetting.SetUpWord();
            EditorUtility.SetDirty(wordSetting);
            AssetDatabase.SaveAssets();
        }

        base.OnInspectorGUI();
    }
}
