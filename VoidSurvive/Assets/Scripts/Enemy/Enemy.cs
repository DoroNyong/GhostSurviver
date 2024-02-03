using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : PoolAble
{
    private EnemyManager enemyManager;
    protected PlayerManager playerManager;
    private CapsuleCollider capsuleCollider;

    public float hp;
    public float speed;

    public GameObject Player = null;

    public Animator animator;

    public bool isDead = false;

    private bool once = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Start()
    {
        enemyManager = EnemyManager.instance;
        playerManager = PlayerManager.instance;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (playerManager.isGameOver && once)
        {
            speed = 10f;
            once = false;
        }

        if (!isDead)
        {
            Vector3 lookPlayer = (Player.transform.position - this.gameObject.transform.position).normalized;
            Vector3 enemyMoveDir = lookPlayer;

            Quaternion newRoataion = Quaternion.LookRotation(enemyMoveDir);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, newRoataion, 2f * Time.deltaTime);

            this.gameObject.transform.position += enemyMoveDir * Time.deltaTime * speed;
        }
    }

    private void Hit()
    {
        if (!isDead)
        {
            hp -= 1;
            if (hp <= 0)
            {
                StartCoroutine(Dead());
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Dead());
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Hit();
        }
    }

    IEnumerator Dead()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        enemyManager.curEnemy -= 1;
        capsuleCollider.enabled = false;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.01f);
        ReleaseObject();
        capsuleCollider.enabled = true;
        isDead = false;
    }

    public virtual void Setting(float hp, float speed)
    {
        this.hp = hp;
        this.speed = speed;
    }
}
