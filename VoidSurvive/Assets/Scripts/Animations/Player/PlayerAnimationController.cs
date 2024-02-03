using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] private Animator animator;

    private bool once = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    private void Update()
    {
        if (!playerManager.isGameOver)
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
}
