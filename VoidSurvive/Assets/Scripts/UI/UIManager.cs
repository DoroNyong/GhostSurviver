using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    SoundManager soundManager;

    [SerializeField] private Stack<GameObject> panels = new Stack<GameObject>();

    [SerializeField] private GameObject settingPanel;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        //soundManager = SoundManager.instance;
        //soundManager.SetSlider(masterSlider, BGMSlider, SFXSlider);
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ����Ű
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameManager.isSetting)
            {
                PanelOn(settingPanel);
            }
            else
            {
                PanelOff();
            }
        }
    }

    public void PanelOn(GameObject panel)
    {
        //soundManager.PlayClickEffect();
        if (panels.Count == 0)
        {
            //soundManager.PauseBGM();
            Time.timeScale = 0;
            gameManager.isSetting = true;
            gameManager.CursorFree();
        }
        panels.Push(panel);
        panels.Peek().SetActive(true);
        Debug.Log(panels.Peek().ToString());
        Debug.Log(panels.Count);
    }

    public void PanelOff()
    {
        //soundManager.PlayClickEffect();
        Debug.Log(panels.Peek().ToString());
        Debug.Log(panels.Count);
        panels.Peek().SetActive(false);
        panels.Pop();
        if (panels.Count == 0)
        {
            //soundManager.ResumeBGM();
            Time.timeScale = 1;
            gameManager.isSetting = false;
            gameManager.CursorLock();
        }
    }

    public void GoTitleScene()
    {
        //soundManager.PlayClickEffect();
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }

    public void ExitGame()
    {
        //soundManager.PlayClickEffect();
        Application.Quit();
    }
}
