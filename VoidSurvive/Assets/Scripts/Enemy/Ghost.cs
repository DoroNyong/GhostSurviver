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
        Setting(1f, 1, 3f, 1);
    }
}
