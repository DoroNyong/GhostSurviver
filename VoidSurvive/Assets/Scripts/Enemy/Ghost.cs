using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
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
