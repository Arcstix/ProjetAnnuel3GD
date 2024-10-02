using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using DG.Tweening;

// envoie un projectile et est transporté jusqu'à la position du projectile
public class Shooter : MonoBehaviour
{
    [Header("Projectile section")]
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _launchPosition;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _useGravity;

    [Header("Camera section")]
    [SerializeField] private CinemachineVirtualCameraBase _freeLookCamera;
    [SerializeField] private CinemachineVirtualCameraBase _targetCamera;

    [Header("Metrics section")]
    [SerializeField] private int _shootPower = 5;
    [SerializeField] private int _transportationSpeed = 5;
    [SerializeField] private int _returnSpeed = 20;
    [SerializeField] private float _slowTimeScale = 0.2f;

    [Header("State section")]
    [SerializeField] private bool _onFreeLook = true;
    [SerializeField] private bool _onTarget = false;
    [SerializeField] private bool _isShooting = false;
    [SerializeField] private bool _hasShoot = false;
    [SerializeField] private bool _onTransportationState = false;
    [SerializeField] private bool _isReturning = false;
    [SerializeField] private bool _isDead = false;

    private GameplayManager _gameplayManager;
    private CharacterController _controller;
    private Projectile currentProjectile;

    public int ShootPower { get => _shootPower; }
    public int TransportationSpeed { get => _transportationSpeed; }
    public bool OnTransportationState { get => _onTransportationState; }
    public bool IsReturning { get => _isReturning; set => _isReturning = value; }
    public int ReturnSpeed { get => _returnSpeed; }

    private void Awake()
    {
        _gameplayManager = GetComponent<GameplayManager>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Debug.DrawLine(_launchPosition.position, _target.position);

        if (_gameplayManager.DefaultThrowBall)
        {
            if (_isReturning) { return; }

            if (!_isDead && !_hasShoot && !_onTransportationState)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ShootProjectile();
                    return;
                }
            }

            if (_isShooting)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _isShooting = false;
                    currentProjectile.StopMovement();
                    return;
                }
            }

            if (_hasShoot && !_isShooting && !_onTransportationState)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ResetAbility();
                    _onTransportationState = true;
                    return;
                }
            }
        }

        if(currentProjectile != null)
        {
            if (_hasShoot && !_onTransportationState && !currentProjectile.IsReturning)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _isReturning = true;
                    ReturnProjectile();
                }
            }
        }       

        if (_onTransportationState)
        {
            DoTransportation();
        }
    }

    private void ReturnProjectile()
    {
        currentProjectile.ReturnToPlayer();
    }

    /// <summary>
    /// Méthode utilisé quand on veut rejoindre un projectile
    /// </summary>
    private void DoTransportation()
    {        
        Vector3 direction = (currentProjectile.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, currentProjectile.transform.position);
        _controller.Move(direction * Mathf.Min(distance + 5, TransportationSpeed) * Time.deltaTime);

        if(distance < 0.5f)
        {
            _onTransportationState = false;
            Destroy(currentProjectile.gameObject);
        }
    }

    private void ShootProjectile()
    {
        ShootState();
        currentProjectile = Instantiate(_projectilePrefab, _launchPosition.position, Quaternion.identity);
        Vector3 direction = (_target.position - _launchPosition.position).normalized;
        Debug.Log("Direction : " + direction);
        currentProjectile.Init(_useGravity, this, direction);
    }

    private void ShootState()
    {
        _hasShoot = true;
        _isShooting = true;
    }

    public void ResetAbility()
    {
        _hasShoot = false;
        _isShooting = false;
        _isReturning = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_launchPosition.position, _target.position);
    }
}
