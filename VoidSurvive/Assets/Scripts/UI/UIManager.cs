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

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;

    private bool once = true;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameManager.instance;
        soundManager = SoundManager.instance;
        soundManager.SetSlider(masterSlider, BGMSlider, SFXSlider);
    }

    private void Update()
    {
        // 세팅 단축키
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

        if (gameManager.isGameOver && once)
        {
            gameOverPanel.SetActive(true);
            once = false;
        }
    }

    public void PanelOn(GameObject panel)
    {
        soundManager.PlayClickEffect();
        if (panels.Count == 0)
        {
            soundManager.PauseBGM();
            Time.timeScale = 0;
            gameManager.isSetting = true;
            gameManager.CursorFree();
        }
        panels.Push(panel);
        panels.Peek().SetActive(true);
    }

    public void PanelOff()
    {
        soundManager.PlayClickEffect();
        panels.Peek().SetActive(false);
        panels.Pop();
        if (panels.Count == 0)
        {
            soundManager.ResumeBGM();
            Time.timeScale = 1;
            gameManager.isSetting = false;
            gameManager.CursorLock();
        }
    }

    public void GoTitleScene()
    {
        Time.timeScale = 1;
        soundManager.PlayClickEffect();
        SceneManager.LoadScene("TitleScene");
    }

    public void ExitGame()
    {
        soundManager.PlayClickEffect();
        Application.Quit();
    }
}
