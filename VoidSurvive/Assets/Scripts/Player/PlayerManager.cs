using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private GameManager gameManager;
    private Life life;

    public bool isMove = false;
    public bool isAiming = false;
    public bool isAimingShot = false;
    public bool isShot = true;

    public GameObject zoomPoint;
    public GameObject aimPoint;
    public GameObject player;

    public int hp = 5;
    public float speed = 5f;

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

        player = this.gameObject;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        life = Life.instance;
    }

    public void HitPlayerHp(int hitHp)
    {
        hp -= hitHp;
        life.UIHpText(hp);

        if (hp <= 0)
        {
            isMove = false;
            isAiming = false;
            gameManager.isGameOver = true;
        }
    }
}
