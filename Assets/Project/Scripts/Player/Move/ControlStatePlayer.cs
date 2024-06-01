using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ControlStatePlayer : MonoBehaviour
{

    public bool isDamage = false;
     

        public void CanDamage()
        {
            isDamage = true;
        }
        public void DontCanDamage()
        {
            isDamage = false;
        }



   

}
