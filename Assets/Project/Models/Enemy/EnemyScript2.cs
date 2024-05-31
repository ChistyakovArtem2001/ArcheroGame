using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript2 : MonoBehaviour, IHittable
{
    [SerializeField]
    private int damage = 1;
    Animator anim;
    public float HP = 100;
    //public Slider healthBar;
    NavMeshAgent agent;
    Rigidbody[] rb;
    public Vector3 startPoint;
    CapsuleCollider BC;
    public bool isDeathEnemy = false;

    public GameObject deathParticlesPrefab; // Префаб системы частиц

    private PlayerHealth playerHealth;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponentsInChildren<Rigidbody>();
        BC = GetComponent<CapsuleCollider>();
        startPoint = transform.position;

        GameObject player = GameObject.FindWithTag("Player"); // Предполагается, что у игрока есть тег "Player"
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        //healthBar.value = HP;
        if (HP <= 0 && !isDeathEnemy)
        {
            StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter(Collider Other)
    {
        Debug.Log($"Collision with: {Other.gameObject.name}");
        if (Other.gameObject.CompareTag("Player"))
        {
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
        TakeDamage(1); // Уменьшение здоровья на 1 при попадании. Вы можете изменить это значение по вашему усмотрению.
    }

    public void TakeDamage(int damageAmount)
    {
        if (HP > 0 && !isDeathEnemy)
        {
            HP -= damageAmount;
        }
    }

    public void LookTrigers()
    {
        BC.isTrigger = false;
    }

    public void UnlookTrigers()
    {
        BC.isTrigger = true;
    }

    IEnumerator Death()
    {
        anim.SetBool("IsDeath", true);
        isDeathEnemy = true;

        // Воспроизведение системы частиц
        if (deathParticlesPrefab != null)
        {
            GameObject particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 4f); // Уничтожаем систему частиц через 2 секунды
        }

        ScoreManager.instance.AddScore(100);

        if (playerHealth != null)
        {
            playerHealth.OnEnemyKilled(); // Уведомляем игрока о смерти врага

        }

        yield return new WaitForSeconds(4f); // Ждем перед уничтожением врага

        Destroy(gameObject);
    }
}
