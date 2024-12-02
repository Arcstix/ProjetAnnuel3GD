using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider projectileCollider;
    private PlayerAbilityManager _playerRef;
    private PlayerAbilityData _abilityData;
    private Vector3 _direction;
    private Vector3 _spawnPosition;
    private bool _hasCollide = false;
    private bool _isReturning = false;

    public bool IsReturning { get => _isReturning; set => _isReturning = value; }

    private void GetRigidbody()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(PlayerAbilityManager playerRef, Vector3 spawnPosition, Vector3 direction, Vector3 target = default)
    {
        GetRigidbody();
        projectileCollider = GetComponent<Collider>();
        _playerRef = playerRef;
        _abilityData = playerRef.Metrics.CurrentPlayerSO.AbilityData;
        _direction = direction;
        _spawnPosition = spawnPosition;
    }

    private bool CheckTravelDistance()
    {
        float distanceTravel = Vector3.Distance(_spawnPosition, transform.position);

        return distanceTravel >= _playerRef.Metrics.CurrentPlayerSO.AbilityData.ShootData.MaxTravelDistance;        
    }

    private void FixedUpdate()
    {
        if (!_hasCollide && !CheckTravelDistance())
        {
            MoveWithoutGravity();
        }
        else if(!_hasCollide && CheckTravelDistance())
        {
            MoveWithGravity();
        }
    }

    private void MoveWithoutGravity()
    {
        _rb.AddForce(_direction * _abilityData.ShootData.BaseSpeed - _rb.velocity, ForceMode.VelocityChange);
    }

    private void MoveWithGravity()
    {
        _rb.AddForce(_direction * _abilityData.ShootData.BaseSpeed + Physics.gravity - _rb.velocity, ForceMode.Acceleration);
    }

    public void MoveToPlayer()
    {
        projectileCollider.isTrigger = false;
        _rb.useGravity = true;
        Vector3 direction = ((_playerRef.transform.position + new Vector3(0, 0.5f, 0)) - transform.position).normalized;
        _rb.AddForce(direction * _abilityData.TransportationData.BaseSpeed - _rb.velocity, ForceMode.VelocityChange);
    }

    public void MoveToDirection(Vector3 direction, Vector3 startPosition, Vector3 endPosition)
    {
        if(Vector3.Distance(startPosition, endPosition) > 0.4f)
        {
            projectileCollider.isTrigger = false;
            _rb.useGravity = true;
            _rb.AddForce(direction * _abilityData.TransportationData.BaseSpeed - _rb.velocity, ForceMode.VelocityChange);
        }
        else
        {
            if (gameObject.transform.childCount > 0)
            {
                RemoveChildObject();
            }
            Destroy(_playerRef.ReusableData.RightProjectileRef.gameObject);
            Destroy(_playerRef.ReusableData.LeftProjectileRef.gameObject);
        }
    }

    public void ReturnToPlayer()
    {
        if (gameObject.transform.childCount > 0)
        {
            RemoveChildObject();
        }
        if (Vector3.Distance(gameObject.transform.position, _playerRef.transform.position) > 0.1f)
        {
            _isReturning = true;
            projectileCollider.isTrigger = true;
            Vector3 direction = ((_playerRef.transform.position + new Vector3(0, 0.5f, 0)) - transform.position).normalized;
            _rb.AddForce(direction * _abilityData.CancelAbilityData.BaseSpeed - _rb.velocity, ForceMode.VelocityChange);
        }
        else
        {
            if (gameObject.transform.childCount > 0)
            {
                RemoveChildObject();
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasCollide && !_isReturning)
        {
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Movable"))
            {
                ProjectileCollide();
                if (collision.gameObject.CompareTag("Movable"))
                {
                    SetParent(collision.gameObject);
                }              
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_playerRef.ReusableData.OnTransportation || _playerRef.ReusableData.OnLeftAttraction || _playerRef.ReusableData.OnRightAttraction)
            {
                if(gameObject.transform.childCount > 0)
                {
                    RemoveChildObject();
                }
                Destroy(gameObject);
            }
        }
    }

    private void RemoveChildObject()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.transform.parent = null;
        }
    }

    private void SetParent(GameObject objectCollide)
    {
        objectCollide.transform.parent = gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.transform.childCount > 0)
            {
                RemoveChildObject();
            }
            Destroy(gameObject);
        }
    }

    private void ProjectileCollide()
    {
        _hasCollide = true;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        projectileCollider.isTrigger = true;
    }

    public void StopMovement()
    {
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
    }
}
