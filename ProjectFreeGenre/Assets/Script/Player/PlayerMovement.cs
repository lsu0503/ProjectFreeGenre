using System;
using System.Collections;
using System.Collections.Generic;
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
    private Rigidbody rb;
    private bool isDashing = false;
    private Vector3 dashStartPos;
    private Vector3 dashEndPos;
    private float dashTime;

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

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
            runParticles.Play();
            dashParticles.Stop();
        }
        else
        {
            Dash();
            dashParticles.Play();
            runParticles.Stop();
        }

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
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            OnDirectionChanged?.Invoke(curMovementInput);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isDashing)
        {
            // ���¹̳��� ������� Ȯ��
            if (playerStat.playerUI.stamina.curValue >= dashEnergy)
            {
                Vector2 dashDirection2D = curMovementInput.normalized;
                Vector3 dashDirection = new Vector3(dashDirection2D.x, 0, dashDirection2D.y);
                dashStartPos = transform.position;
                dashEndPos = transform.position + dashDirection * dashForce;
                dashTime = 0;
                isDashing = true;

                // Dash �� Sprint �޼��� ȣ���Ͽ� ���¹̳� ����
                playerStat.Sprint(dashEnergy);
            }
        }
    }

    void Dash()
    {
        dashTime += Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(dashStartPos, dashEndPos, dashTime);

        if (dashTime >= dashDuration)
        {
            isDashing = false;
        }
    }
}
