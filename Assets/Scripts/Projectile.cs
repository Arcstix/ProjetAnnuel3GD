using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;
    private Shooter _shooterRef;
    private bool _hasCollide = false;
    private bool _isReturning = false;

    public bool IsReturning { get => _isReturning; set => _isReturning = value; }

    private void GetRigidbody()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(bool useGravity, Shooter shooterRef, Vector3 direction)
    {
        _shooterRef = shooterRef;
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

    public void ReturnToPlayer()
    {
        _isReturning = true;
        GetComponent<Collider>().isTrigger = true;
        Vector3 direction = ((_shooterRef.transform.position + new Vector3(0, 0.5f, 0)) - transform.position).normalized;
        _rb.velocity = direction * _shooterRef.ReturnSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasCollide && !_isReturning)
        {
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
            {
                ProjectileCollide();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isReturning)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _shooterRef.ResetAbility();
                Destroy(gameObject);
            }
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
