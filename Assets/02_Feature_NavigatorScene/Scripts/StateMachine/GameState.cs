using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManagerSM gameManager;
    
    
    //Called on FSM Start
    public virtual void Initialize(GameManagerSM sm)
    {
        this.gameManager = sm;
    }

    public virtual void Enter(){}
    public virtual void Tick(){}
    public virtual void Exit(){}
}