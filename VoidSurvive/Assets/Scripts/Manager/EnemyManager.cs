using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private GameManager gameManager;
    private Enemy enemy;

    public Transform EnemySpawnPointArm;
    public Transform EnemySpawnPoint;

    public int curEnemy = 0;

    public float maxCreateTime = 2f;
    public float minCreateTime = 0.5f;
    public int minEnemy = 10;
    public int maxEnemy = 20;
    public float elitePercentage = 10;

    private bool once = true;

    // 적 개체 하이어라키 장소
    public Transform enemyPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        RandomSeed();

        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        while (true)
        {
            if (gameManager.isGameOver && once)
            {
                maxCreateTime = 0.15f;
                minCreateTime = 0.15f;
                maxEnemy = 100;
                once = false;
            }

            if (curEnemy < maxEnemy)
            {

                curEnemy += 1;

                if (curEnemy > minEnemy)
                {
                    yield return new WaitForSeconds(maxCreateTime);
                }
                else
                {
                    yield return new WaitForSeconds(minCreateTime);
                }

                float x = Random.Range(3f, 13f);
                float y = Random.Range(0, 360f);
                Vector3 spawnPointAngle = EnemySpawnPointArm.rotation.eulerAngles;
                EnemySpawnPointArm.rotation = Quaternion.Euler(x, y, spawnPointAngle.z);

                int percentage = Random.Range(0, 100);

                string enemyName;

                if (percentage > elitePercentage)
                {
                    enemyName = "Ghost";
                }
                else
                {
                    enemyName = "EliteGhost";
                }

                var ghostGo = ObjectPoolManager.instance.GetGo(enemyName);

                ghostGo.transform.position = EnemySpawnPoint.position;
                ghostGo.transform.rotation = EnemySpawnPoint.rotation;

                enemy = ghostGo.GetComponent<Enemy>();
                enemy.CreateEnemy();
            }
            else
            {
                yield return null;
            }
        }
    }

    private void RandomSeed()
    {
        float randomSeed = Random.Range(0f, 100f);
        Random.InitState((int)randomSeed);
    }
}
