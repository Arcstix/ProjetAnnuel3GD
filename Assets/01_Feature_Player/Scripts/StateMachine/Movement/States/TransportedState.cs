using UnityEngine;

public class TransportedState : MovementState
{
    public TransportedState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Tick()
    {
        base.Tick();

        if (!reusableData.OnTransportation)
        {
            if (reusableData.InAir)
            {
                stateMachine.ChangeState(stateMachine.FallingState);
            }
            else if (reusableData.OnLandingPlatform)
            {
                stateMachine.ChangeState(stateMachine.OnPlatformState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        RotateInMoveDirection(rigidbody, rigidbody.velocity);
    }

    void RotateInMoveDirection(Rigidbody rb, Vector3 velocity)
    {
        // Ignore la composante verticale
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);

        // Si le mouvement est significatif
        if (horizontalVelocity.sqrMagnitude > 0.01f)
        {
            // Calcule la rotation vers la direction du mouvement
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);

            // Applique uniquement la rotation sur l'axe Y
            Quaternion yRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

            // Effectue la rotation via MoveRotation
            rb.MoveRotation(yRotation);
        }
    }
}
