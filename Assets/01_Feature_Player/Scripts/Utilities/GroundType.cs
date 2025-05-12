using UnityEngine;

public enum GroundTypeEnum
{
    None,
    Rock,
    Sable
}

public class GroundType : MonoBehaviour
{
    [SerializeField] private GroundTypeEnum groundType;

    public GroundTypeEnum GetGroundType()
    {
        return groundType;
    }
}
