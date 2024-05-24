using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoboEcho : MonoBehaviour
{
    public float waveFrequency = 2f; // ������� ���������� �������� ����
    public float maxWaveDistance = 10f; // ������������ ��������� �����
    public GameObject wavePrefab; // ������ ������������ �������� �����

    private Collider mobCollider;

    private void Start()
    {
        mobCollider = GetComponent<Collider>();
        StartCoroutine(EmitWaves());
    }

    private IEnumerator EmitWaves()
    {
        while (true)
        {
            EmitWave();
            yield return new WaitForSeconds(waveFrequency);
        }
    }

    private void EmitWave()
    {
        GameObject wave = Instantiate(wavePrefab, transform.position, Quaternion.identity);
        Wave waveScript = wave.GetComponent<Wave>();
        waveScript.Initialize(maxWaveDistance, mobCollider);
    }
}
