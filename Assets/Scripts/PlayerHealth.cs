using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;

    public void TakeDamage(int damage)
    {
        Debug.Log($"Player taking damage: {damage}");
        health -= damage;
        Debug.Log($"Player health: {health}");
        if (health <= 0)
        {
            Debug.Log("BB You Dead");
        }
    }
}
