using UnityEngine;

[System.Serializable]
public class AbilityData
{
    [field: SerializeField] public float TimerBeforeAim { get; set; } = 0.5f;
    
    [field : SerializeField] public float AimDistance { get; set; } = 5f;
    
    [field : SerializeField] public Vector3 AimBallOffset { get; set; } = new Vector3(0f, 0f, 0.2f);
    
    [field : SerializeField] [field : Range(10f, 100f)] public float AimSpeed { get; set; } = 50f;
    [field : SerializeField] [field : Range(10f, 100f)] public float ShootSpeed { get; set; } = 50f;
    [field : SerializeField] [field : Range(10f, 100f)] public float RecallSpeed { get; set; } = 50f;
    
    [field : SerializeField] public GameObject AimBall { get; private set; }
    
    [field : SerializeField] public GameObject RightBall { get; private set; }
    
    [field : SerializeField] public GameObject LeftBall { get; private set; }
}
