using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip BGM_Start;
    [SerializeField] private AudioClip BGM_Lobby;
    [SerializeField] private AudioClip BGM_Select;
    [SerializeField] private AudioClip BGM_Main_Idle;
    [SerializeField] private AudioClip BGM_Main_Berserk;
    [SerializeField] private AudioClip BGM_End;

    [SerializeField] private AudioClip clickEffect; 
    [SerializeField] private AudioClip cancelEffect;
    [SerializeField] private AudioClip loadingEffect;
    [SerializeField] private AudioClip towerShotEffect;

    [SerializeField] private AudioClip stageClear;
    [SerializeField] private AudioClip gameOver;

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider masterAudioSlider = null;
    [SerializeField] private Slider BGMAudioSlider = null;
    [SerializeField] private Slider SFXAudioSlider = null;

    [SerializeField] private float masterVolume = 0.5f;
    [SerializeField] private float BGMVolume = 0.5f;
    [SerializeField] private float SFXVolume = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScene")
        {
            BgmPlay(BGM_Start);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayerShotEffect()
    {
        effectAudioSource.PlayOneShot(towerShotEffect);
    }

    public void PlayClickEffect()
    {
        effectAudioSource.PlayOneShot(clickEffect);
    }

    public void PlayCancelEffect()
    {
        effectAudioSource.PlayOneShot(cancelEffect);
    }

    public void PlayLoadingEffect()
    {
        effectAudioSource.PlayOneShot(loadingEffect);
    }

    public void PauseBGM()
    {
        bgmAudioSource.Pause();
    }
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void ResumeBGM()
    {
        bgmAudioSource.Play();
    }

    public void PlayStageClear()
    {
        effectAudioSource.PlayOneShot(stageClear);
    }

    public void PlayGameOver()
    {
        effectAudioSource.PlayOneShot(gameOver);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("OnSceneLoaded");

        
        if (scene.name == "StartScene")
        {
            BgmPlay(BGM_Start);
        }
        else if (scene.name == "Loading")
        {
            return;
        }
        else if (scene.name == "GameLobby")
        {
            BgmPlay(BGM_Lobby);
        }
        else if (scene.name == "MonsterSelectScene")
        {
            BgmPlay(BGM_Select);
        }
        else if (scene.name == "MainScene")
        {
            BgmPlay(BGM_Main_Idle);
        }
        else if (scene.name == "EndScene")
        {
            BgmPlay(BGM_End);
        }
        else
        {
            if (bgmAudioSource.clip != BGM_Start)
            {
                BgmPlay(BGM_Start);
            }
        }
    }

    public void PlayBGMMain_IDle()
    {
        BgmPlay(BGM_Main_Idle);
    }

    public void PlayBGMMain_Berserk()
    {
        BgmPlay(BGM_Main_Berserk);
    }

    private void BgmPlay(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = 0.5f;
        bgmAudioSource.Play();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        masterMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        BGMVolume = volume;
        masterMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
        masterMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetSlider(Slider MasterSlider, Slider BGMSlider, Slider SFXSlider)
    {
        MasterSlider.value = masterVolume;
        BGMSlider.value = BGMVolume;
        SFXSlider.value = SFXVolume;

        masterAudioSlider = MasterSlider;
        BGMAudioSlider = BGMSlider;
        SFXAudioSlider = SFXSlider;

        masterAudioSlider.onValueChanged.AddListener(SetMasterVolume);
        BGMAudioSlider.onValueChanged.AddListener(SetBGMVolume); 
        SFXAudioSlider.onValueChanged.AddListener(SetSFXVolume);
    }
}
