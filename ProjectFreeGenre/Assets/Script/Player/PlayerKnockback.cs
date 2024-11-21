using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        float adjustedForce = force / player.playerStat.knockBackResistance;

        // Rigidbody�� ���� ���Ͽ� �˹� ����
        player.rb.AddForce(direction.normalized * adjustedForce, ForceMode.Impulse);
    }
}
