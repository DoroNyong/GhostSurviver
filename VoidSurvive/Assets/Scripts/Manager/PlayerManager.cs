using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

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

    public bool isMove = false;

    public bool isAiming = false;

    public bool isGameOver = false;

    public GameObject aimPoint = null;
}
