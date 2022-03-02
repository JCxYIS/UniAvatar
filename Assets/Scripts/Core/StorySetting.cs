using System;
using System.Collections.Generic;
using UniAvatar;
using UnityEngine;
using Utopia;

namespace UniAvatar
{
    [CreateAssetMenu(fileName = "StorySetting", menuName = "UniAvatar/StorySetting")]
    public class StorySetting : ScriptableObject
    {
        [Tooltip("*required*")]
        public ActionSetting ActionSetting;
        public WordSetting WordSetting;
        public FlagSetting FlagSetting;
    }
}