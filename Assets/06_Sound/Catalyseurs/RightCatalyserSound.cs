using System;
using UnityEngine;

public class RightCatalyserSound : MonoBehaviour
{
    private ToolManager toolManager;

    private void Awake()
    {
        toolManager = GetComponent<ToolManager>();
        toolManager.OnCollision += ProjectileCollision;
    }


    public void ProjectileCollision()
    {
        // Son qui s'active lorsque le projectile touche un objet/ingrédient/mur/sol... (quelque chose). 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/projectile collision");
    }

    public void ProjectileIdleDroit()
    {
        // Son qui s'active lorsque le projectile droit n'est pas sur nous 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Idle Balle droit");
    }

    public void ProjectileActivationDroit()
    {
        // Son qui s'active lorsque le projectile droit est activé (pour nous transporter vers lui)
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/activation droit");
    }
}
