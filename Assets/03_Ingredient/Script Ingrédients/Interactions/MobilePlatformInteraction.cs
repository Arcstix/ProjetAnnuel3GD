using System;
using UnityEngine;

public class MobilePlatformInteraction : InteractionSystem
{
    private enum MobileType
    {
        None,
        Translation,
        Rotation,
    }
    
    [SerializeField] private GameObject mobilePlatform;
    [SerializeField] private Rigidbody PhysicPlatform;
    [SerializeField] private MobileType mobileType = MobileType.None;
    
    [Header("Target Position at the end of interaction")]
    [SerializeField] private Vector3 targetPosition;
    
    [Header("Target rotation at the end of interaction")]
    [SerializeField] private Vector3 targetRotation;
    
    
    public void MovePlatform()
    {
        switch (mobileType)
        {
            case MobileType.None:
                Debug.Log(gameObject.name + " : MobilePlatform doesn't support translation or rotation");
                break;
            case MobileType.Translation:
                DoTranslation();
                break;
            case MobileType.Rotation:
                DoRotation();
                break;
            default:
                Debug.Log(gameObject.name + " : MobilePlatform doesn't support translation or rotation");
                break;
        }
    }

    private void DoRotation()
    {
        if (mobilePlatform.transform.rotation != Quaternion.Euler(targetRotation))
        {
            if (PhysicPlatform)
            {
                PhysicPlatform.isKinematic = false;
            }
            
            mobilePlatform.transform.rotation = Quaternion.Lerp(mobilePlatform.transform.rotation,
                Quaternion.Euler(targetRotation), Time.deltaTime * 2f);
        }
    }

    private void DoTranslation()
    {
        if (mobilePlatform.transform.localPosition != targetPosition)
        {
            if (PhysicPlatform)
            {
                PhysicPlatform.isKinematic = false;
            }
            
            mobilePlatform.transform.localPosition = Vector3.Lerp(mobilePlatform.transform.localPosition, targetPosition, Time.deltaTime * 2f);
        }
    }
}
