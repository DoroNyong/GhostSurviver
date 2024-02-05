using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : PoolAble
{
    protected GameManager gameManager;
    protected PlayerManager playerManager;
    private EnemyManager enemyManager;
    protected SkinnedMeshRenderer skinnedMeshRenderer;
    private CapsuleCollider capsuleCollider;

    public float hp;
    public int attack;
    public float speed;
    public int score;

    public GameObject Player = null;

    public Animator animator;

    public bool isDead = false;

    private bool once = true;

    protected Color originColor;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        skinnedMeshRenderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material = Instantiate(skinnedMeshRenderer.material);
    }

    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        playerManager = PlayerManager.instance;
        enemyManager = EnemyManager.instance;
        Player = playerManager.player;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (gameManager.isGameOver && once)
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
                gameManager.IncreaseScore(score);
                StartCoroutine(Dead());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerManager.HitPlayerHp(attack);
            StartCoroutine(Dead());
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Hit();
        }
    }

    public void CreateEnemy()
    {
        StartCoroutine(Create());
    }

    IEnumerator Create()
    {
        float x = 0;

        while (x < 0.667f)
        {
            x += Time.deltaTime / 1f * 0.667f;
            skinnedMeshRenderer.material.color = new Vector4(originColor.r, originColor.g, originColor.b, x);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Dead()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        enemyManager.curEnemy -= 1;
        capsuleCollider.enabled = false;
        StartCoroutine(DeadColor(animator.GetCurrentAnimatorStateInfo(0).length));
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.01f);
        RestoreSetting();
    }

    IEnumerator DeadColor(float deadTime)
    {
        float x = skinnedMeshRenderer.material.color.a;
        float y = x;
        while (x > 0f)
        {
            x -= Time.deltaTime / deadTime * y;
            skinnedMeshRenderer.material.color = new Vector4(originColor.r, originColor.g, originColor.b, x);
            yield return new WaitForFixedUpdate();
        }
    }

    private void RestoreSetting()
    {
        ReleaseObject();
        capsuleCollider.enabled = true;
        isDead = false;
    }

    public virtual void Setting(float hp, int attack, float speed, int score)
    {
        this.hp = hp;
        this.attack = attack;
        this.speed = speed;
        this.score = score;
    }
}
