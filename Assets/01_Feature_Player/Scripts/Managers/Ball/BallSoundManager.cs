using System;
using UnityEngine;

public class BallSoundManager : MonoBehaviour
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
}
