using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour, IKnockback
{
    private Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        float adjustedForce = force;

        // Rigidbody�� ���� ���Ͽ� �˹� ����
        player.rb.AddForce(direction.normalized * adjustedForce, ForceMode.Impulse);
    }
}
