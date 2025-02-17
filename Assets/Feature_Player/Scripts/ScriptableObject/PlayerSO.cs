using UnityEngine;


[CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] [field: Range(0f,2f)] public float Drag { get; private set; } = 1f;
    [field : SerializeField] public GroundedData GroundedData { get; private set; }
    [field : SerializeField] public FallingData FallingData { get; private set; }
    [field : SerializeField] public AbilityData AbilityData { get; private set; }
    [field : SerializeField] public StaminaData StaminaData { get; private set; }
    [field: SerializeField] public PlayerCameraData CameraData { get; private set; }
}
