using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private Rigidbody _rb;
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
        _playerRef = playerRef;
        _abilityData = playerRef.Metrics.CurrentPlayerSO.AbilityData;
        _direction = direction;
        _spawnPosition = spawnPosition;
    }

    private void Update()
    {
        if (_abilityData.TransportationData.AutomaticTransportation)
        {
            if (CheckTravelDistance())
            {
                _hasCollide = true;
                _playerRef.playerAbilityStateMachine.ChangeState(_playerRef.playerAbilityStateMachine.TransportationState);
            }
        }
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
                //_playerRef.playerAbilityStateMachine.ChangeState(_playerRef.playerAbilityStateMachine.TransportationState);
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_playerRef.ReusableData.OnTransportation)
            {
                _playerRef.CancelCallBack();
                Destroy(gameObject);
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
