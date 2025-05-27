using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DraftPushObject : MonoBehaviour
{
    private float maxDist;
    private BoxCollider boxCollider;
    
    public AnimationCurve propulsionCurve;
    public float propulsionForce = 5f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        maxDist = boxCollider.size.y;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Projectile"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
           
            float distanceToBase = Vector3.Distance(transform.position, other.transform.position);
           
            rb.AddForce(transform.up * propulsionCurve.Evaluate(distanceToBase / maxDist) * propulsionForce);
        }
    }
}
