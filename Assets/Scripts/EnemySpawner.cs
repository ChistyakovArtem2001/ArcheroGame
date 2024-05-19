using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // ������ �����
    public Transform[] spawnPoints;  // ����� ������
    public int maxEnemies = 3;       // ������������ ���������� ������ ������������
    public float spawnInterval = 5f; // �������� ������

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
