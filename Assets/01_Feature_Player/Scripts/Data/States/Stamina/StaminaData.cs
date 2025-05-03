using UnityEngine;

[System.Serializable]
public class StaminaData
{
    [field : SerializeField] public int MaxStamina { get; set; } = 100;
    
    [field : SerializeField] public int MaxNumberOfChargePerObject { get; set; } = 1;
    
    [field : SerializeField] public int MinimumStamina { get; set; } = 20;
    [field : SerializeField] public int ConsumptionRate { get; set; } = 5;
    [field : SerializeField] public int RecoveryRate { get; set; } = 5;
}
