using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int all_score = 100;
    public TextMeshProUGUI healthText; // ������ �� ��������� ������� ��� ����������� ��������

    public Text victoryText; // ������ �� ��������� ������� ��� ������
    public Text defeatText;  // ������ �� ��������� ������� ��� ���������

    private void Start()
    {
        UpdateHealthText();
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
        UpdateHealthText(); // ��������� ����� �������� ����� ��������� �����
        Debug.Log($"Player health: {health}");
        if (health <= 0)
        {
            Debug.Log("BB You Dead");
            if (defeatText != null)
            {
                defeatText.enabled = true; // �������� ��������� ���� ��� ���������
            }
        }
    }

    public void OnEnemyKilled()
    {
        if (ScoreManager.instance.score >= 100)
        {
            victoryText.enabled = true; // �������� ��������� ���� ��� ������
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + health; // ��������� ��������� ���� ��������
        }
    }
}
