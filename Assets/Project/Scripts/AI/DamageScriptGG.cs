using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScriptGG : MonoBehaviour
{
    public int damageAmounts = 5;


    private void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Castle")
        {
           Debug.Log("Урон проходит");
           Debug.Log($"Collision with: {Other.gameObject.name}");
            if (Other.tag == "Castle")
            {

                PlayerHealth collisionPlayerHealth = Other.gameObject.GetComponent<PlayerHealth>();
                if (collisionPlayerHealth != null)
                {
                    Debug.Log("PlayerHealth component found");
                    collisionPlayerHealth.TakeDamage(damageAmounts);
                }
                else
                {
                    Debug.LogError("PlayerHealth component not found on player");
                }
            }
        }
        //Debug.Log("Урон проходит");
    }
}
