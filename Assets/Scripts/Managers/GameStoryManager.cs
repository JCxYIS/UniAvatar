﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UniAvatar
{
    public class GameStoryManager : MonoSingleton<GameStoryManager>
    {
        public ActionSetting ActionSetting;
        public bool PlayOnStart = true;
        public UnityEvent OnFinishStory;

        private Dictionary<string, IAction> m_actionMap = new Dictionary<string, IAction>();
        public Dictionary<string, System.Action> CustomActionMap;
        [SerializeField] [ReadOnly] private int m_actionPtr = 1;

        public HashSet<string> m_nameList = new HashSet<string>();

        private void Awake()
        {
            // Init();
        }

        private void Start()
        {
            Play();
        }

        public void Init(ActionSetting actionSetting, int startAtStep = 1, Dictionary<string, System.Action> customActions = null)
        {
            ActionSetting = actionSetting;
            m_actionPtr = startAtStep - 1;
            CustomActionMap = customActions ?? new Dictionary<string, System.Action>();
            
            m_actionMap.Add("Talk", new Talk());
            m_actionMap.Add("Animate", new Animate());
            m_actionMap.Add("Choice", new Choice());

            foreach (var action in ActionSetting.ActionDatas)
            {
                if (action.Type == "Talk")
                {
                    m_nameList.Add(action.Arg1);
                }
            }            
        }

        public void Play()
        {
            // eof
            if (m_actionPtr >= ActionSetting.ActionDatas.Count)
            {
                Debug.Log("Reach last action.");
                OnFinishStory.Invoke();
                return;
            }

            // ++
            var actionData = ActionSetting.ActionDatas[m_actionPtr++];

            // exit
            if (string.IsNullOrEmpty(actionData.Type) || actionData.Type == "Stop")
            {
                Debug.Log("Stop / Empty action. Now exit.");
                OnFinishStory.Invoke();
                return;
            }

            var arg1 = actionData.Arg1;
            var arg2 = actionData.Arg2;
            var arg3 = actionData.Arg3;
            var arg4 = actionData.Arg4;
            var arg5 = actionData.Arg5;
            var extraArgs = actionData.ExtraArgs;

            // Branch is a special action, jump the action pointer directly.
            if (string.Equals(actionData.Type, "Branch"))
            {
                var flag = arg1;
                var matchValue = arg2;
                int matchStep = 0;
                int unmatchStep = 0;

                // match/unmatch step
                if(string.IsNullOrWhiteSpace(arg3))
                {
                    matchStep = m_actionPtr + 1;
                    // Debug.Log("Unset matchStep, treat as current step + 1");
                }
                else if(!int.TryParse(arg3, out matchStep))
                {
                    Debug.LogError("Failed to match matchStep of branch");
                    return;
                }
                if(string.IsNullOrWhiteSpace(arg4))
                {
                    unmatchStep = m_actionPtr + 1;
                    // Debug.Log("Unset unmatchStep, treat as current step + 1");
                }
                else if(!int.TryParse(arg4, out unmatchStep))
                {
                    Debug.LogError("Failed to match unmatchStep of branch");
                    return;
                }

                // match condition
                if (string.IsNullOrWhiteSpace(flag) || string.Equals(FlagManager.Instance.Get(flag), matchValue))
                {
                    m_actionPtr = matchStep - 1;
                    // print("branch condition match, goto "+m_actionPtr);
                }
                else
                {
                    m_actionPtr = unmatchStep - 1;
                    // print("branch condition unmatch, goto "+m_actionPtr);
                }

                // Also, jump to next step after branching.
                Play();
                return;
            }
            else if(string.Equals(actionData.Type, "Custom"))
            {
                // custom action!
                if(CustomActionMap.TryGetValue(actionData.Arg1, out System.Action a))
                    a.Invoke();
                Play();
                return;
            }

            // Execute next action, if some action auto go next, send callback play.
            var action = m_actionMap[actionData.Type];
            action.Execute(arg1, arg2, arg3, arg4, arg5, ()=>Play(), extraArgs);

            // If the action is animate, pass to next.
            if (string.Equals(actionData.Type, "Animate"))
            {
                Play();
            }
        }

    }
}