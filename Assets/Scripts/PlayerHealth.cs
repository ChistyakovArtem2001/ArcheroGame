using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int all_score = 100;
    public TextMeshProUGUI healthText; // Ссылка на текстовый элемент для отображения здоровья

    public Text victoryText; // Ссылка на текстовый элемент для победы
    public Text defeatText;  // Ссылка на текстовый элемент для поражения

    private void Start()
    {
        UpdateHealthText();
        if (victoryText != null)
        {
            victoryText.enabled = false; // Скрываем текст победы при старте
        }
        if (defeatText != null)
        {
            defeatText.enabled = false; // Скрываем текст поражения при старте
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Player taking damage: {damage}");
        health -= damage;
        UpdateHealthText(); // Обновляем текст здоровья после получения урона
        Debug.Log($"Player health: {health}");
        if (health <= 0)
        {
            Debug.Log("BB You Dead");
            if (defeatText != null)
            {
                defeatText.enabled = true; // Включаем текстовое поле для поражения
            }
        }
    }

    public void OnEnemyKilled()
    {
        if (ScoreManager.instance.score >= 100)
        {
            victoryText.enabled = true; // Включаем текстовое поле для победы
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + health; // Обновляем текстовое поле здоровья
        }
    }
}
