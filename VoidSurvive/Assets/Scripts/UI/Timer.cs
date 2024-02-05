using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private PlayerManager playerManager;

    [SerializeField] private TMP_Text timerText;

    private float curTime;

    private int minute;
    private int second;

    private void Awake()
    {
        curTime = 0;
        timerText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        playerManager = PlayerManager.instance;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (!playerManager.isGameOver)
        {
            curTime += Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            timerText.text = string.Format("{0:D2} : {1:D2}", minute, second);
            yield return null;
        }
        yield return null;
    }
}
