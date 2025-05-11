//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BallGaucheSoundManager : MonoBehaviour
//{
//    public void ProjectileCollision()
//    {
//        // !!!! A VOIR !!!!! Son qui s'active lorsque le projectile touche un objet/ingrédient/mur/sol... (quelque chose). 
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/projectile collision");
//    }

//    public void ProjectileIdleGauche()
//    {
//        // Son qui s'active lorsque le projectile gauche n'est pas sur nous 
//        [FMODUnity.EventRef]
//        public string selectsound;

//        public KeyCode presstoplaysound;

//        void Start ()
//        {
//            soundevent = FMODUnity.RuntimeManager.CreateInstance(selectsound);
//        }

//        void Update ()
//        {
//            FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundevent, GetComponent<Transform>(), GetComponent<Rigidbody>());
//            Playsound();
//        }

//        void Playsound()
//        { 
//            if (Input.GetKeyDown(pressplaysound)) // la condition ici ce serait if la balle est pas sur le joueur (peut etre utiliser un bool)
//            {
//                FMOD.Studio.PLAYBACK_STATE fmodPbState;
//                soundevent.getPlaybackState(out fmodPbState);
//                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
//                {
//                    soundevent.start();
//                }
//            }
//            if (Input.GetKeyUp(presstoplaysound)) // Ici la condiiton est si la balle reviens sur le joueur
//            {
//            soundevent.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//            }
//        }
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Idle Balle gauche");
//    }

//    public void ProjectileActivationGauche()
//    {
//    // Son qui s'active lorsque le projectile gauche est activé (pour nous transporter vers lui) 

//        [FMODUnity.EventRef]
//        public string selectsound;

//        public KeyCode presstoplaysound;

//    void Start()
//    {
//        soundevent = FMODUnity.RuntimeManager.CreateInstance(selectsound);
//    }

//    void Update()
//    {
//        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundevent, GetComponent<Transform>(), GetComponent<Rigidbody>());
//        Playsound();
//    }

//    void Playsound()
//    {
//        if (Input.GetKeyDown(pressplaysound)) // la condition ici ce serait if joueur se transporte vers le projectile 
//        {
//            FMOD.Studio.PLAYBACK_STATE fmodPbState;
//            soundevent.getPlaybackState(out fmodPbState);
//            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
//            {
//                soundevent.start();
//            }
//        }
//        if (Input.GetKeyUp(presstoplaysound)) // Ici la condiiton est si la balle reviens sur le joueur
//        {
//            soundevent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//        }


//    FMODUnity.RuntimeManager.PlayOneShot("event:/Player/activation gauche");
//    }
//}
