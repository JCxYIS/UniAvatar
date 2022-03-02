using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniAvatar
{
    public class WordsManager : MonoSingleton<WordsManager>
    {
        public WordSetting WordSetting;

        [HideInInspector]
        public int CurrentLanaguage = 0;

        private Dictionary<string, WordData> m_wordSettingMap;

        private void Awake()
        {
            // Init();
        }

        public void Init(WordSetting ws)
        {
            if(WordSetting == null)
            {
                Debug.LogWarning("WordSetting is null! That means we all use the \"key\" value to print");
                return;
            }
            m_wordSettingMap = WordSetting.WordSheet.ToDictionary(x => x.PrimaryKey);
        }

        public string GetWordByKey(string key)
        {
            try
            {
                return m_wordSettingMap[key].Contents[CurrentLanaguage];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogWarning($"Cannot find the word, with key={key}, language={CurrentLanaguage}");
                return key;
            }
        }

    }
}