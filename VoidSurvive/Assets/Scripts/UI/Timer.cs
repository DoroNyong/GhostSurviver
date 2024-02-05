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

    private int minute;
    private int second;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.time = 0;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (!gameManager.isGameOver)
        {
            gameManager.time += Time.deltaTime;
            minute = (int)gameManager.time / 60;
            second = (int)gameManager.time % 60;
            timerText.text = string.Format("{0:D2} : {1:D2}", minute, second);
            yield return null;
        }
        yield return null;
    }
}
