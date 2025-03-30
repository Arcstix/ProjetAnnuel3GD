using UnityEngine;

public interface I_Interact
{
    public void Interact(InteractionSystem otherSystem);
    
    public void ExitInteraction(InteractionSystem otherSystem);
}
