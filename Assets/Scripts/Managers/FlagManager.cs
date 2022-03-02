using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace UniAvatar
{
    [System.Serializable]
    public class Flag
    {
        public Flag(string key)
        {
            this.Key = key;
        }
        public string Key;
        public string Value;
    }

    public class FlagManager : MonoSingleton<FlagManager>
    {
        public FlagSetting FlagSetting;
        public List<Flag> RuntimeFlags = new List<Flag>();
        private Dictionary<string, Flag> m_flagMap = new Dictionary<string, Flag>();

        private void Awake()
        {
            // Init();
        }

        public void Init(FlagSetting fs)
        {
            FlagSetting = fs;

            if(FlagSetting == null)
            {
                Debug.LogWarning("FlagSetting is null! That means there is no flag, or u are too lazy to specify each flags");
                return;
            }

            foreach (var flag in FlagSetting.Flags)
            {
                RuntimeFlags.Add(new Flag(flag));
            }

            m_flagMap = RuntimeFlags.ToDictionary(key => key.Key, value => value);
        }

        public void Set(string flagName, string value)
        {
            if (m_flagMap.ContainsKey(flagName) == false)
            {
                Debug.LogWarning($"[FLAG_SET] Not found a flag named {flagName}");
                
                m_flagMap.Add(flagName, new Flag(flagName){Value=value} );
                return;
            }

            m_flagMap[flagName].Value = value;
        }

        public string Get(string flagName)
        {
            if (m_flagMap.ContainsKey(flagName) == false)
            {
                Debug.LogWarning($"[FLAG_GET] Not found a flag named {flagName}");
                return string.Empty;
            }

            return m_flagMap[flagName].Value;
        }

    }
}