using System;
using TMPro;
using UnityEngine;

public class EndLevelCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelDuration;
    [SerializeField] private TextMeshProUGUI levelDeath;
    
    private void Start()
    {
        LevelManager.Instance.OnLevelFinished += DisplayInfoLevel;
    }

    private void DisplayInfoLevel()
    {
        levelDeath.text = LevelManager.Instance.GetNumberOfDeath().ToString();
        levelDuration.text = LevelManager.Instance.GetLevelDuration();
    }
}
