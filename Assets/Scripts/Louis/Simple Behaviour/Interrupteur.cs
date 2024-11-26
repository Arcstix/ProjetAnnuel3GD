using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interrupteur : MonoBehaviour
{
    [Header("Il s'active avec un objet layer Movable")]

    public bool isCollide = false;

    public UnityEvent eventEnterCollision;
    public UnityEvent eventExitCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isCollide)
        {
            if (collision.collider.CompareTag("Movable"))
            {
                isCollide = true;
                eventEnterCollision?.Invoke();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isCollide)
        {
            if (collision.collider.CompareTag("Movable"))
            {
                isCollide = false;
                eventExitCollision?.Invoke();
            }
        }
    }
}
