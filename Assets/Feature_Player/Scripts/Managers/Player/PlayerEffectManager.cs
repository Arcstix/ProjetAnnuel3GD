using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedEffect;
    [SerializeField] private float speedForEffect = 10f;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude > speedForEffect)
        {
            speedEffect.Play();
        }
    }
}
