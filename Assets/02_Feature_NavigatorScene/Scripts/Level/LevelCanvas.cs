using System;
using TMPro;
using UnityEngine;

public class LevelCanvas : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private TextMeshProUGUI levelDuration;
    [SerializeField] private TextMeshProUGUI levelDeath;

    private void Start()
    {
        if (levelManager)
        {
            levelManager.OnTimeUpdate += UpdateTimeDisplay;
            levelManager.OnDeathUpdate += UpdateDeathDisplay;
        }
    }

    private void UpdateDeathDisplay(int value)
    {
        levelDeath.text = "X " + value.ToString("D3");
    }

    private void UpdateTimeDisplay(float value)
    {
        string timeString = $"{(int)(value / 60):00}:{(int)(value % 60):00}:{(int)((value * 1000) % 1000):000}";
        
        levelDuration.text = timeString;
    }
}
