using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();

    public void Exit();

    /// <summary>
    /// Method usefull for input that need to be read every frame
    /// </summary>
    public void HandleInput();

    public void Tick();

    public void FixedTick();
}
