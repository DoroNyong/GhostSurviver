using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public List<GameObject> enemies = new List<GameObject>();

    public float createTime = 2f;
    public int maxMonster = 10;
    public float elitePercentage = 10;

    public Transform enemyPoint;

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
        while (!playerManager.isGameOver)
        {
            if (enemies.Count < maxMonster)
            {
                yield return new WaitForSeconds(createTime);

                int spawnPointIdx = Random.Range(0, spawnPoints.Length);
                int enemyIdx;

                if (Random.Range(0, 100) > elitePercentage)
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
        float temp = Time.time * 100f;
        Random.InitState((int)temp);
    }
}
