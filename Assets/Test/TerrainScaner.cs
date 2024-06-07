using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class TerrainScaner : MonoBehaviour
{
    public GameObject TerrainScannerPrefab;
    public float duration = 10;
    public float size = 500;
    public float scanRadius = 100; // Радиус сканирования
    public InputHelpers.Button buttonToBind;
    public GameObject vrController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {
        bool isButtonPressed = false;
        if (Input.GetKeyUp(KeyCode.Space) || InputHelpers.IsPressed(vrController.GetComponent<InputDevice>(), buttonToBind, out isButtonPressed))
        {
            SpawnTerrainScanner();
        }
    }

    void SpawnTerrainScanner()
    {
        GameObject terScanner = Instantiate(TerrainScannerPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        ParticleSystem terScannerPS = terScanner.transform.GetChild(0).GetComponent<ParticleSystem>();

        if (terScannerPS != null)
        {
            var main = terScannerPS.main;
            main.startLifetime = duration;
            main.startSize = size;
        }
        else
        {
            Debug.Log("НЕ ИМЕЕТ СИСТЕММУ ЧАСТИЦ");
        }

        // Обнаружение объектов с тегом "Enemy"
        DetectEnemies();

        Destroy(terScanner, duration);
    }

    void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, scanRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Debug.Log("Обнаружен враг: " + hitCollider.name);

                // Ищем систему частиц в дочерних объектах
                ParticleSystem enemyPS = hitCollider.GetComponentInChildren<ParticleSystem>();
                if (enemyPS != null)
                {
                    // Запускаем систему частиц
                    enemyPS.Play();
                    Debug.Log("Система частиц запущена на: " + hitCollider.name);
                }
                else
                {
                    Debug.Log("Система частиц не найдена на: " + hitCollider.name);
                }
            }
        }
    }
}

//{
//    public GameObject TerrainScannerPrefab;
//    public float duration = 10;
//    public float size = 500;
//    public float scanRadius = 100; // Радиус сканирования

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyUp(KeyCode.Space))
//        {
//            SpawnTerrainScanner();
//        }
//    }

//    void SpawnTerrainScanner()
//    {
//        GameObject terScanner = Instantiate(TerrainScannerPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
//        ParticleSystem terScannerPS = terScanner.transform.GetChild(0).GetComponent<ParticleSystem>();

//        if (terScannerPS != null)
//        {
//            var main = terScannerPS.main;
//            main.startLifetime = duration;
//            main.startSize = size;

//            // Включаем обнаружение столкновений
//            var collision = terScannerPS.collision;
//            collision.enabled = true;
//            collision.type = ParticleSystemCollisionType.World;
//            collision.sendCollisionMessages = true;

//            // Назначаем скрипт для обработки столкновений
//            TerrainScannerCollisionHandler collisionHandler = terScanner.GetComponent<TerrainScannerCollisionHandler>();
//            if (collisionHandler == null)
//            {
//                collisionHandler = terScanner.AddComponent<TerrainScannerCollisionHandler>();
//            }
//        }
//        else
//        {
//            Debug.Log("НЕ ИМЕЕТ СИСТЕММУ ЧАСТИЦ");
//        }

//        Destroy(terScanner, duration);
//    }
//}

