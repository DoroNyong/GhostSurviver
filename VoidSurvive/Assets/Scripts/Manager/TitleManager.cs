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
        // ���� ����Ű
        if (Input.GetKeyDown(KeyCode.Escape) && panels.Count > 0)
        {
            PanelOff();
        }
    }

    public void SetResolution()
    {
        int setWidth = 1280; // ����� ���� �ʺ�
        int setHeight = 720; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
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
