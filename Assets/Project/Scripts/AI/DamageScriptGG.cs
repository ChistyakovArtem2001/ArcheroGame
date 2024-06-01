using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScriptGG : MonoBehaviour
{
    public int damageAmounts = 5;
    private ControlStatePlayer player;

    private void Start()
    {
        // Получаем самый верхний родительский объект
        Transform topmostParent = transform;
        while (topmostParent.parent != null)
        {
            topmostParent = topmostParent.parent;
        }

        // Получаем компонент ControlStatePlayer на самом верхнем родительском объекте
        player = topmostParent.GetComponent<ControlStatePlayer>();

        if (player == null)
        {
            Debug.LogError("ControlStatePlayer component not found on the topmost parent object");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Castle")
        {
            Debug.Log("Урон проходит");
            Debug.Log($"Collision with: {other.gameObject.name}");

            PlayerHealth collisionPlayerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (collisionPlayerHealth != null)
            {
                if (player != null && player.isDamage)
                {
                    Debug.Log("PlayerHealth component found and player can take damage");
                    collisionPlayerHealth.TakeDamage(damageAmounts);
                }
                else
                {
                    Debug.Log("Player cannot take damage right now or player component is missing");
                }
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the collided object");
            }
        }
    }
}
