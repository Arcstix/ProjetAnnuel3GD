using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHandleFilled : MonoBehaviour
{
    public PlayerMetricsManager metricsManager;
    public Image filledImage;
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
    }
}
