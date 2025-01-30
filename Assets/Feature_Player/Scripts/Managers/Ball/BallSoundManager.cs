using System;
using UnityEngine;

public class BallSoundManager : MonoBehaviour
{
    private BallManager ballManager;

    private void Awake()
    {
        ballManager = GetComponent<BallManager>();
        ballManager.OnCollision += ProjectileCollision;
    }


    public void ProjectileCollision()
    {
        Debug.Log("Ball Collision");
        // Son qui s'active lorsque le projectile touche un objet/ingrédient/mur/sol... (quelque chose). 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/projectile collision");
    }
}
