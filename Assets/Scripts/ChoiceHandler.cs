using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using DG.Tweening;

namespace UniAvatar
{
    public class ChoiceHandler : MonoBehaviour
    {
        // [SerializeField] private Animator m_choiceAnimator;
        [Header("Bindings")]
        public Image BackgroundImg;
        public Transform ChoicesContainer;
        public Button ChoiceTemplate;
        private CanvasGroup m_canvasGroup;

        [Header("Animation Settings")]
        public float BG_FadeInDuration = .5f;
        public float BG_FadeOutDuration = .3f;
        public float Text_TotalFadeInDuration = .5f;

        // #if TMP_SUPPORT
        //         private TMP_Text m_cTMP = new List<TMP_Text>();
        // #else
        //         private List<Text> m_cText = new List<Text>();
        // #endif
        private List<IDisposable> m_subs = new List<IDisposable>();
        private List<Button> m_choices = new List<Button>();

        private void Awake()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
			if(!m_canvasGroup)
				m_canvasGroup = gameObject.AddComponent<CanvasGroup>();
            Init();
        }

        private void Init()
        {
            BackgroundImg.gameObject.SetActive(false);
            ChoiceTemplate.gameObject.SetActive(false);
			m_canvasGroup.alpha = 0;
            m_choices.ForEach(b => Destroy(b.gameObject));
            m_choices.Clear();
        }

        public void ShowChoice(string flag, string[] choiceTexts, string[] choiceValues, System.Action callback)
        {
            // Fade BG
			m_canvasGroup.alpha = 1;
            BackgroundImg.gameObject.SetActive(true);
            BackgroundImg.color = Color.clear;
            BackgroundImg.DOFade(0.5f, BG_FadeInDuration);

            // Create options
			float duationPerChoice = Text_TotalFadeInDuration / choiceTexts.Length; ;
            for (int i = 0; i < choiceTexts.Length; i++) // FIXME option count
            {
                Button g = Instantiate(ChoiceTemplate, ChoicesContainer);
                g.gameObject.SetActive(true);
                m_choices.Add(g);

                // anim
                CanvasGroup cg = g.GetComponent<CanvasGroup>();
                cg.alpha = 0;
                cg.DOFade(1, duationPerChoice)
                    .SetDelay(Text_TotalFadeInDuration / 2f + duationPerChoice * i);

                // text
#if TMP_SUPPORT
                g.GetComponentInChildren<TMP_Text>().text = choiceTexts[i];
#else
        		g.GetComponentInChildren<Text>().text = choiceTexts[i];
#endif
                // listener
                AddChoiceButtonListener(g, flag, choiceValues[i], callback);
            }


            // m_choiceAnimator.SetBool("Show", true);

            // m_sub1 =
            // Choice1.OnClickAsObservable()
            //        .First()
            //        .Subscribe(_ =>
            //        {
            //            FlagManager.Instance.Set(flag, c1Value);
            //            m_choiceAnimator.SetBool("Show", false);
            //            m_sub1?.Dispose();
            //            m_sub2?.Dispose();
            //            Observable.Interval(TimeSpan.FromSeconds(1))
            //                      .First()
            //                      .Subscribe(__ => callback.Invoke());
            //        });

        }

        public void AddChoiceButtonListener(Button button, string flagName, string flagValue, System.Action callback)
        {
            var obs = button.OnClickAsObservable()
                   .First()
                   .Subscribe(_ =>
                   {
                       	// set flags
                       	FlagManager.Instance.Set(flagName, flagValue);

                       	// disponse all listeners
                       	m_subs.ForEach(o => o.Dispose());
                       	m_subs.Clear();

                       	// anim
						m_canvasGroup.DOFade(0, BG_FadeOutDuration)
							.OnComplete(()=>{
								callback.Invoke();
                       	        Init();
							});

                       	// wait for anim
                       	// Observable.Interval(TimeSpan.FromSeconds(1))
                       	//     .First()
                       	//     .Subscribe(__ =>
                       	//     {
                       	//         callback.Invoke();
                       	//         Init();
                       	//     });
                   });
            m_subs.Add(obs);
        }
    }
}