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
        if(GUILayout.Button("Setup Word Setting from Google Sheet"))
        {
            var wordSetting = (WordSetting)target;
            var path = wordSetting.LoadCsvFromGoogleSheet();
            wordSetting.SetUpWord(System.IO.File.ReadAllText(path));
            EditorUtility.SetDirty(wordSetting);
            AssetDatabase.SaveAssets();
        }

        bool clickBtn = GUILayout.Button("Setup Word Setting from CSV");
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
