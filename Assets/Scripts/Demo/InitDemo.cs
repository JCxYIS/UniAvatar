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
        var uniAvatar = go.GetComponent<UniAvatar.UniAvatar>();
        uniAvatar.Init(m_storys[m_storyDropdown.value]);
        uniAvatar.OnFinishStory.AddListener(()=>{
            print("Back to InitDemo");
            Destroy(uniAvatar.gameObject);
        });
    }
}
