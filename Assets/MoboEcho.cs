using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoboEcho : MonoBehaviour
{
    public float waveFrequency = 2f; // Частота испускания звуковых волн
    public float maxWaveDistance = 10f; // Максимальная дальность волны
    public GameObject wavePrefab; // Префаб визуализации звуковой волны

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
