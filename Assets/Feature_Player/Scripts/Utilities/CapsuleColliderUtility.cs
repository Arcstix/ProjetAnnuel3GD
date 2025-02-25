using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CapsuleColliderUtility 
{
    [field: SerializeField] public CapsuleColliderData CapsuleColliderData { get; private set; }

    [field: SerializeField] public DefaultColliderData DefaultColliderData { get; private set; }

    [field: SerializeField] public SlopeData SlopeData { get; private set; }

    [field: SerializeField] public LayerData LayerData { get; private set; }

    public void Initialize(GameObject gameObject)
    {
        if(CapsuleColliderData != null)
        {
            return;
        }

        CapsuleColliderData = new CapsuleColliderData();

        CapsuleColliderData.Initialize(gameObject);
    }

    public void CalculateCapsuleColliderDimension()
    {
        SetCapsuleColliderRadius(DefaultColliderData.Radius);

        SetCapsuleColliderHeight(DefaultColliderData.Height * (1f - SlopeData.StepHeightPercentage));

        ReCalculateCapsuleColliderCenter();

        CapsuleColliderData.UpdateColliderData();
    }

    public void ReCalculateCapsuleColliderCenter()
    {
        float colliderHeightDifference = DefaultColliderData.Height - CapsuleColliderData.Collider.height;

        Vector3 newColliderCenter = new Vector3(0f, DefaultColliderData.CenterY + (colliderHeightDifference / 2f), 0f);

        CapsuleColliderData.Collider.center = newColliderCenter;
    }

    public void SetCapsuleColliderRadius(float radius)
    {
        CapsuleColliderData.Collider.radius = radius;
    }

    public void SetCapsuleColliderHeight(float height)
    {
        CapsuleColliderData.Collider.height = height;
    }
}
