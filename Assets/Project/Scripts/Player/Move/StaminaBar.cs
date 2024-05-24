using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float minStamina = 100f;
    public Image bar;
    public bool checkStaminaVar = true;
    MoveMain moveState;
    AttackController comboAttack;
    void Start()
    {
        moveState = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveMain>();
        comboAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackController>();
    }

    void Update()
    {
        CheckStamina();
    }
     public void DecreasedStamina(float Decreased )
    {
        stamina -= Decreased;
        bar.fillAmount = stamina/100f;
        if (stamina < minStamina) stamina = minStamina;
    }
    public void IncreasedStamina(float Increased)
    {
        stamina += Increased;
        bar.fillAmount = stamina / 100f;
        if ( stamina > maxStamina ) stamina = maxStamina;
    }

    public void CheckStamina()
    {
        if(stamina < minStamina + 10 ) 
        {
            checkStaminaVar = true;
            moveState.canJump = false;
            comboAttack.canAttack = false;
            moveState.isRolling = true;
        }
        if (stamina > minStamina + 10 && checkStaminaVar)
        {
            moveState.canJump = true;
            comboAttack.canAttack = true;
            moveState.isRolling = false;
            checkStaminaVar = false;
        }
    }
}
