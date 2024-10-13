using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerAbilityManager _playerRef;
    private PlayerAbilityData _abilityData;
    private Vector3 _direction;
    private bool _hasCollide = false;
    private bool _isReturning = false;

    public bool IsReturning { get => _isReturning; set => _isReturning = value; }

    private void GetRigidbody()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(PlayerAbilityManager playerRef, Vector3 direction, Vector3 target = default)
    {
        GetRigidbody();
        _playerRef = playerRef;
        _abilityData = playerRef.PlayerSO.AbilityData;
        _direction = direction;
    }

    private void FixedUpdate()
    {
        if (!_hasCollide)
        {
            Move();
        }
    }

    private void MoveToTarget(Vector3 target)
    {
        //_rb.velocity = target * _shooterRef.ShootPower;
    }

    private void Throw(Vector3 direction)
    {
        //_rb.AddForce(direction * _shooterRef.ShootPower, ForceMode.Impulse);
    }

    private void Move()
    {
        _rb.AddForce(_direction * _abilityData.ShootData.BaseSpeed - _rb.velocity, ForceMode.VelocityChange);
    }

    public void ReturnToPlayer()
    {
        _isReturning = true;
        GetComponent<Collider>().isTrigger = true;
        Vector3 direction = ((_playerRef.transform.position + new Vector3(0, 0.5f, 0)) - transform.position).normalized;
        _rb.AddForce(direction * _abilityData.CancelAbilityData.BaseSpeed - _rb.velocity, ForceMode.VelocityChange);
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
        if (other.gameObject.CompareTag("Player"))
        {
            _playerRef.CancelCallBack();
            Destroy(gameObject);
        }
    }

    private void ProjectileCollide()
    {
        _hasCollide = true;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        GetComponent<Collider>().isTrigger = true;
    }

    public void StopMovement()
    {
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
    }
}
