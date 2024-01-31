using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private Transform noZoomPosition;
    [SerializeField] private Transform zoomPosition;

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    private void Update()
    {
        if (!playerManager.isAiming)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, noZoomPosition.position, 30f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, zoomPosition.position, 30f * Time.deltaTime);
        }
    }
}
