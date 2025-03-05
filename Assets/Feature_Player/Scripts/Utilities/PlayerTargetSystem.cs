using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTargetSystem : MonoBehaviour, I_Initializer
{
    private PlayerAbilityManager abilityManager;
    private PlayerReusableStateData reusableData;
    private bool isActivated = true;
    
    public Camera playerCamera;
    
    public List<InteractiveTarget> targets;
    public List<InteractiveTarget> reachableTargets;

    [Header("Target")]
    public InteractiveTarget currentTarget;
    public InteractiveTarget storedTarget;

    [Header("Parameters")]
    //Weight values that determine what distance (screen/player) gets prioritized
    [SerializeField] float screenDistanceWeight = 1;
    [SerializeField] float positionDistanceWeight = 8;
    //Min Distance for targets
    public float minReachDistance = 70;
    
    [Header("User Interface")]
    [SerializeField] RectTransform rectImage;
    [SerializeField] float rectSizeMultiplier = 2;
    [SerializeField] float rotateAngle = 90;
    [SerializeField] float startScale = 4;
    [SerializeField] float animationDuration = 0.25f;

    private void Awake()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
    }

    private void Start()
    {
        isActivated = true;
    }

    public void Init(PlayerReusableStateData reusableStateData)
    {
        reusableData = reusableStateData;
    }

    #region Events

    private void OnEnable()
    {
        abilityManager.OnAbilityStarted += SubscribeEvent;
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
        abilityManager.OnAbilityStarted -= SubscribeEvent;
    }

    private void SubscribeEvent()
    {
        abilityManager.AbilityStateMachine.AimState.OnEnterAim += DisableTargetSystem;
        abilityManager.AbilityStateMachine.AimState.OnExitAim += EnableTargetSystem;
    }
    
    private void UnsubscribeEvent()
    {
        abilityManager.AbilityStateMachine.AimState.OnEnterAim -= DisableTargetSystem;
        abilityManager.AbilityStateMachine.AimState.OnExitAim -= EnableTargetSystem;
    }

    #endregion

    private void EnableTargetSystem()
    {
        isActivated = true;
    }

    private void DisableTargetSystem()
    {
        isActivated = false;
    }

    void Update()
    {
        if (reachableTargets.Count < 1 || !isActivated)
        {
            storedTarget = null;
            
            if (reusableData != null)
                reusableData.ObjectAutoAimed = null;
            
            rectImage.gameObject.SetActive(false);
            return;
        }
        
        currentTarget = reachableTargets[TargetIndex()];
        
        CheckTargetChange();
        
        //User Interface
        rectImage.gameObject.SetActive(true);
        rectImage.transform.position = ClampedScreenPosition(currentTarget.transform.position);
        float distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        rectImage.sizeDelta = new Vector2(Mathf.Clamp(115 - (distanceFromTarget - rectSizeMultiplier),100,150), Mathf.Clamp(115 - (distanceFromTarget - rectSizeMultiplier),100,150));
    }

    Vector3 ClampedScreenPosition(Vector3 targetPos)
    {
        Vector3 WorldToScreenPos = Camera.main.WorldToScreenPoint(targetPos);
        Vector3 clampedPosition = new Vector3(Mathf.Clamp(WorldToScreenPos.x, 0, Screen.width), Mathf.Clamp(WorldToScreenPos.y, 0, Screen.height), WorldToScreenPos.z);
        return clampedPosition;
    }


    private int TargetIndex()
    {
        //Creates an array where the distances between the target and the screen/player will be stored
        float[] distances = new float[reachableTargets.Count];

        //Populates the distances array with the sum of the Target distance from the screen center and the Target distance from the player
        for (int i = 0; i < reachableTargets.Count; i++)
        {
            distances[i] =
                (Vector2.Distance(playerCamera.WorldToScreenPoint(reachableTargets[i].transform.position), MiddleOfScreen()) * screenDistanceWeight)
                +
                (Vector3.Distance(transform.position, reachableTargets[i].transform.position) * positionDistanceWeight);
        }

        //Finds the smallest of the distances
        float minDistance = Mathf.Min(distances);

        int index = 0;

        //Find the index number relative to the target with the smallest distance
        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }

        return index;
    }

    public void ClearCurrentTarget()
    {
        currentTarget = null;
    }

    void CheckTargetChange()
    {
        if (storedTarget != currentTarget)
        {
            storedTarget = currentTarget;
            
            if (reusableData != null)
                reusableData.ObjectAutoAimed = storedTarget.gameObject;
            
            rectImage.DOComplete();
            rectImage.DORotate(new Vector3(0,0,rotateAngle), animationDuration).From();
            rectImage.DOScale(startScale, animationDuration).From();
        }
    }

    Vector2 MiddleOfScreen()
    {
        return new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
