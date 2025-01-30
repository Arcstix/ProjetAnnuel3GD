using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private float _ballSpeed;
    private float _transportObjectSpeed;
    private Vector3 _endPos;
    private GameObject _futurParent; // if the ball will be a child of an object interactable
    private bool _canBeActivate = false;
    private Vector3 refVelocity;

    public event Action OnCollision;
    
    public void InitializeBall(float ballSpeed, float transportObjectSpeed, Vector3 endPos, GameObject futurParent)
    {
        _ballSpeed = ballSpeed;
        _transportObjectSpeed = transportObjectSpeed;
        _endPos = endPos;
        _futurParent = futurParent;
        _canBeActivate = false;
    }
    
    public void SetNewInfo(Vector3 endPos, float speed, GameObject futurParent)
    {
        _endPos = endPos;
        _ballSpeed = speed;
        if (futurParent == null)
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
        }
        else
        {
            // Played when position = end position
            if (!_canBeActivate)
            {
                transform.position = _endPos;
                _canBeActivate = true;
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
        objectToMove.GetComponent<Rigidbody>().AddForce(direction * _transportObjectSpeed, ForceMode.Acceleration);
    }
}
