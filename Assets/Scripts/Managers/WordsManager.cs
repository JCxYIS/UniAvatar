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
                Debug.LogError($"Cannot find the word, with key={key}, language={CurrentLanaguage}");
                return "";
            }
        }

    }
}