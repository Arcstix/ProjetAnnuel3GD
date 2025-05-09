using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BoosterAnimation : MonoBehaviour
{
    [SerializeField] private Animator boosterAnimator;
    [SerializeField] private float speedTransition = 1f;
    [SerializeField] private float defaultBoosterSpeed = 0.3f;
    [SerializeField] private float detectionBoosterSpeed = 3f;

    private float targetSpeed;
    private float currentSpeed;

    private void Start()
    {
        InitBoosterSpeed();
    }

    private void Update()
    {
        if (targetSpeed < currentSpeed || targetSpeed > currentSpeed)
        {
            boosterAnimator.speed = Mathf.Lerp(currentSpeed, targetSpeed, speedTransition * Time.deltaTime);
            currentSpeed = boosterAnimator.speed;
        }
    }

    private void InitBoosterSpeed()
    {
        currentSpeed = defaultBoosterSpeed;
        targetSpeed = defaultBoosterSpeed;
        boosterAnimator.speed = defaultBoosterSpeed;
    }

    public void OnBoosterDetection()
    {
        targetSpeed = detectionBoosterSpeed;
    }

    public void OnBoosterRelease()
    {
        targetSpeed = defaultBoosterSpeed;
    }
}
