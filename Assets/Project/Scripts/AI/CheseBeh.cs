using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class CheseBeh : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float attackRange = 2f;
    float noVisibleRange = 5005f;
    private Vector3 spawnPoint;
    bool isAttackingEnrm = false;
    float attackTimer = 0f;
    float attackDelay = 10f;
    EnemyScript2 enemyScript;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        enemyScript = animator.GetComponent<EnemyScript2>();
        spawnPoint = animator.GetComponent<EnemyScript2>().startPoint;
        agent.speed = 4;
        player = GameObject.FindGameObjectWithTag("Castle").transform;
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Castle").transform;
        }
        float Distance = Vector3.Distance(animator.transform.position, player.position);
        float DistanceToHome = Vector3.Distance(animator.transform.position, spawnPoint);

        if (Distance > noVisibleRange)
        {
            if (DistanceToHome < 1)
            {
                animator.SetFloat("speed", 0f);
            }
            else
            {
                animator.SetFloat("speed", 0.2f);
                
                //Debug.Log("До дома" +  DistanceToHome);
                //Debug.Log("spawnPoint " + spawnPoint);
                agent.SetDestination(spawnPoint);
            }
        }

        if (isAttackingEnrm)
        {
            attackTimer -= Time.deltaTime;
            //Debug.Log(AttackTimer);
            //Debug.Log(attackTimer);
            if (attackTimer <= 0)
            {
                isAttackingEnrm = false;
            }
        }

        if (Distance < attackRange && !isAttackingEnrm)
        {
            agent.SetDestination(player.position);
            animator.SetBool("IsAttaking", true);
            isAttackingEnrm = true;
            attackTimer = Random.Range(0, 2f);
        }

        if (Distance > attackRange & noVisibleRange > Distance)
        {
            agent.SetDestination(player.position);
            //Debug.Log("Надо бежать");
            animator.SetFloat("speed", 1f);
        }

        if (enemyScript.HP <= 0)
        {
            animator.SetBool("IsAttaking", false);
            enemyScript.isDeathEnemy = true;
        }


    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 2;
    }


}
