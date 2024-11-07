using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetricsManager : MonoBehaviour
{
    [SerializeField] private PlayerSO defaultPlayerSO;

    [SerializeField] private List<PlayerSO> playerSOs = new List<PlayerSO>();

    public PlayerSO CurrentPlayerSO { get; private set; }
    public int CurrentAbilityID { get; private set; }

    private Dictionary<int, float> abilityDictionary = new Dictionary<int, float>();

    public void Init()
    {
        InitializeDictionary();
        CurrentPlayerSO = defaultPlayerSO;
        CurrentAbilityID = 0;
    }

    private void InitializeDictionary()
    {
        foreach (var playerSO in playerSOs)
        {
            abilityDictionary.Add(playerSO.DataIdentifier, 0);
        }
    }

    public void AddDurability(int abilityID, float durability)
    {
        abilityDictionary[abilityID] += durability;
        Debug.Log("Durabiity added");
    }

    public void ChangePlayerSO(PlayerSO newPlayerSO)
    {
        CurrentPlayerSO = newPlayerSO;
    }

    public void ChangePlayerSO(int nextPlayerSO)
    {
        CurrentAbilityID = nextPlayerSO;
    }
}
