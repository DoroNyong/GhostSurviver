using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private SoundManager soundManager;

    [SerializeField] private Stack<GameObject> panels = new Stack<GameObject>();

    [SerializeField] private GameObject soundSettingPanel;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        SetResolution();
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

    public void SetResolution()
    {
        int setWidth = 1280; // 사용자 설정 너비
        int setHeight = 720; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }

    public void PanelOn(GameObject panel)
    {
        //soundManager.PlayClickEffect();
        panels.Push(panel);
        panels.Peek().SetActive(true);
    }

    public void PanelOff()
    {
        //soundManager.PlayClickEffect();
        panels.Peek().SetActive(false);
        panels.Pop();
    }

    public void GoMainScene()
    {
        //soundManager.PlayClickEffect();
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        //soundManager.PlayClickEffect();
        Application.Quit();
    }
}
