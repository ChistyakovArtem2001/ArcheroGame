using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveHelicopterToWin : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartMoveToWin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartMoveToWin()
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("LetsGo", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

}
