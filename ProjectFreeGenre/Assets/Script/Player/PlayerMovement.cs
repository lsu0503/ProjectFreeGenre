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
    public event Action<Vector2> OnDirectionChanged; // ���� ���� �̺�Ʈ

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // �ڽ� ������Ʈ�� SpriteRenderer ��������
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

        // �̵� ���⿡ ���� ��������Ʈ ����
        if (curMovementInput.x < 0)
        {
            spriteRenderer.flipX = true; // �������� �̵� �� ����
        }
        else if (curMovementInput.x > 0)
        {
            spriteRenderer.flipX = false; // ���������� �̵� �� ���� ����
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
