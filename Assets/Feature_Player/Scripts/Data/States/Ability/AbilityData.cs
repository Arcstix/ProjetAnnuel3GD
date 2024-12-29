using UnityEngine;

[System.Serializable]
public class AbilityData
{
    [field : Header("Aim Data"), Space(1)]
    [field : SerializeField] public float AimDistance { get; set; } = 5f;
    [field : SerializeField] public Vector3 AimBallOffset { get; set; } = new Vector3(0f, 0f, 0.2f);
    [field : SerializeField] public GameObject AimBall { get; private set; }
    [field : SerializeField] public float AimSpeed { get; set; } = 50f;
    
    [field : Header ("Shoot Data"), Space(1)]
    [field : SerializeField] public float ShootSpeed { get; set; } = 50f;
    [field : SerializeField] public BallManager RightBall { get; private set; }
    [field : SerializeField] public BallManager LeftBall { get; private set; }
    
    [field : Header("Recall Data"), Space(1)]
    [field : SerializeField] [field : Range(10f, 100f)] public float RecallSpeed { get; set; } = 50f;
    
    [field : Header("Transport Data"), Space(1)]
    [field: SerializeField] public float TransportSpeed { get; set; } = 10f;
    [field : SerializeField] public AnimationCurve TransportCurve { get; private set; }
}
