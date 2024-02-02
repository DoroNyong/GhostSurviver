using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private PlayerManager playerManager;

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public List<GameObject> enemies = new List<GameObject>();

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
        playerManager = PlayerManager.instance;
        RandomSeed();

        if (enemyPrefabs.Length > 0)
        {
            StartCoroutine(CreateMonster());
        }
        else
        {
            Debug.Log("에너미프리팹 비어있음");
        }
    }

    IEnumerator CreateMonster()
    {
        while (true)
        {
            if (playerManager.isGameOver && once)
            {
                maxCreateTime = 0.15f;
                minCreateTime = 0.15f;
                maxEnemy = 100;
                once = false;
            }

            if (enemies.Count < maxEnemy)
            {
                if (enemies.Count > minEnemy)
                {
                    yield return new WaitForSeconds(maxCreateTime);
                }
                else
                {
                    yield return new WaitForSeconds(minCreateTime);
                }

                int spawnPointIdx = Random.Range(0, spawnPoints.Length);
                int enemyIdx;

                int percentage = Random.Range(0, 100);

                if (percentage > elitePercentage)
                {
                    enemyIdx = 0;
                }
                else
                {
                    enemyIdx = 1;
                }

                enemies.Add(Instantiate(enemyPrefabs[enemyIdx], spawnPoints[spawnPointIdx].position, spawnPoints[spawnPointIdx].rotation, enemyPoint));
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
