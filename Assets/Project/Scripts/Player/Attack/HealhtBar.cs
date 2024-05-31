using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float HP = 100f;
    public float maxHealth = 100f;
    public float minHealth = 100f;
    public Image bar;
    public bool checkHealthVar = true;
    MoveMain moveState;
    AttackController comboAttack;
    public bool canTakeDamage = true;

    Animator Anim;
    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        moveState = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveMain>();
        comboAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackController>();
    }

    void Update()
    {
        ChechHP();
    }


    public void TakeDamage(int DamageAmount)
    {
        if (HP >= 0 & canTakeDamage)
        {
            HP -= DamageAmount;
            //Debug.Log("ХП героя" + HP);
            bar.fillAmount = HP / 100f;
        }
        if (HP <= 0 )
        {
            StartCoroutine(LoadMainMenu());
        }
    }

    public void ChechHP()
    {
        if (HP <= 0)
        {
            Anim.SetBool("IsDeath", true);
        }
    }


    IEnumerator LoadMainMenu()
    {

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
        


}
