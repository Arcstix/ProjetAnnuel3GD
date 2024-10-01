using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

// envoie un projectile et est transporté jusqu'à la position du projectile
public class Shooter : MonoBehaviour
{
    [Header("Projectile section")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _launchPosition;
    [SerializeField] private bool _useGravity;

    [Header("Camera section")]
    [SerializeField] private CinemachineVirtualCameraBase _freeLookCamera;
    [SerializeField] private CinemachineVirtualCameraBase _targetCamera;

    [Header("Metrics section")]
    [SerializeField] private int _shootPower = 5;
    [SerializeField] private int _transportationSpeed = 5;
    [SerializeField] private float _slowTimeScale = 0.2f;

    [Header("State section")]
    [SerializeField] private bool _onFreeLook = true;
    [SerializeField] private bool _onTarget = false;
    [SerializeField] private bool _isShooting = false;
    [SerializeField] private bool _hasShoot = false;
    [SerializeField] private bool _onTransportationState = false;
    [SerializeField] private bool _isDead = false;


    public void ResetAbility()
    {
        _isShooting = false;
        _hasShoot = false;
    }
}
