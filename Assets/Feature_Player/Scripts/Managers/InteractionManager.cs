using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public enum InteractionType
    {
        None,
        Player,
        Ball,
        Destructible
    }
    
    [SerializeField] private InteractionType interactionType = InteractionType.None;

    public InteractionType Interaction { get => interactionType; set => interactionType = value; }

    private bool isActive = false;

    public void Activate(bool active)
    {
        isActive = active;
    }

    public bool IsActivate()
    {
        return isActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.TryGetComponent(out InteractionManager manager))
            {
                switch (interactionType)
                {
                    case InteractionType.Player:
                        if (!manager.IsActivate())
                        {
                            switch (manager.Interaction)
                            {
                                case InteractionType.Ball:
                                    // la balle se détruit
                                    Destroy(manager.gameObject);
                                    break;
                                case InteractionType.Destructible:
                                    // Il ne se passe rien pour l'instant
                                    break;
                            }
                        }
                        break;
                    case InteractionType.Ball:
                        if (!manager.IsActivate())
                        {
                            switch (manager.Interaction)
                            {
                                case InteractionType.Player:
                                    // la balle se détruit
                                    Destroy(gameObject);
                                    break;
                                case InteractionType.Ball:
                                    // l'autre balle est détruite
                                    Destroy(manager.gameObject);
                                    // la balle est détruite
                                    Destroy(gameObject);
                                    break;
                                case InteractionType.Destructible:
                                    // la balle se détruit
                                    Destroy(gameObject);
                                    break;
                            }
                        }
                        else
                        {
                            switch (manager.Interaction)
                            {
                                case InteractionType.Ball:
                                    // la balle est détruite
                                    Destroy(gameObject);
                                break;
                            }
                        }
                        break;
                    case InteractionType.Destructible:
                        if (!manager.IsActivate())
                        {
                            switch (manager.Interaction)
                            {
                                case InteractionType.Player:
                                    // Objet détruit
                                    Destroy(gameObject);
                                    break;
                                case InteractionType.Ball:
                                    // la balle est détruite
                                    Destroy(manager.gameObject);
                                    break;
                                case InteractionType.Destructible:
                                    // Autre Objet détruit
                                    Destroy(other.gameObject);
                                    // Objet détruit
                                    Destroy(gameObject);
                                    break;
                            }
                        }
                        else
                        {
                            switch (manager.Interaction)
                            {
                                case InteractionType.Player:
                                    // Objet détruit
                                    Destroy(gameObject);
                                    break;
                                case InteractionType.Ball:
                                    // la balle est détruite
                                    Destroy(manager.gameObject);
                                    break;
                                case InteractionType.Destructible:
                                    // Objet détruit
                                    Destroy(gameObject);
                                    break;
                            }
                        }
                        break;
                } 
            }
        }
    }

    
}
