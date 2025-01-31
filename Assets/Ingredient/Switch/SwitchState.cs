using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchState : MonoBehaviour
{
    public abstract void Enter(GameObject refObject);
    public abstract void Tick(GameObject refObject);
    public abstract void Exit(GameObject refObject);
}
