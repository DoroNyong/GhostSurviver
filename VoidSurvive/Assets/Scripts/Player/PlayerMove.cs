using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    public Transform characterBody;
    public Transform cameraArm;

    private float getKeySpeed;

    public int rotationSpeed = 5;

    private static bool isMove;

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    void Update()
    {
        if (!playerManager.isGameOver)
        {
            LookAround();
            Aim();
            Move();
        }
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 35f);
        }
        else
        {
            x = Mathf.Clamp(x, 345f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(h, v);
        playerManager.isMove = moveInput.magnitude != 0;

        if (playerManager.isMove)
        {

            if ((h != 0) && (v != 0))
            {
                getKeySpeed = 1 / Mathf.Sqrt(2);
            }
            else
            {
                getKeySpeed = 1;
            }

            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            if (!playerManager.isAiming)
            {
                Quaternion newRotation = Quaternion.LookRotation(moveDir);
                characterBody.rotation = Quaternion.Slerp(characterBody.rotation, newRotation, rotationSpeed * Time.deltaTime);

                transform.position += moveDir * Time.deltaTime * playerManager.speed * getKeySpeed;
            }
            else
            {
                Vector3 zoomForward = new Vector3(playerManager.aimPoint.transform.forward.x, 0f, playerManager.aimPoint.transform.forward.z).normalized;
                Vector3 zoomMoveDir = -zoomForward;

                Quaternion newRoataion = Quaternion.LookRotation(zoomMoveDir);
                characterBody.rotation = Quaternion.Slerp(characterBody.rotation, newRoataion, rotationSpeed * 4 * Time.deltaTime);

                transform.position += moveDir * Time.deltaTime * playerManager.speed * getKeySpeed / 2;
            }
        }
        else
        {
            if (playerManager.isAiming)
            {
                Vector3 zoomForward = new Vector3(playerManager.aimPoint.transform.forward.x, 0f, playerManager.aimPoint.transform.forward.z).normalized;
                Vector3 zoomMoveDir = -zoomForward;

                Quaternion newRoataion = Quaternion.LookRotation(zoomMoveDir);
                characterBody.rotation = Quaternion.Slerp(characterBody.rotation, newRoataion, rotationSpeed * 4 * Time.deltaTime);
            }
        }
    }

    private void Aim()
    {
        if (!Input.GetMouseButton(1))
        {
            playerManager.isAiming = false;
        }
        else
        {
            playerManager.isAiming = true;
        }
    }

}
