using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteGhost : Enemy
{
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
            playerManager.hp -= 2;
        }
    }
}
