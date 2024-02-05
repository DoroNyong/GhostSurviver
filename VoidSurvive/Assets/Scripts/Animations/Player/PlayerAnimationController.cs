using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    GameManager gameManager;
    PlayerManager playerManager;

    [SerializeField] private Animator animator;

    private bool once = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        playerManager = PlayerManager.instance;
    }

    private void Update()
    {
        if (!gameManager.isGameOver)
        {
            AnimationUpdate();
        }
        else
        {
            if (once)
            {
                animator.SetTrigger("isDead");
                once = false;
            }
        }
    }

    private void AnimationUpdate()
    {
        if (!playerManager.isMove)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

        if (!playerManager.isAiming)
        {
            animator.SetBool("isAiming", false);
        }
        else
        {
            animator.SetBool("isAiming", true);
        }
    }

    public void IsAimingShot()
    {
        playerManager.isAimingShot = true;
    }
}
