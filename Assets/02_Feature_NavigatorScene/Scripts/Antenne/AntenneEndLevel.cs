using System;
using System.Collections.Generic;
using UnityEngine;

public class AntenneEndLevel : MonoBehaviour
{
    [SerializeField] private AntenneInteraction _leftAntenne;
    [SerializeField] private AntenneInteraction _rightAntenne;
    
    private bool _isLeftAntenneActive;
    private bool _isRightAntenneActive;
    
    private void Start()
    {
        _leftAntenne.OnToolInteraction += LeftActivation;
        _rightAntenne.OnToolInteraction += RightActivation;
    }

    private void LeftActivation(bool isActive)
    {
        _isLeftAntenneActive = isActive;
        CheckEndLevel();
    }

    private void RightActivation(bool isActive)
    {
        _isRightAntenneActive = isActive;
        CheckEndLevel();
    }
    
    private void CheckEndLevel()
    {
        if (_isLeftAntenneActive && _isRightAntenneActive)
        {
            GameManagerSM.Instance.ChangeState(GameManagerSM.Instance.GetComponent<EndLevelState>());
        }
    }
}
