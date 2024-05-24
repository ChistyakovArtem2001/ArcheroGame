using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelicopterMove : MonoBehaviour
{
    public bool exitActivate = false;
    Animator anim;
    public Camera camera1;
    public Camera camera2;
    GameObject player;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void StartAnimation()
    {
        anim.SetBool("IsMove", true);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Player" & exitActivate)
        {
            StartCoroutine(StartEnd());
        }
    }

    IEnumerator StartEnd()
    {
        yield return new WaitForSeconds(2f);
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;
        player.SetActive(false);
        yield return new WaitForSeconds(5f);
        anim.SetBool("LetsGo", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
