using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out InteractionSystem interactionSystem))
        {
            if (interactionSystem.interactorType == InteractorType.Platform)
            {
                // On utilise le premier point de contact (tu peux affiner si besoin)
                ContactPoint contact = other.contacts[0];

                // Nouvelle orientation : on aligne transform.up avec la normale de la surface
                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, contact.normal) * transform.rotation;
    
                // Appliquer la rotation
                transform.rotation = targetRotation;
            }
        }
    }
}
