using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScannerCollisionHandler : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Обнаружен враг: " + other.name);

            // Ищем систему частиц в дочерних объектах
            ParticleSystem enemyPS = other.GetComponentInChildren<ParticleSystem>();
            if (enemyPS != null)
            {
                // Запускаем систему частиц
                enemyPS.Play();
                Debug.Log("Система частиц запущена на: " + other.name);
            }
            else
            {
                Debug.Log("Система частиц не найдена на: " + other.name);
            }
        }
    }
}
