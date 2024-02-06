using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip BGM_Title;
    [SerializeField] private AudioClip BGM_Main;
    [SerializeField] private AudioClip BGM_GameOver;

    [SerializeField] private AudioClip clickEffect; 
    [SerializeField] private AudioClip cancelEffect;
    [SerializeField] private AudioClip loadingEffect;
    [SerializeField] private AudioClip playerShotEffect;
    [SerializeField] private AudioClip enemyDeadEffect;

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider masterAudioSlider = null;
    [SerializeField] private Slider BGMAudioSlider = null;
    [SerializeField] private Slider SFXAudioSlider = null;

    [SerializeField] private float masterVolume = 0.6f;
    [SerializeField] private float BGMVolume = 0.6f;
    [SerializeField] private float SFXVolume = 0.6f;

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
            BgmPlay(BGM_Title);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayShotEffect()
    {
        effectAudioSource.PlayOneShot(playerShotEffect);
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

    public void PlayEnemyDeadEffect(Transform transform)
    {
        AudioSource.PlayClipAtPoint(enemyDeadEffect, transform.position, SFXVolume);
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

    public void GameOverBGM()
    {
        bgmAudioSource.clip = BGM_GameOver;
        bgmAudioSource.loop = false;
        bgmAudioSource.volume = 0.5f;
        bgmAudioSource.Play();
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("OnSceneLoaded" + scene.name);
        
        if (scene.name == "TitleScene")
        {
            BgmPlay(BGM_Title);
        }
        else if (scene.name == "MainScene")
        {
            BgmPlay(BGM_Main);
        }
        else
        {
            if (bgmAudioSource.clip != BGM_Title)
            {
                BgmPlay(BGM_Title);
            }
        }
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
