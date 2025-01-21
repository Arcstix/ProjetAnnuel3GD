using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[System.Serializable]
public class PlayerCameraData
{
    [field: SerializeField] public float BaseFOV = 60f;
    
    [field : Header("Plus la valeur est basse plus la transition sera lent.")]
    [field: SerializeField] public float SmoothingFactorBaseFOV = 0.05f;
    [field : Header("Il faut mettre une valeur entre la base FOV et le max voulu.")]
    [field: SerializeField] public AnimationCurve TransitionBaseTransportFOV;
    // [field: SerializeField] [Range(0f, 10f)] public float DefaultDistance = 6f;
    // [field: SerializeField] [Range(0f, 10f)] public float MinimumDistance = 1f;
    // [field: SerializeField] [Range(0f, 10f)] public float MaximumDistance = 6f;
    //
    // [field: SerializeField] [Range(0f, 10f)] public float ZoomSmoothing = 4f;
    // [field: SerializeField] [Range(0f, 10f)] public float ZoomSensitivity = 1f;

    [field: SerializeField] [Range(0f, 10f)] public float ControllerVerticalSpeed = 4f;
    [field: SerializeField] [Range(0f, 10f)] public float ControllerHorizontalSpeed = 4f;
}
