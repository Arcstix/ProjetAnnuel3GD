using UnityEngine;


[CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public int DataIdentifier { get; private set; } = 0;
    [field : SerializeField] public PlayerGroundedData GroundedData { get; private set; }
    [field: SerializeField] public PlayerAbilityData AbilityData { get; private set; }
}
