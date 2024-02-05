using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float time;

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
}
