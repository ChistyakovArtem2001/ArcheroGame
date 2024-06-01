using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToControl : MonoBehaviour
{
    public string sceneToLoad; // Название сцены для загрузки, устанавливается в инспекторе для кнопки Play

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("С кем то сталкиванетсяИ");
        if (other.CompareTag("Arrow"))
        {
            Debug.Log("ДА ЕЖ ЖИ");
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
        // Выход из игры
        Debug.Log("Exiting game...");
        Application.Quit();

        // Если вы запускаете в редакторе, закомментируйте следующую строку
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
