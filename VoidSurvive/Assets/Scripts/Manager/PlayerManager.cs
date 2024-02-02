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

    private void Update()
    {
        if (hp <= 0)
        {
            isMove = false;
            isAiming = false;
            isGameOver = true;
        }
    }

    public bool isMove = false;
    public bool isAiming = false;

    public bool isGameOver = false;

    public GameObject aimPoint = null;

    public int hp = 5;
    public float speed = 5f;
}
