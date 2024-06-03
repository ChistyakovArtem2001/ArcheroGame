using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int all_score = 100;
    public TextMeshProUGUI healthText; // ������ �� ��������� ������� ��� ����������� ��������

    public Text victoryText; // ������ �� ��������� ������� ��� ������
    public Text defeatText;  // ������ �� ��������� ������� ��� ���������

    private void Start()
    {
        UpdateHealthText(5);
        if (victoryText != null)
        {
            victoryText.enabled = false; // �������� ����� ������ ��� ������
        }
        if (defeatText != null)
        {
            defeatText.enabled = false; // �������� ����� ��������� ��� ������
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Player taking damage: {damage}");
        health -= damage;
        UpdateHealthText(damage); // ��������� ����� �������� ����� ��������� �����
        Debug.Log($"Player health: {health}");
        if (health <= 0)
        {
            health = 0;
            Debug.Log("BB You Dead");
            if (defeatText != null)
            {
                health = 0;
                defeatText.enabled = true; // �������� ��������� ���� ��� ���������
                StartCoroutine(LoadPreviousSceneCoroutine());

            }
        }
    }

    public void OnEnemyKilled()
    {
        if (ScoreManager.instance.score >= 100)
        {
            victoryText.enabled = true; // �������� ��������� ���� ��� ������
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
                healthText.text = "HP: " + health; // ��������� ��������� ���� ��������
            }
            
        }
    }

    private IEnumerator LoadPreviousSceneCoroutine()
    {
        // ��������� 3 �������
        yield return new WaitForSeconds(3f);

        // ��������� ������� ������� �����
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ������������ ������ ���������� �����
        int previousSceneIndex = currentSceneIndex - 1;

        // ��������, ���������� �� ���������� �����
        if (previousSceneIndex >= 0)
        {
            // �������� ����� ����������
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(previousSceneIndex);

            // �������� ���������� ��������
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("��� ���������� ����� ��� ��������.");
        }
    }
}
