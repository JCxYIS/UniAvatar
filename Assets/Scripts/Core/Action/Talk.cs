using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniAvatar
{
    public class Talk : IAction
    {
        public void Execute(string nameKey, string contentKey,
                            string arg3, string arg4, string arg5, System.Action callback, params string[] extraArgs)
        {
            // Send to Dialogue Manager
            DialogueManager.Instance.Say(nameKey, contentKey);
        }
    }
}