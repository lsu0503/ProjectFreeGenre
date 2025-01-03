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
    public float dashForce; // Dash 힘
    public float dashDuration; // Dash 지속 시간
    public float dashEnergy; // Dash 시 소모되는 스태미나
    public Vector2 curMovementInput;
    public float inputThreashold;
    private Rigidbody rb;
    private bool isDashing = false;

    public bool isOnKnockback;

    private Animator animator;
    private SpriteRenderer spriteRenderer; // SpriteRenderer 참조
    private ParticleSystem runParticles; // Run 상태의 ParticleSystem
    private ParticleSystem dashParticles; // Dash 상태의 ParticleSystem
    private PlayerStat playerStat; // PlayerStat 참조
    public event Action<Vector2> OnDirectionChanged; // 진행 방향 이벤트

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // 자식 오브젝트의 SpriteRenderer 가져오기
        playerStat = GetComponent<PlayerStat>(); // PlayerStat 컴포넌트 가져오기

        // 자식 오브젝트에서 특정 이름을 가진 파티클 시스템 가져오기
        foreach (Transform child in transform)
        {
            if (child.name == "Run")
            {
                runParticles = child.GetComponent<ParticleSystem>();
                runParticles.Stop(); // 씬 시작 시 Run 파티클 정지
            }
            else if (child.name == "Dash")
            {
                dashParticles = child.GetComponent<ParticleSystem>();
                dashParticles.Stop(); // 씬 시작 시 Dash 파티클 정지
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

        // 이동 방향에 따라 스프라이트 반전 및 파티클 시스템 회전
        if (curMovementInput.x < 0)
        {
            spriteRenderer.flipX = true; // 왼쪽으로 이동 시 반전
            runParticles.transform.localScale = new Vector3(-1, 1, 1); // Run 파티클 시스템 반전
            dashParticles.transform.localScale = new Vector3(-1, 1, 1); // Dash 파티클 시스템 반전
        }
        else if (curMovementInput.x > 0)
        {
            spriteRenderer.flipX = false; // 오른쪽으로 이동 시 원래 방향
            runParticles.transform.localScale = new Vector3(1, 1, 1); // Run 파티클 시스템 원래 방향
            dashParticles.transform.localScale = new Vector3(1, 1, 1); // Dash 파티클 시스템 원래 방향
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
            // 스태미나가 충분한지 확인
            if (playerStat.playerUI.stamina.curValue >= dashEnergy)
            {
                Vector2 dashDirection2D = curMovementInput.normalized;
                Vector3 dashDirection = new Vector3(dashDirection2D.x, 0, dashDirection2D.y);
                
                // Dash 시 Sprint 메서드 호출하여 스태미나 감소
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