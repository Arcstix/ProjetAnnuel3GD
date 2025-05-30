using System;
using UnityEngine;

public class UIInteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject shootInteraction;
    [SerializeField] private GameObject recallInteraction;
    [SerializeField] private GameObject dashInteraction;
    [SerializeField] private GameObject attractionInteraction;
    [SerializeField] private GameObject throwInteraction;

    [SerializeField] private bool isRight = false;

    [SerializeField] private PlayerAbilityManager abilityManager;

    private PlayerReusableStateData reusableData;
    private GameObject currentLeftObject;
    //private InteractorType currentLeftInteractor;
    
    private GameObject currentRightObject;
    //private InteractorType currentRightInteractor;

    private void Awake()
    {
        abilityManager.OnAbilityStarted += GetReusableData;
    }

    private void GetReusableData()
    {
        reusableData = abilityManager.ReusableData;
    }

    private void Update()
    {
        if (reusableData == null) return;

        HandleShootRecall();
        
        if (isRight)
        {
            HandleRightManipulation();
        }
        else
        {
            HandleLeftManipulation();
        }
    }

    private void HandleLeftManipulation()
    {
        if (reusableData.LeftObject == null)
        {
            HideEverything();
            return;
        }

        if (currentLeftObject != reusableData.LeftObject.gameObject)
        {
            currentLeftObject = reusableData.LeftObject.gameObject;
            
            if (reusableData.LeftParent != null)
            {
                InteractorType leftParentType = reusableData.LeftParent.GetComponent<InteractiveTarget>().GetCurrentType();
                if (leftParentType is InteractorType.PulseProjectile or InteractorType.MobilePlatform)
                {
                    DisplayAttraction();
                    return;
                }
                else if (reusableData.LeftParent.GetComponent<InteractiveTarget>().GetCurrentType() ==
                         InteractorType.BrightProjectile)
                {
                    DisplayThrow();
                    return;
                }
                else
                {
                    if (reusableData.RightObject == null)
                    {
                        DisplayDash();
                        return;
                    }
                }
            }

            if (reusableData.RightObject == null)
            {
                DisplayDash();
            }
        }
    }

    private void HandleRightManipulation()
    {
        if (reusableData.RightObject == null)
        {
            HideEverything();
            return;
        }

        if (currentRightObject != reusableData.RightObject.gameObject)
        {
            currentRightObject = reusableData.RightObject.gameObject;
            
            if (reusableData.RightParent != null)
            {
                InteractorType rightParentType = reusableData.RightParent.GetComponent<InteractiveTarget>().GetCurrentType();
                if (rightParentType is InteractorType.PulseProjectile or InteractorType.MobilePlatform)
                {
                    DisplayAttraction();
                    return;
                }
                else if (reusableData.RightParent.GetComponent<InteractiveTarget>().GetCurrentType() ==
                         InteractorType.BrightProjectile)
                {
                    DisplayThrow();
                    return;
                }
                else
                {
                    if (reusableData.LeftObject == null)
                    {
                        DisplayDash();
                        return;
                    }
                }
            }

            if (reusableData.LeftObject == null)
            {
                DisplayDash();
            }
        }
    }

    private void DisplayDash()
    {
        dashInteraction.SetActive(true);
        attractionInteraction.SetActive(false);
        throwInteraction.SetActive(false);
    }

    private void DisplayThrow()
    {
        dashInteraction.SetActive(false);
        attractionInteraction.SetActive(false);
        throwInteraction.SetActive(true);
    }

    private void DisplayAttraction()
    {
        dashInteraction.SetActive(false);
        attractionInteraction.SetActive(true);
        throwInteraction.SetActive(false);
    }

    private void HideEverything()
    {
        dashInteraction.SetActive(false);
        attractionInteraction.SetActive(false);
        throwInteraction.SetActive(false);
    }

    private void HandleShootRecall()
    {
        if (isRight)
        {
            DisplayShootRecall(reusableData.RightObject == null);
        }
        else
        {
            DisplayShootRecall(reusableData.LeftObject == null);
        }
    }

    private void DisplayShootRecall(bool canShoot)
    {
        shootInteraction.SetActive(canShoot);
        recallInteraction.SetActive(!canShoot);
    }
}
