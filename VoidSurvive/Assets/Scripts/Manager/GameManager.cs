using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Score scoreSc;

    public float time = 0;
    public int score = 0;

    public bool isSetting = false;
    public bool isGameOver = false;

    private bool once = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }
    }

    void Start()
    {
        scoreSc = Score.instance;
        CursorLock();
    }

    private void Update()
    {
        if (isGameOver && once)
        {
            CursorFree();
        }
    }

    public void CursorFree()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        scoreSc.ScoreText(this.score);
    }
}
