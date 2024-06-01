using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damageAmount = 20;
    ControlStatePlayer DS;
    public void Start()
    {
        DS = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlStatePlayer>();
    }
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Enemy") {
            Other.GetComponent<EnemyScript2>().TakeDamage(damageAmount);
        
        }

    }
}
