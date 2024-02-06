using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private GameManager gameManager;
    private SoundManager soundManager;
    private PlayerManager playerManager;

    public Transform characterBody;
    public Transform cameraArm;

    private float getKeySpeed;

    public int rotationSpeed = 5;

    public Transform shotPos;
    public Transform targetPos;

    private Vector3 targetDir;

    private Animator animator;

    private void Start()
    {
        gameManager = GameManager.instance;
        soundManager = SoundManager.instance;
        playerManager = PlayerManager.instance;
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!gameManager.isGameOver && !gameManager.isSetting)
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

                this.gameObject.transform.position += moveDir * Time.deltaTime * playerManager.speed * getKeySpeed;
            }
            else
            {
                Vector3 zoomDir = new Vector3(playerManager.zoomPoint.transform.forward.x, 0f, playerManager.zoomPoint.transform.forward.z).normalized;

                Quaternion newRoataion = Quaternion.LookRotation(zoomDir);
                characterBody.rotation = Quaternion.Slerp(characterBody.rotation, newRoataion, rotationSpeed * 4 * Time.deltaTime);

                // 조준시 이동은 애니메이션 미구현
                //this.gameObject.transform.position += moveDir * Time.deltaTime * playerManager.speed * getKeySpeed / 2;
            }
        }
        else
        {
            if (playerManager.isAiming)
            {
                Vector3 zoomDir = new Vector3(playerManager.zoomPoint.transform.forward.x, 0f, playerManager.zoomPoint.transform.forward.z).normalized;

                Quaternion newRoataion = Quaternion.LookRotation(zoomDir);
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

            if (Input.GetMouseButton(0))
            {
                if (playerManager.isAimingShot && playerManager.isShot)
                {
                    ShotCoolCheck();
                }
            }
        }
    }

    private void ShotCoolCheck()
    {
        playerManager.isShot = false;
        Shot();
        animator.SetTrigger("isShot");
        StartCoroutine(ShotCool(0.2f));
    }

    private void Shot()
    {
        soundManager.PlayShotEffect();
        var bulletGo = ObjectPoolManager.instance.GetGo("Bullet");

        bulletGo.transform.position = shotPos.position;

        targetPos = playerManager.aimPoint.transform;
        targetDir = (targetPos.position - shotPos.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(targetDir);
        shotPos.transform.rotation = rotation;
        shotPos.transform.Rotate(Vector3.right * 90);

        bulletGo.transform.rotation = shotPos.rotation;
    }

    IEnumerator ShotCool(float rapid)
    {
        float ShotCool = 1f;
        while (ShotCool > 0f)
        {
            ShotCool -= Time.deltaTime / rapid;
            yield return new WaitForFixedUpdate();
        }
        playerManager.isShot = true;
    }
}
