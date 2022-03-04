using System.Collections;
using System.Collections.Generic;
using UniAvatar;
using UnityEngine;
using UnityEngine.UI;

public class InitDemo : MonoBehaviour
{
    [Header("Bindings")]
    [SerializeField] InputField m_startOnStep;
    [SerializeField] Dropdown m_storyDropdown;


    [Header("Prefabs")]
    [SerializeField] GameObject m_prefabTmp;
    [SerializeField] GameObject m_prefabUgui;

    [Header("Storys")]
    [SerializeField] List<StorySetting> m_storys;


    // Start is called before the first frame update
    void Start()
    {
        m_startOnStep.text = "1";
        List<Dropdown.OptionData> opt = new List<Dropdown.OptionData>();
        foreach(var story in m_storys)
        {
            opt.Add(new Dropdown.OptionData(story.name));
        }
        m_storyDropdown.options = opt;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(bool isUgui)
    {
        GameObject go;
#if TMP_SUPPORT
        go = Instantiate(m_prefabTmp);
#else
        go = Instantiate(m_prefabUgui);
#endif
        var uniAvatar = go.GetComponent<UniAvatarManager>();


        uniAvatar.Init(m_storys[m_storyDropdown.value], int.Parse(m_startOnStep.text));

        uniAvatar.CustomActions.Add("FUNC_EXAMPLE", (param)=>{
            string output = "你執行了 FUNC_EXAMPLE，帶入參數分別有：";
            for(int i = 0; i < param.Length; i++)
            {
                output += param[i] + " ";
            }
            print(output);

            // set flag
            UniAvatarManager.Instance.FlagManager.Set("FLG_F1", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            // jump to next step
            UniAvatarManager.Instance.GameStoryManager.Play();
        });

        uniAvatar.OnFinishStory.AddListener(()=>{
            print("Back to InitDemo");
            Destroy(uniAvatar.gameObject);
        });
    }
}
