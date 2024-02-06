using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private TMP_Text timerText;

    private float curTime;

    private int minute;
    private int second;

    private void Start()
    {
        gameManager = GameManager.instance;
        curTime = 0;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (!gameManager.isGameOver)
        {
            curTime += Time.deltaTime;
            gameManager.time = curTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            timerText.text = minute.ToString("00") + " : " + second.ToString("00");
            yield return null;
        }
    }
}
