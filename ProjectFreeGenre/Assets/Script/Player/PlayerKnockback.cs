using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKnockback : MonoBehaviour, IKnockback
{
    private Player player;
    private Coroutine routine;

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        float adjustedForce = force / player.playerStat.knockBackResistance;

        // Rigidbody¿¡ ÈûÀ» °¡ÇÏ¿© ³Ë¹é Àû¿ë
        player.rb.AddForce(direction.normalized * adjustedForce, ForceMode.Impulse);

        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(KnockBackRecover());
    }

    public IEnumerator KnockBackRecover()
    {
        player.playerMove.isOnKnockback = true;
        yield return new WaitForSeconds(ConstantCollection.knockbackTime);
        player.playerMove.isOnKnockback = false;
    }

    public void StopRoutine()
    {
        StopAllCoroutines();
        player.playerMove.isOnKnockback = false;
    }
}
