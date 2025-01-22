using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchState : MonoBehaviour
{
    public abstract void Enter(GameObject gameObject);
    public abstract void Tick(GameObject gameObject);
    public abstract void Exit(GameObject gameObject);
}
