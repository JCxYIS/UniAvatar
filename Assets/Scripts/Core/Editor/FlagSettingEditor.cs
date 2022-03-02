using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UniAvatar;

[CustomEditor(typeof(FlagSetting))]
public class FlagSettingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Setup Flag Setting from Google Sheet"))
        {
            var flagSetting = (FlagSetting)target;
            flagSetting.LoadCsvFromGoogleSheet();
        }

        bool clickBtn = GUILayout.Button("Setup Flag Setting from CSV");
        if (clickBtn)
        {
            var flagSetting = (FlagSetting)target;
            flagSetting.SetUpFlag();
            EditorUtility.SetDirty(flagSetting);
            AssetDatabase.SaveAssets();
        }

        base.OnInspectorGUI();
    }
}
