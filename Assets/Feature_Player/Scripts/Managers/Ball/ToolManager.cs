using System;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private float _ballSpeed;
    private float _transportObjectSpeed;
    private Vector3 _endPos;
    private Transform _launcherTransform;
    private GameObject _futurParent; // if the ball will be a child of an object interactable
    private bool _canBeActivate = false;
    private Vector3 refVelocity;
    private GameObject objectAutoAimed;
    private bool isLaunched = false;

    public event Action OnCollision;
    
    public void InitializeBall(float ballSpeed, float transportObjectSpeed, Vector3 endPos, Transform launcherTransform, GameObject futurParent,
        GameObject autoAimed = null)
    {
        _ballSpeed = ballSpeed;
        _transportObjectSpeed = transportObjectSpeed;
        _endPos = endPos;
        _launcherTransform = launcherTransform;
        _canBeActivate = false;

        if (autoAimed != null)
        {
            this.objectAutoAimed = autoAimed;
            _futurParent = autoAimed;
        }
        else
        {
            _futurParent = futurParent;
        }
    }
    
    public void SetNewInfo(Vector3 endPos, float speed, GameObject futurParent)
    {
        _endPos = endPos;
        _ballSpeed = speed;
        if (futurParent == null && transform.parent != null)
        {
            transform.SetParent(null);
        }
        _futurParent = futurParent;
        _canBeActivate = false;
    }

    public bool CanBeActivate()
    {
        return _canBeActivate;
    }
    
    private void Update()
    {
        if (_canBeActivate)
        {
            return;
        }
        
        if (Vector3.Distance(transform.position, _endPos) > 0.1f)
        {
            if (_futurParent != null)
            {
                if (transform.parent == _futurParent.transform)
                {
                    transform.SetParent(null);
                }
            }
            transform.position = Vector3.Lerp(transform.position, _endPos, Time.unscaledDeltaTime * _ballSpeed);
            transform.LookAt(_endPos, Vector3.up);
        }
        else
        {
            // Played when position = end position
            if (!_canBeActivate)
            {
                transform.position = _endPos;
                _canBeActivate = true;
                if (objectAutoAimed != null)
                {
                    _futurParent.GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<InteractionSystem>().Interact(objectAutoAimed.GetComponent<InteractionSystem>());
                }
            }
            
            if (_futurParent != null)
            {
                if (transform.parent != _futurParent.transform)
                {
                    OnCollision?.Invoke();
                    transform.SetParent(_futurParent.transform);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isLaunched && _futurParent != null)
        {
            Vector3 direction = (_launcherTransform.position - _futurParent.transform.position).normalized;
            _futurParent.GetComponent<Rigidbody>().AddForce(direction * _transportObjectSpeed, ForceMode.Acceleration);
        }
    }

    public void Move(GameObject objectToMove, GameObject destination)
    {
        Vector3 direction = (destination.transform.position - objectToMove.transform.position).normalized;

        if (objectToMove.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = objectToMove.AddComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.drag = 2;
            rb.useGravity = false;
        }
        else
        {
            Rigidbody rb = objectToMove.GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
        objectToMove.GetComponent<Rigidbody>().AddForce(direction * _transportObjectSpeed, ForceMode.Acceleration);
    }

    public void DisableInteraction()
    {
        Rigidbody rb = _futurParent.GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    public void Launch()
    {
        Debug.Log("Launch");
        isLaunched = true;
    }

    public void ThrowProjectile(Vector3 direction, float throwSpeed)
    {
        Rigidbody rb = _futurParent.GetComponent<Rigidbody>();
        rb.AddForce(direction * throwSpeed, ForceMode.Impulse);
    }
}
