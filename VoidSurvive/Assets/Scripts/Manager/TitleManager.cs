using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private SoundManager soundManager;

    [SerializeField] private Stack<GameObject> panels = new Stack<GameObject>();

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        soundManager = SoundManager.instance;
        soundManager.SetSlider(masterSlider, BGMSlider, SFXSlider);
    }

    private void Update()
    {
        // 세팅 단축키
        if (Input.GetKeyDown(KeyCode.Escape) && panels.Count > 0)
        {
            PanelOff();
        }
    }

    public void PanelOn(GameObject panel)
    {
        soundManager.PlayClickEffect();
        panels.Push(panel);
        panels.Peek().SetActive(true);
    }

    public void PanelOff()
    {
        soundManager.PlayClickEffect();
        panels.Peek().SetActive(false);
        panels.Pop();
    }

    public void GoMainScene()
    {
        soundManager.PlayClickEffect();
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        soundManager.PlayClickEffect();
        Application.Quit();
    }
}
