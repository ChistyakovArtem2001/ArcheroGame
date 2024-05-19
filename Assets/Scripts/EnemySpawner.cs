using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // Префаб врага
    public Transform[] spawnPoints;  // Точки спауна
    public int maxEnemies = 3;       // Максимальное количество врагов одновременно
    public float spawnInterval = 5f; // Интервал спауна

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            activeEnemies.RemoveAll(enemy => enemy == null);
            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        activeEnemies.Add(newEnemy);
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }


}
