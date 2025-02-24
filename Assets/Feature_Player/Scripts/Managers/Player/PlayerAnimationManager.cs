using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private static readonly int AimL = Animator.StringToHash("Aim_L");
    private static readonly int AimR = Animator.StringToHash("Aim_R");
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int RecallL = Animator.StringToHash("Recall_L");
    private static readonly int RecallR = Animator.StringToHash("Recall_R");
    private static readonly int ActivateR = Animator.StringToHash("ActivateR");
    private static readonly int ActivateL = Animator.StringToHash("Activate_L");
    [SerializeField] private Animator animator;
    
    private PlayerAbilityManager abilityManager;

    private void TriggerAimL()
    {
        animator.SetTrigger(AimL);
    }

    private void TriggerAimR()
    {
        animator.SetTrigger(AimR);
    }

    private void TriggerShoot()
    {
        animator.SetTrigger(Shoot);
    }

    private void TriggerRecallL()
    {
        animator.SetTrigger(RecallL);
    }

    private void TriggerRecallR()
    {
        animator.SetTrigger(RecallR);
    }

    private void TriggerActivateR()
    {
        animator.SetTrigger(ActivateR);
    }

    private void TriggerActivateL()
    {
        animator.SetTrigger(ActivateL);
    }
}
