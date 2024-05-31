using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float expansionSpeed = 5f; // Скорость расширения волны
    public Light waveLight; // Световой компонент волны
    private float maxDistance;
    private Vector3 initialScale;
    private Collider mobCollider;

    public void Initialize(float maxDistance, Collider mobCollider)
    {
        this.maxDistance = maxDistance;
        this.mobCollider = mobCollider;
        initialScale = transform.localScale;

        // Игнорирование коллизий с мобом
        Collider waveCollider = GetComponent<Collider>();
        if (waveCollider != null && mobCollider != null)
        {
            Physics.IgnoreCollision(waveCollider, mobCollider);
        }

        // Включение светового компонента
        if (waveLight != null)
        {
            waveLight.enabled = true;
        }
    }

    private void Update()
    {
        float expansion = expansionSpeed * Time.deltaTime;
        transform.localScale += new Vector3(expansion, expansion, expansion);

        if (transform.localScale.x >= initialScale.x + maxDistance)
        {
            Destroy(gameObject);
        }

        CheckCollisions();
    }

    private void CheckCollisions()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != gameObject && hitCollider != mobCollider)
            {
                // Обработка столкновений с объектами
                Debug.Log("Wave hit: " + hitCollider.gameObject.name);
                // Здесь можно добавить логику отражения или реакции на столкновение
            }
        }
    }
}
