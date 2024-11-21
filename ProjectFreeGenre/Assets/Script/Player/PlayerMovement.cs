using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float dashForce = 10f; // Dash ��
    public float dashDuration = 0.2f; // Dash ���� �ð�
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
    public event Action<Vector2> OnDirectionChanged; // ���� ���� �̺�Ʈ

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // �ڽ� ������Ʈ�� SpriteRenderer ��������

        // �ڽ� ������Ʈ���� Ư�� �̸��� ���� ��ƼŬ �ý��� ��������
        foreach (Transform child in transform)
        {
            if (child.name == "Run")
            {
                runParticles = child.GetComponent<ParticleSystem>();
            }
            else if (child.name == "Dash")
            {
                dashParticles = child.GetComponent<ParticleSystem>();
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }
        else
        {
            Dash();
        }

        animator.SetBool("isMoving", curMovementInput != Vector2.zero);

        // �̵� ���⿡ ���� ��������Ʈ ���� �� ��ƼŬ �ý��� ȸ��
        if (curMovementInput.x < 0)
        {
            spriteRenderer.flipX = true; // �������� �̵� �� ����
            runParticles.transform.rotation = Quaternion.Euler(0, 180, 0); // Run ��ƼŬ �ý��� ȸ��
            dashParticles.transform.rotation = Quaternion.Euler(0, 180, 0); // Dash ��ƼŬ �ý��� ȸ��
        }
        else if (curMovementInput.x > 0)
        {
            spriteRenderer.flipX = false; // ���������� �̵� �� ���� ����
            runParticles.transform.rotation = Quaternion.Euler(0, 0, 0); // Run ��ƼŬ �ý��� ȸ��
            dashParticles.transform.rotation = Quaternion.Euler(0, 0, 0); // Dash ��ƼŬ �ý��� ȸ��
        }

        // �̵� ���¿� ���� ��ƼŬ �ý��� ����
        if (curMovementInput != Vector2.zero && !isDashing)
        {
            if (!runParticles.isPlaying)
            {
                runParticles.Play(); // �̵� ���� �� Run ��ƼŬ ���
            }
            if (dashParticles.isPlaying)
            {
                dashParticles.Stop(); // Dash ��ƼŬ ����
            }
        }
        else if (curMovementInput == Vector2.zero && runParticles.isPlaying)
        {
            runParticles.Stop(); // ���� �� Run ��ƼŬ ����
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
            Vector2 dashDirection2D = curMovementInput.normalized;
            Vector3 dashDirection = new Vector3(dashDirection2D.x, 0, dashDirection2D.y);
            dashStartPos = transform.position;
            dashEndPos = transform.position + dashDirection * dashForce;
            dashTime = 0;
            isDashing = true;

            // Dash ������ ��ƼŬ ���
            if (!dashParticles.isPlaying)
            {
                dashParticles.Play();
            }
            if (runParticles.isPlaying)
            {
                runParticles.Stop(); // Run ��ƼŬ ����
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
