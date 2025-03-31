using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(InteractiveTarget))]
public class InteractionSystem : MonoBehaviour, I_Interact
{
    public InteractorType interactorType = InteractorType.None;
    public List<InteractionSystem> detectedObjects = new List<InteractionSystem>();
    
    public bool _isInteractive = false;
    
    protected InteractiveTarget _target;
    
    protected virtual void Start()
    {
        _target = GetComponent<InteractiveTarget>();
        _target.SetInteractionType(interactorType);
    }

    public virtual void Interactable(bool isInteractable)
    {
        this._isInteractive = isInteractable;
    }
    
    private void DetectedObject(GameObject other)
    {
        InteractionSystem interaction = other.GetComponents<InteractionSystem>().FirstOrDefault(c => c.enabled);
        if (interaction != null && !detectedObjects.Contains(interaction))
        {
            detectedObjects.Add(interaction);
            Interact(interaction);
        }
    }

    private void RemoveObject(GameObject other)
    {
        InteractionSystem interaction = other.GetComponents<InteractionSystem>().FirstOrDefault(c => c.enabled);
        if (interaction != null && detectedObjects.Contains(interaction))
        {
            detectedObjects.Remove(interaction);
            ExitInteraction(interaction);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!_isInteractive) return;
        if(other.gameObject == this.gameObject) return;
        
        DetectedObject(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveObject(other.gameObject);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!_isInteractive) return;
        if (other.gameObject == this.gameObject) return;
        
        DetectedObject(other.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        RemoveObject(collision.gameObject);
    }

    public virtual void Interact(InteractionSystem otherSystem){}

    public virtual void ExitInteraction(InteractionSystem otherSystem){}
}
