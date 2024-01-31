using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    public Transform characterBody;
    public Transform cameraArm;

    public int Speed = 5;
    private float Getkey_speed;

    public int rotationSpeed = 5;

    private static bool isMove;

    private void Start()
    {
        playerManager = PlayerManager.instance;
    }

    void Update()
    {
        LookAround();
        move();
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(h, v);
        playerManager.isMove = moveInput.magnitude != 0;

        if (playerManager.isMove)
        {

            if ((h != 0) && (v != 0))
            {
                Getkey_speed = 1 / Mathf.Sqrt(2);
            }
            else
            {
                Getkey_speed = 1;
            }

            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            Quaternion newRotation = Quaternion.LookRotation(moveDir);
            characterBody.rotation = Quaternion.Slerp(characterBody.rotation, newRotation, rotationSpeed * Time.deltaTime);

            transform.position += moveDir * Time.deltaTime * Speed * Getkey_speed;
        }
    }

}
