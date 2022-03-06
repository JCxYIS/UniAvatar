using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Utopia;
using RedBlueGames.Tools.TextTyper;
using TMPro;

namespace UniAvatar
{
    public class ChoiceManager : MonoSingleton<ChoiceManager>
    {
        public ChoiceHandler Handler;

        public bool IsShowingChoice;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {

        }

        public void ShowChoice(string Flag, string[] cKeys, string[] cValues, System.Action callback)
        {
            List<string> cText = new List<string>();
            foreach (string key in cKeys)
            {
                // localization
                cText.Add(WordsManager.Instance.GetWordByKey(key));
            }

            IsShowingChoice = true;
            Handler.ShowChoice(Flag, cText.ToArray(), cValues, callback);
        }
    }
}