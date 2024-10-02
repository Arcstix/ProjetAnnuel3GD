using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int _movementSpeed = 2;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    [SerializeField] private int _checkGroundDistance = 1;
    [SerializeField] private LayerMask _walkableLayer;
    [SerializeField] private int _gravityDiviser = 4;

    [SerializeField] private Camera _camera;

    private CharacterController _controller;
    private Shooter _shooter;
    private Vector3 _currentDirection;
    private float turnSmoothVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * _checkGroundDistance);

        if(Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Vector3.down, out hit, _checkGroundDistance, _walkableLayer))
        {
            if (!_shooter.OnTransportationState)
            {
                Vector3 inputDirection = GetInputDirection();

                if (inputDirection.magnitude >= 0.1f)
                {
                    float targetAngle = HandleRotation(inputDirection);

                    Vector3 motion = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    _controller.Move((motion.normalized + Physics.gravity / _gravityDiviser) * _movementSpeed * Time.deltaTime);
                }
            }
        }
        else if(!_shooter.OnTransportationState)
        {
            HandleRotation(GetInputDirection());
            _controller.Move((Physics.gravity / _gravityDiviser) * Time.deltaTime);
        }
    }

    private float HandleRotation(Vector3 inputDirection)
    {
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        return targetAngle;
    }

    private static Vector3 GetInputDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        return inputDirection;
    }
}
