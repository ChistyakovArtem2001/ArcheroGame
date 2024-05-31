using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ControlStatePlayer : MonoBehaviour
{
    StaminaBar stamina;
    MoveMain moveState;
    AttackController comboAttack;
    HealthBar canTakeDamage;

    void Start()
    {
        moveState = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveMain>();
        comboAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackController>();
        stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>();
        canTakeDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
    }

    public void LookMove() {

        moveState.isMove = false;
    }
    public void UnlookMove()
    {
        moveState.isMove = true;
    }
    public void LookRoll()
    {
        moveState.isRolling = true;
    }
    public void UnlookRoll()
    {
        moveState.isRolling = false;
    }
    public void LookAttack()
    {
        stamina.DecreasedStamina(1f);
        comboAttack.comboAttack = true;
    }
    public void UnlookAttack()
    {
        comboAttack.comboAttack = false;
    }

    public void LookAttack1()
    {
        comboAttack.canAttack = false;
    }
    public void UnlookAttack1()
    {
        comboAttack.canAttack = true;
    }
    public void UnlookCanTakeDamage()
    {
      
        canTakeDamage.canTakeDamage = true;
    }

    public void LookCanTakeDamage()
    {
        canTakeDamage.canTakeDamage = false;
    }

    public void LookJump()
    {
        moveState.canJump = false;
        stamina.DecreasedStamina(15f);
    }

    public void UnlookJump()
    {
        moveState.canJump = true;
    }
    public void ActivateTriggerAttack()
    {   
        comboAttack.childObject.GetComponent<MeshCollider>().isTrigger = true;
    }
    public void DiactivateTriggerAttack() 
    { 
        comboAttack.childObject.GetComponent<MeshCollider>().isTrigger = false; 
    }

}
