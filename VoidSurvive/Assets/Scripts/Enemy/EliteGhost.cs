using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGhost : Enemy
{
    [SerializeField]
    private Color color;

    protected override void Awake()
    {
        base.Awake();
        skinnedMeshRenderer.material.color = color;
        originColor = skinnedMeshRenderer.material.color;
    }

    protected override void Start()
    {
        base.Start();
        Setting(3f, 4f);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Player"))
        {
            playerManager.HitPlayerHp(2);
        }
    }
}
