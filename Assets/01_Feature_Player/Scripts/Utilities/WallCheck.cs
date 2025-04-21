using System;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] private float _timeForWallRun = 2f;
    [SerializeField] private bool _canWallRun = false;
    
    public void SetCanWallRun(bool canWallRun)
    {
        _canWallRun = canWallRun;
        if (_canWallRun)
        {
            Invoke(nameof(ResetCanWallRun), _timeForWallRun);
        }
    }

    protected void ResetCanWallRun()
    {
        _canWallRun = false;
    }

    public bool CanWallRun()
    {
        return _canWallRun;
    }
}
