using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
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
        Setting(1f, 3f);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Player"))
        {
            playerManager.hp -= 1;
        }
    }
}
