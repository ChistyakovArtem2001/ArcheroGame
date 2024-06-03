using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int all_score = 100;
    public TextMeshProUGUI healthText; // Ссылка на текстовый элемент для отображения здоровья

    public Text victoryText; // Ссылка на текстовый элемент для победы
    public Text defeatText;  // Ссылка на текстовый элемент для поражения

    private void Start()
    {
        UpdateHealthText(5);
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
        UpdateHealthText(damage); // Обновляем текст здоровья после получения урона
        Debug.Log($"Player health: {health}");
        if (health <= 0)
        {
            health = 0;
            Debug.Log("BB You Dead");
            if (defeatText != null)
            {
                health = 0;
                defeatText.enabled = true; // Включаем текстовое поле для поражения
                StartCoroutine(LoadPreviousSceneCoroutine());

            }
        }
    }

    public void OnEnemyKilled()
    {
        if (ScoreManager.instance.score >= 100)
        {
            victoryText.enabled = true; // Включаем текстовое поле для победы
            StartCoroutine(LoadPreviousSceneCoroutine());
        }
    }

    private void UpdateHealthText(int damage)
    {
        if (healthText != null)
        {
            if (health <= damage)
            {
                healthText.text = "HP: " + 0;
            }
            else
            {
                healthText.text = "HP: " + health; // Обновляем текстовое поле здоровья
            }
            
        }
    }

    private IEnumerator LoadPreviousSceneCoroutine()
    {
        // Подождать 3 секунды
        yield return new WaitForSeconds(3f);

        // Получение индекса текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Рассчитываем индекс предыдущей сцены
        int previousSceneIndex = currentSceneIndex - 1;

        // Проверка, существует ли предыдущая сцена
        if (previousSceneIndex >= 0)
        {
            // Загрузка сцены асинхронно
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(previousSceneIndex);

            // Ожидание завершения загрузки
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("Нет предыдущей сцены для загрузки.");
        }
    }
}
