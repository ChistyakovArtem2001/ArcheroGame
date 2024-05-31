using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator animator;
    private bool isAttack = false;
    public bool comboAttack = false;
    public bool canAttack = true;
    StaminaBar stamina;
    public GameObject childObject;

     void Start()
    {
        animator = GetComponent<Animator>();
        stamina = GameObject.FindGameObjectWithTag("Castle").GetComponent<StaminaBar>();
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {     
            if (!comboAttack)
            {
                comboAttack = true;
                StartAttackAnimation();
                animator.ResetTrigger("IsAttack");
            }
            else{
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    Invoke("ContinueComboAttack", 0.3f);
                }
            }
        }
    }

    private void StartAttackAnimation()
    {
        if (comboAttack)
        {
            animator.SetTrigger("IsAttack1");
        }
    }

    private void ContinueComboAttack()
    {
        animator.SetTrigger("IsAttack");
    }
}

