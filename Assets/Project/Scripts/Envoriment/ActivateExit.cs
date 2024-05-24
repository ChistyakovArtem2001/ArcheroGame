using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateExit : MonoBehaviour
{
    public HelicopterMove HM;
    
    public void Start()
    {
        HM = GameObject.FindGameObjectWithTag("Helicopter").GetComponent< HelicopterMove>();
    }
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Player")
        {
            Debug.Log("Найдите вертолет");
            HM.StartAnimation();
            HM.exitActivate = true;
           
        }

    }
}
