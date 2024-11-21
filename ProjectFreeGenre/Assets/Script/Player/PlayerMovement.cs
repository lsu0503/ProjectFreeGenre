using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float dashForce = 10f; // Dash 힘
    public float dashDuration = 0.2f; // Dash 지속 시간
    public Vector2 curMovementInput;
    private Rigidbody rb;
    private bool isDashing = false;
    private Vector3 dashStartPos;
    private Vector3 dashEndPos;
    private float dashTime;

    private Animator animator;
    private SpriteRenderer spriteRenderer; // SpriteRenderer 참조
    private ParticleSystem runParticles; // Run 상태의 ParticleSystem
    private ParticleSystem dashParticles; // Dash 상태의 ParticleSystem
    public event Action<Vector2> OnDirectionChanged; // 진행 방향 이벤트

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // 자식 오브젝트의 SpriteRenderer 가져오기

        // 자식 오브젝트에서 특정 이름을 가진 파티클 시스템 가져오기
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

        // 이동 방향에 따라 스프라이트 반전 및 파티클 시스템 회전
        if (curMovementInput.x < 0)
        {
            spriteRenderer.flipX = true; // 왼쪽으로 이동 시 반전
            runParticles.transform.rotation = Quaternion.Euler(0, 180, 0); // Run 파티클 시스템 회전
            dashParticles.transform.rotation = Quaternion.Euler(0, 180, 0); // Dash 파티클 시스템 회전
        }
        else if (curMovementInput.x > 0)
        {
            spriteRenderer.flipX = false; // 오른쪽으로 이동 시 원래 방향
            runParticles.transform.rotation = Quaternion.Euler(0, 0, 0); // Run 파티클 시스템 회전
            dashParticles.transform.rotation = Quaternion.Euler(0, 0, 0); // Dash 파티클 시스템 회전
        }

        // 이동 상태에 따라 파티클 시스템 제어
        if (curMovementInput != Vector2.zero && !isDashing)
        {
            if (!runParticles.isPlaying)
            {
                runParticles.Play(); // 이동 중일 때 Run 파티클 재생
            }
            if (dashParticles.isPlaying)
            {
                dashParticles.Stop(); // Dash 파티클 정지
            }
        }
        else if (curMovementInput == Vector2.zero && runParticles.isPlaying)
        {
            runParticles.Stop(); // 멈출 때 Run 파티클 정지
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

            // Dash 상태의 파티클 재생
            if (!dashParticles.isPlaying)
            {
                dashParticles.Play();
            }
            if (runParticles.isPlaying)
            {
                runParticles.Stop(); // Run 파티클 정지
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
