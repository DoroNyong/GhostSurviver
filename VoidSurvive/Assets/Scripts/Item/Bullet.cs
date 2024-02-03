using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : PoolAble
{
    private PlayerManager playerManager;

    [SerializeField] private Transform targetPos;
    [SerializeField] private Vector3 targetDir;

    [SerializeField] private float distance;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        targetPos = playerManager.aimPoint.transform;
        Debug.Log(targetPos.position);
        targetDir = (targetPos.position - this.gameObject.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(targetDir);
        transform.rotation = rotation;
        transform.Rotate(Vector3.right * 90);
        Debug.Log(targetDir);
    }

    void Update()
    {
        distance = Vector3.Magnitude(playerManager.player.transform.position - this.gameObject.transform.position);

        if (distance > 40)
        {
            Pool.Release(this.gameObject);
        }

        this.gameObject.transform.position += targetDir * Time.deltaTime * 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Pool.Release(this.gameObject);
        }
    }
}
