using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class MoveMain : MonoBehaviour
{
    public float ìoveSpeedWalk = 5f;
    public float moveSpeedRun = 5f;
    public bool canJump = true;
    public float jumpForce = 7f;
    public bool isGround;
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask ground;
    private Camera mainCamera;
    private Animator animator;
    public Animator anim;
    public bool canRun = true;
    public bool isRolling = false;
    public bool isMove = true; 
    public float waitSecondRoll = 2f;
    AttackController comboAttack;
    StaminaBar stamina;
    Rigidbody rb;

    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
        comboAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackController>();
        stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaBar>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isMove)
        {
            Move();
            stamina.IncreasedStamina(0.5f);
        }
        if (!isRolling && Input.GetKeyDown(KeyCode.Space))
        {
            stamina.DecreasedStamina(10f);
            StartCoroutine(Roll());
        }
        Jump();
    }


    public void Move()
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = cameraForward * verticalInput + cameraRight * horizontalInput;
        if (moveDirection.magnitude > Mathf.Abs(0.01f)) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * 500f);
        moveDirection.Normalize();
        animator.SetFloat("speed", Vector3.ClampMagnitude(moveDirection, 0.2f).magnitude);
        transform.Translate(moveDirection * ìoveSpeedWalk * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.LeftControl) && canRun)
        {
            stamina.DecreasedStamina(0.2f);
            animator.SetFloat("speed", Vector3.ClampMagnitude(moveDirection, 1).magnitude);
            transform.Translate(moveDirection * (ìoveSpeedWalk + moveSpeedRun) * Time.deltaTime, Space.World);
        }
    }

    IEnumerator Roll()
    {
        isRolling = true;
        anim.SetTrigger("Rolls");
        yield return new WaitForSeconds(waitSecondRoll);
        isRolling = false;
    }

    public void Jump()
    {
        CheckingGround();

        if (Input.GetKeyDown(KeyCode.LeftShift)&& isGround && canJump)
        {
            anim.SetTrigger("OnGround");
            rb.AddForce(Vector3.up * jumpForce);
            print(rb.position);
        }
    }

    public float rayLength = 100f;
    public float capsuleHeight = 0.1f;
    public float capsuleRadius = 0.5f; 
    public LayerMask groundedLayer;

    public void CheckingGround() 
    {
        Vector3 rayStart = transform.position;
        Vector3 rayDirection = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(rayStart, rayDirection, out hit, rayLength, groundedLayer))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
}
    