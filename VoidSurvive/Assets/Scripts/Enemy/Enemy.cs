using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public float speed = 3f;

    public GameObject Player = null;

    public Animator animator;

    public bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isDead)
        {
            Vector3 lookPlayer = (Player.transform.position - gameObject.transform.position).normalized;
            Vector3 enemyMoveDir = lookPlayer;

            Quaternion newRoataion = Quaternion.LookRotation(enemyMoveDir);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, newRoataion, 2f * Time.deltaTime);

            transform.position += enemyMoveDir * Time.deltaTime * 4f;
        }
    }

    private void Hit()
    {
        hp -= 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDead = true;
            StartCoroutine("Dead");
        }
    }

    IEnumerator Dead()
    {
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.01f);
        Destroy(gameObject);
    }
}
