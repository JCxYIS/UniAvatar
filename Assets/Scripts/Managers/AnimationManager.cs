using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniAvatar
{
    [System.Serializable]
    public class AnimationFunctionPair
    {
        public string Key;
        public AnimationFunctionBase AnimationFunction;
    }

    [System.Serializable]
    public class AnimationTargetPair
    {
        public string Key;
        public AnimationTargetBase AnimateTarget;
    }

    public class AnimationManager : MonoSingleton<AnimationManager>
    {
        public AnimationTargetPair[] AnimationTargetSetting;
        public AnimationFunctionPair[] AnimationFunctionSetting;

        private Dictionary<string, AnimationTargetBase> m_animationTargetMap = new Dictionary<string, AnimationTargetBase>();
        private Dictionary<string, AnimationFunctionBase> m_animationFunctionMap = new Dictionary<string, AnimationFunctionBase>();

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            m_animationTargetMap = AnimationTargetSetting.ToDictionary(key => key.Key, value => value.AnimateTarget);
            m_animationFunctionMap = AnimationFunctionSetting.ToDictionary(key => key.Key, value => value.AnimationFunction);
        }

        public void PlayAnim(string targetKey, string functionKey)
        {
            if(!m_animationTargetMap.TryGetValue(targetKey, out var target))
            {
                Debug.LogWarning($"Cannot get animation target \"{targetKey}\". Now ignore.");
                return;
            }
            if(!m_animationFunctionMap.TryGetValue(functionKey, out var function))
            {
                Debug.LogWarning($"Cannot get animation function \"{targetKey}\". Now ignore.");
                return;
            }

            var functionInstnace = function.CreateInstance();
            functionInstnace.Play(target);
        }

        public void InterruptAnim(string targetKey, string functionKey)
        {
            if(!m_animationTargetMap.TryGetValue(targetKey, out var target))
            {
                Debug.LogWarning($"Cannot get animation target \"{targetKey}\". Now ignore.");
                return;
            }
            if(!m_animationFunctionMap.TryGetValue(functionKey, out var function))
            {
                Debug.LogWarning($"Cannot get animation function \"{targetKey}\". Now ignore.");
                return;
            }

            var functionInstnace = function.CreateInstance();
            functionInstnace.Interrupt();
        }

    }
}