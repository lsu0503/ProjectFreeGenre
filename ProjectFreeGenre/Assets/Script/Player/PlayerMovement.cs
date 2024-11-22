using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float dashForce; // Dash ��
    public float dashDuration; // Dash ���� �ð�
    public float dashEnergy; // Dash �� �Ҹ�Ǵ� ���¹̳�
    public Vector2 curMovementInput;
    public float inputThreashold;
    private Rigidbody rb;
    private bool isDashing = false;

    public bool isOnKnockback;

    private Animator animator;
    private SpriteRenderer spriteRenderer; // SpriteRenderer ����
    private ParticleSystem runParticles; // Run ������ ParticleSystem
    private ParticleSystem dashParticles; // Dash ������ ParticleSystem
    private PlayerStat playerStat; // PlayerStat ����
    public event Action<Vector2> OnDirectionChanged; // ���� ���� �̺�Ʈ

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // �ڽ� ������Ʈ�� SpriteRenderer ��������
        playerStat = GetComponent<PlayerStat>(); // PlayerStat ������Ʈ ��������

        // �ڽ� ������Ʈ���� Ư�� �̸��� ���� ��ƼŬ �ý��� ��������
        foreach (Transform child in transform)
        {
            if (child.name == "Run")
            {
                runParticles = child.GetComponent<ParticleSystem>();
                runParticles.Stop(); // �� ���� �� Run ��ƼŬ ����
            }
            else if (child.name == "Dash")
            {
                dashParticles = child.GetComponent<ParticleSystem>();
                dashParticles.Stop(); // �� ���� �� Dash ��ƼŬ ����
            }
        }
    }

    private void Start()
    {
        isOnKnockback = false;
    }

    void FixedUpdate()
    {
        if (!isDashing)
            Move();

        animator.SetBool("isMoving", curMovementInput != Vector2.zero);

        // �̵� ���⿡ ���� ��������Ʈ ���� �� ��ƼŬ �ý��� ȸ��
        if (curMovementInput.x < 0)
        {
            spriteRenderer.flipX = true; // �������� �̵� �� ����
            runParticles.transform.localScale = new Vector3(-1, 1, 1); // Run ��ƼŬ �ý��� ����
            dashParticles.transform.localScale = new Vector3(-1, 1, 1); // Dash ��ƼŬ �ý��� ����
        }
        else if (curMovementInput.x > 0)
        {
            spriteRenderer.flipX = false; // ���������� �̵� �� ���� ����
            runParticles.transform.localScale = new Vector3(1, 1, 1); // Run ��ƼŬ �ý��� ���� ����
            dashParticles.transform.localScale = new Vector3(1, 1, 1); // Dash ��ƼŬ �ý��� ���� ����
        }
    }

    void Move()
    {
        if (isOnKnockback) return;

        Vector3 dir = new Vector3(curMovementInput.x, 0.0f, curMovementInput.y);
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isOnKnockback)
        {
            curMovementInput = Vector2.zero;
            runParticles.Stop();

            return;
        }

        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();

            if (inputVector.magnitude > inputThreashold)
            {
                runParticles.Play();
                curMovementInput = inputVector;
                OnDirectionChanged?.Invoke(curMovementInput);
            }

            else
            {
                curMovementInput = Vector2.zero;
                runParticles.Stop();
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            runParticles.Stop();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(isOnKnockback) return;

        if (context.phase == InputActionPhase.Performed && !isDashing)
        {
            // ���¹̳��� ������� Ȯ��
            if (playerStat.playerUI.stamina.curValue >= dashEnergy)
            {
                Vector2 dashDirection2D = curMovementInput.normalized;
                Vector3 dashDirection = new Vector3(dashDirection2D.x, 0, dashDirection2D.y);
                
                // Dash �� Sprint �޼��� ȣ���Ͽ� ���¹̳� ����
                playerStat.Sprint(dashEnergy);

                StartCoroutine(Dash(dashDirection));
            }
        }
    }

    private IEnumerator Dash(Vector3 dashDirection)
    {
        isDashing = true;
        dashParticles.Play();
        rb.velocity = dashDirection * dashForce;

        yield return new WaitForSeconds(dashDuration);
        dashParticles.Stop();
        isDashing = false;
    }
}