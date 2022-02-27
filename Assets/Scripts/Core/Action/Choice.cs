using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniAvatar
{
    public class Choice : IAction
    {
        public void Execute(string Flag, string C1Key,
                            string C2Key, string C1Value, string C2Value, System.Action callback, params string[] extraArgs)
        {
            List<string> cKeys = new List<string>{C1Key, C2Key};
            List<string> cValues = new List<string>{C1Value, C2Value};

            // handle extra args
            for(int i = 0; i < extraArgs.Length; i++)
            {
                if(i % 2 == 0)
                    cKeys.Add(extraArgs[i]);
                else
                    cValues.Add(extraArgs[i]);
            }

            ChoiceManager.Instance.ShowChoice(Flag, cKeys.ToArray(), cValues.ToArray(), callback);
        }
    }
}