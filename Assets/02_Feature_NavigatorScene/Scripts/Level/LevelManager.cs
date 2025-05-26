using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int numberOfDeath;
    private float levelDuration;
    private bool isStarted = false;

    public event Action<int> OnDeathUpdate;
    public event Action<float> OnTimeUpdate;
    public event Action OnLevelFinished;
    
    public static LevelManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Évite les doublons
            return;
        }

        Instance = this;
    }
    
    public void StartLevel()
    {
        numberOfDeath = 0;
        levelDuration = 0;
        OnTimeUpdate?.Invoke(levelDuration);
        isStarted = true;
    }

    public void EndLevel()
    {
        isStarted = false;
        OnLevelFinished?.Invoke();
    }

    public bool GetStartedState()
    {
        return isStarted;
    }

    public void IncrementDeath()
    {
        numberOfDeath++;
        OnDeathUpdate?.Invoke(numberOfDeath);
    }

    public int GetNumberOfDeath()
    {
        return numberOfDeath;
    }

    public string GetLevelDuration()
    {
        return $"{(int)(levelDuration / 60):00}:{(int)(levelDuration % 60):00}:{(int)((levelDuration * 1000) % 1000):000}";;
    }

    private void Update()
    {
        if (isStarted)
        {
            levelDuration += Time.deltaTime;
            OnTimeUpdate?.Invoke(levelDuration);
        }
    }
}
