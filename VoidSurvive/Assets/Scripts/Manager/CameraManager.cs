using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private Transform noZoomPosition;
    [SerializeField] private Transform zoomPosition;

    public GameObject crossHair;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        crossHair = Instantiate(crossHair);
        playerManager.aimPoint = crossHair;
    }

    private void Update()
    {
        if (!playerManager.isAiming)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, noZoomPosition.position, 30f * Time.deltaTime);
            crossHair.SetActive(false);
            playerManager.isAimingShot = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, zoomPosition.position, 30f * Time.deltaTime);
            if (playerManager.isAimingShot )
            {
                PositionCrossHair();
                crossHair.SetActive(true);
            }
        }
    }

    private void PositionCrossHair()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        int layer_mask = LayerMask.GetMask("Default");

        if(Physics.Raycast(ray, out hit, 25f, layer_mask))
        {
            Vector3 hitPosition = hit.point;
            crossHair.transform.position = hitPosition;
            crossHair.transform.LookAt(Camera.main.transform);
        }
    }
}
