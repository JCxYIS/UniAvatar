using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UniAvatar
{
    /// <summary>
    /// UniAvatar 功能彙總 (大概吧)
    /// </summary>
    public class UniAvatarManager : MonoSingleton<UniAvatarManager>
    {
        [Header("DATA")]
        public StorySetting StorySetting;

        [Header("Bindings")]
        public AnimationManager AnimationManager;
        public AudioManager AudioManager;
        public ChoiceManager ChoiceManager;
        public DialogueManager DialogueManager;
        public FlagManager FlagManager;
        public GameStoryManager GameStoryManager;
        public InputManager InputManager;
        public WordsManager WordsManager;

        bool isInited = false;

        /// <summary>
        /// On Finish Story
        /// </summary>
        public UnityEvent OnFinishStory => GameStoryManager.OnFinishStory;

        /// <summary>
        /// If the story has any "custom" action, do what (of that key)?
        /// </summary>
        public Dictionary<string, System.Action<string[]>> CustomActions => GameStoryManager.CustomActionMap;


        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            if(!isInited)
                Init(StorySetting);
        }

        /// <summary>
        /// Init the story
        /// </summary>
        /// <param name="storySetting">data</param>
        /// <param name="startAtStep">start at which step?</param>
        /// <param name="customActions">if the story has any "custom" action, do what?</param>
        public void Init(StorySetting storySetting, int startAtStep = 1, Dictionary<string, System.Action<string[]>> customActions = null)
        {
            StorySetting = storySetting;
            FlagManager.Init(storySetting.FlagSetting);
            WordsManager.Init(storySetting.WordSetting);
            GameStoryManager.Init(storySetting.ActionSetting, startAtStep, customActions);

            isInited = true;
            print("[UniAvatar] Inited");
        }        
    }
}