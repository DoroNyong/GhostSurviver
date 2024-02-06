using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private GameManager gameManager;
    private SoundManager soundManager;
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
        soundManager = SoundManager.instance;
        life = Life.instance;
    }

    public void HitPlayerHp(int hitHp, Transform transform)
    {
        if (!gameManager.isGameOver)
        {
            hp -= hitHp;
            life.UIHpText(hp);
            soundManager.PlayEnemyDeadEffect(transform);
        }

        if (hp <= 0 && !gameManager.isGameOver)
        {
            isMove = false;
            isAiming = false;
            gameManager.isGameOver = true;
            soundManager.GameOverBGM();
        }
    }

    public void DeathFloor()
    {
        life.Dead();
        isMove = false;
        isAiming = false;
        gameManager.isGameOver = true;
        soundManager.GameOverBGM();
    }
}
