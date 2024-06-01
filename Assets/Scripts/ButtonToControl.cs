using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToControl : MonoBehaviour
{
    public string sceneToLoad; // �������� ����� ��� ��������, ��������������� � ���������� ��� ������ Play

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("� ��� �� ��������������");
        if (other.CompareTag("Arrow"))
        {
            Debug.Log("�� �� ��");
            if (gameObject.name == "PlayButton")
            {
                LoadGameScene();
            }
            else if (gameObject.name == "ExitButton")
            {
                ExitGame();
            }
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame()
    {
        // ����� �� ����
        Debug.Log("Exiting game...");
        Application.Quit();

        // ���� �� ���������� � ���������, ��������������� ��������� ������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
