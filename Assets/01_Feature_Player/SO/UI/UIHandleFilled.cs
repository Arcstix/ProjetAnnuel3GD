using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHandleFilled : MonoBehaviour
{
    private static readonly int IsAvailable = Animator.StringToHash("isAvailable");
    
    public PlayerMetricsManager metricsManager;
    public Image filledImage;
    public Animator pastilleAnimator;
    public ParticleSystem pastilleParticles;
    public bool isRight;

    private void OnEnable()
    {
        if (isRight)
        {
            if (metricsManager.useCharge)
            {
                metricsManager.OnRightChargeSet += HandleFilled;
            }
            else
            {
                metricsManager.OnRightStaminaSet += HandleFilled;
            }
        }
        else
        {
            if (metricsManager.useCharge)
            {
                metricsManager.OnLeftChargeSet += HandleFilled;
            }
            else
            {
                metricsManager.OnLeftStaminaSet += HandleFilled;
            }
        }
    }

    private void OnDisable()
    {
        if (isRight)
        {
            if (metricsManager.useCharge)
            {
                metricsManager.OnRightChargeSet -= HandleFilled;
            }
            else
            {
                metricsManager.OnRightStaminaSet -= HandleFilled;
            }
        }
        else
        {
            if (metricsManager.useCharge)
            {
                metricsManager.OnLeftChargeSet -= HandleFilled;
            }
            else
            {
                metricsManager.OnLeftStaminaSet -= HandleFilled;
            }
        }
    }

    private void HandleFilled(float fillAmount, float maxFillAmount)
    {
        filledImage.fillAmount = fillAmount/maxFillAmount;

        if (Mathf.Approximately(fillAmount, maxFillAmount))
        {
            pastilleAnimator.SetBool(IsAvailable, true);
            if (!pastilleParticles.isPlaying)
            {
                pastilleParticles.Play();
            }
        }
        else
        {
            pastilleAnimator.SetBool(IsAvailable, false);
            if (!pastilleParticles.isStopped)
            {
                pastilleParticles.Stop();
            }
        }
    }
}
