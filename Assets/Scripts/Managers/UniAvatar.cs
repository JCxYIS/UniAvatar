using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UniAvatar
{
    /// <summary>
    /// UniAvatar 功能彙總 (大概吧)
    /// </summary>
    public class UniAvatar : MonoBehaviour
    {
        [Header("DATA")]
        public StorySetting StorySetting;

        [Header("Bindings")]
        [SerializeField] AnimationManager animationManager;
        [SerializeField] AudioManager audioManager;
        [SerializeField] ChoiceManager choiceManager;
        [SerializeField] DialogueManager dialogueManager;
        [SerializeField] FlagManager flagManager;
        [SerializeField] GameStoryManager gameStoryManager;
        [SerializeField] InputManager inputManager;
        [SerializeField] WordsManager wordsManager;

        bool isInited = false;

        public UnityEvent OnFinishStory => gameStoryManager.OnFinishStory;


        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            if(!isInited)
                Init(StorySetting);
        }

        public void Init(StorySetting storySetting)
        {
            StorySetting = storySetting;
            flagManager.Init(storySetting.FlagSetting);
            wordsManager.Init(storySetting.WordSetting);
            gameStoryManager.Init(storySetting.ActionSetting);

            isInited = true;
            print("[UniAvatar] Inited");
        }        
    }
}