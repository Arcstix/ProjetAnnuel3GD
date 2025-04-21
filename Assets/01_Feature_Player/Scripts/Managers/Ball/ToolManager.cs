using System;
using System.Linq;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public float _maxSpeed = 10f;
    
    private float _ballSpeed;
    private float _transportObjectSpeed;
    private Vector3 _endPos;
    private Transform _launcherTransform;
    private GameObject _futurParent; // if the ball will be a child of an object interactable
    private bool _canBeActivate = false;
    private Vector3 refVelocity;
    private GameObject objectAutoAimed;
    private bool isLaunched = false;
    private Rigidbody _parentRb;

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
            _parentRb = _futurParent.GetComponent<Rigidbody>();
        }
        else
        {
            _futurParent = futurParent;
            _parentRb = _futurParent.GetComponent<Rigidbody>();
        }
    }
    
    public void SetNewInfo(Vector3 endPos, float speed, GameObject futurParent)
    {
        _endPos = endPos;
        _ballSpeed = speed;
        if (futurParent == null && transform.parent != null)
        {
            GetComponentInParent<InteractiveTarget>().EnableInteraction();
            transform.SetParent(null);
        }
        _futurParent = futurParent;
        if (_futurParent != null)
        {
            _parentRb = _futurParent.GetComponent<Rigidbody>();
        }
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
                    if (_parentRb)
                    {
                        _parentRb.useGravity = false;
                    }
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

            if (_parentRb.velocity.magnitude < _maxSpeed && Vector3.Distance(_launcherTransform.position, _futurParent.transform.position) > 0.5f)
            {
                _parentRb.AddForce(direction * _transportObjectSpeed, ForceMode.Acceleration);
            }
        }
    }

    public void Move(GameObject objectToMove, GameObject destination)
    {
        Vector3 direction = (destination.transform.position - objectToMove.transform.position).normalized;
        
        Rigidbody rb = objectToMove.GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            rb = objectToMove.AddComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.drag = 2;
            rb.useGravity = false;
        }
        else
        {
            rb = objectToMove.GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        float distance = Vector3.Distance(destination.transform.position, _futurParent.transform.position);
        
        if (rb.velocity.magnitude < _maxSpeed && distance > 0.5f)
        {
            objectToMove.GetComponent<Rigidbody>().AddForce(direction * _transportObjectSpeed * distance, ForceMode.Acceleration);
        }
    }

    public void DisableInteraction()
    {
        if (_futurParent != null)
        {
            Rigidbody rb = _futurParent.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.useGravity = true;
            }
            InteractionSystem interaction = _futurParent.GetComponents<InteractionSystem>().FirstOrDefault(c => c.enabled);
            if (interaction != null)
            {
                interaction.ExitInteraction(GetComponent<InteractionSystem>());
            }
        }
    }

    public void SetLaunch(bool isActive)
    {
        Debug.Log("Launch");
        isLaunched = isActive;
    }

    public void ThrowProjectile(Vector3 direction, float throwSpeed)
    {
        _parentRb.AddForce(direction * throwSpeed, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        if (transform.parent != null)
        {
            InteractiveTarget interactiveTarget = GetComponentInParent<InteractiveTarget>();
            if (interactiveTarget)
            {
                interactiveTarget.EnableInteraction();
            }
        }
    }
}
