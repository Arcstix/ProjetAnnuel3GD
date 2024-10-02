using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    private Shooter _shooterRef;
    private bool _hasCollide = false;
    private Vector3 _directionRef;

    private void GetRigidbody()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(bool useGravity, Shooter shooterRef, Vector3 direction)
    {
        _shooterRef = shooterRef;
        _directionRef = direction;
        if(_rb == null)
        {
            GetRigidbody();
        }

        _rb.useGravity = useGravity;

        if (useGravity)
        {
            Throw(direction);
        }
        else
        {
            Move(direction);
        }
    }


    private void Throw(Vector3 direction)
    {
        _rb.AddForce(direction * _shooterRef.ShootPower, ForceMode.Impulse);
    }

    private void Move(Vector3 direction)
    {
        _rb.velocity = direction * _shooterRef.ShootPower;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
        {
            ProjectileCollide();
        }
    }

    private void ProjectileCollide()
    {
        _hasCollide = true;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
    }

    public void StopMovement()
    {
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
    }
}
