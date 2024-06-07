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
    public float scanRadius = 100; // ������ ������������
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
            Debug.Log("�� ����� �������� ������");
        }

        // ����������� �������� � ����� "Enemy"
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
                Debug.Log("��������� ����: " + hitCollider.name);

                // ���� ������� ������ � �������� ��������
                ParticleSystem enemyPS = hitCollider.GetComponentInChildren<ParticleSystem>();
                if (enemyPS != null)
                {
                    // ��������� ������� ������
                    enemyPS.Play();
                    Debug.Log("������� ������ �������� ��: " + hitCollider.name);
                }
                else
                {
                    Debug.Log("������� ������ �� ������� ��: " + hitCollider.name);
                }
            }
        }
    }
}

//{
//    public GameObject TerrainScannerPrefab;
//    public float duration = 10;
//    public float size = 500;
//    public float scanRadius = 100; // ������ ������������

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

//            // �������� ����������� ������������
//            var collision = terScannerPS.collision;
//            collision.enabled = true;
//            collision.type = ParticleSystemCollisionType.World;
//            collision.sendCollisionMessages = true;

//            // ��������� ������ ��� ��������� ������������
//            TerrainScannerCollisionHandler collisionHandler = terScanner.GetComponent<TerrainScannerCollisionHandler>();
//            if (collisionHandler == null)
//            {
//                collisionHandler = terScanner.AddComponent<TerrainScannerCollisionHandler>();
//            }
//        }
//        else
//        {
//            Debug.Log("�� ����� �������� ������");
//        }

//        Destroy(terScanner, duration);
//    }
//}

