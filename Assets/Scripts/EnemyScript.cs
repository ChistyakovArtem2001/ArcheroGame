using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IHittable
{
    private Rigidbody rb;
    private Animator animator;
    private bool stopped = false;

    private Transform player;

    [SerializeField]
    private int health = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float attackDistance = 2f;

    [SerializeField]
    private int damage = 1;

    private PlayerHealth playerHealth;

    private EnemySpawner spawner;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindWithTag("Castle");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component not found on player");
            }
        }
        else
        {
            Debug.LogError("Player object not found");
        }

   
        spawner = FindObjectOfType<EnemySpawner>();
        if (spawner == null)
        {
            Debug.LogError("EnemySpawner not found in scene");
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log($"Collision with: {collision.gameObject.name}");
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        audioSource.Play();
    //        PlayerHealth collisionPlayerHealth = collision.gameObject.GetComponent<PlayerHealth>();
    //        if (collisionPlayerHealth != null)
    //        {
    //            Debug.Log("PlayerHealth component found");
    //            collisionPlayerHealth.TakeDamage(damage);
    //        }
    //        else
    //        {
    //            Debug.LogError("PlayerHealth component not found on player");
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider Other)
    {
        Debug.Log($"Collision with: {Other.gameObject.name}");
        if (Other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            PlayerHealth collisionPlayerHealth = Other.gameObject.GetComponent<PlayerHealth>();
            if (collisionPlayerHealth != null)
            {
                Debug.Log("PlayerHealth component found");
                collisionPlayerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on player");
            }
        }
    }

    public void GetHit()
    {
        health--;
        if (health <= 0)
        {
            rb.isKinematic = false;
            stopped = true;
            animator.SetTrigger("DieTrigger");
            StartCoroutine(DeathRoutine());
        }
    }

    private void FixedUpdate()
    {
        if (!stopped && player != null)
        {
            Vector3 direction = player.position - transform.position;
            

            if (direction.magnitude > attackDistance)
            {
                Vector3 newPosition = transform.position + direction.normalized * Time.fixedDeltaTime * speed;
                rb.MovePosition(newPosition);
                animator.SetBool("isMoving", true);
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            if (player == null)
            {
                Debug.LogError("Player is null");
            }
            if (stopped)
            {
                Debug.Log("Enemy is stopped");
                animator.SetBool("isMoving", false);
            }
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking player");

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log($"Player health after attack: {playerHealth.health}");
        }

        animator.SetTrigger("AttackTrigger");
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(2f);

        if (spawner != null)
        {
            spawner.OnEnemyDeath(this.gameObject);
        }

        Destroy(gameObject);
    }
}
