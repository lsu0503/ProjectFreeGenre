using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterKnockBack : MonoBehaviour
{
    protected Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        KnockBack(collision);
    }

    protected abstract void KnockBack(Collision collision);
}
