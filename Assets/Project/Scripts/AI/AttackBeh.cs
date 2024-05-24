using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackBeh : StateMachineBehaviour
{
    MeshCollider collider;
    Transform player;
    EnemyScript2 enemyScript;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Castle").transform;
        collider = animator.GetComponentInChildren<MeshCollider>();
        collider.isTrigger = true;
        enemyScript = animator.GetComponent<EnemyScript2>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 playerPosition = player.position;
        playerPosition.y = animator.transform.position.y; // Установка одинаковой высоты, чтобы игнорировать изменения по оси Y
        animator.transform.LookAt(playerPosition);
        //animator.transform.LookAt(player);
        float Distance = Vector3.Distance(animator.transform.position, player.position);
        if (Distance > 0)
        {
            animator.SetBool("IsAttaking", false);  
        }
        if (enemyScript.HP <= 0)
        {
            animator.SetBool("IsAttaking", false);
            enemyScript.isDeathEnemy = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        collider.isTrigger = false;
    }
}
