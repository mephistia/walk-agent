using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionWithBodyPart : MonoBehaviour
{
    Collider selfCollider;
    void Start()
    {
        selfCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BodyPart")
        {
            Physics.IgnoreCollision(collision.collider, selfCollider);
        }
    }


}
