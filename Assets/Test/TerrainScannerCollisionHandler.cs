using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScannerCollisionHandler : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("��������� ����: " + other.name);

            // ���� ������� ������ � �������� ��������
            ParticleSystem enemyPS = other.GetComponentInChildren<ParticleSystem>();
            if (enemyPS != null)
            {
                // ��������� ������� ������
                enemyPS.Play();
                Debug.Log("������� ������ �������� ��: " + other.name);
            }
            else
            {
                Debug.Log("������� ������ �� ������� ��: " + other.name);
            }
        }
    }
}
