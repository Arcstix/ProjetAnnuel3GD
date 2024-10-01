using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    private Shooter _shooterRef;

    private void GetRigidbody()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(bool useGravity, Shooter shooterRef)
    {
        _shooterRef = shooterRef;
        if(_rb == null)
        {
            GetRigidbody();
        }

        _rb.useGravity = useGravity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
        {
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("Projectile Destroy");
            _shooterRef.ResetAbility();
            Destroy(gameObject);
        }
    }
}
