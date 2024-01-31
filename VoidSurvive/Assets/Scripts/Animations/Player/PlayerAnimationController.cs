using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] private Animator animator;

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
        AnimationUpdate();
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
    }
}
