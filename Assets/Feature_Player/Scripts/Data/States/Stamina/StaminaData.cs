using UnityEngine;

[System.Serializable]
public class StaminaData
{
    [field : SerializeField] public float MaxStamina { get; set; } = 100f;
    
    [field : SerializeField] public float MinimumStamina { get; set; } = 20f;
    [field : SerializeField] public float ConsumptionRate { get; set; } = 5f;
    [field : SerializeField] public float RecoveryRate { get; set; } = 5f;
}
