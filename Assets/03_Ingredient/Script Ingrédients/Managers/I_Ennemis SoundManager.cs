//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class I_EnnemisSoundManager : MonoBehaviour
//{
//    public void IdleEnnemi()
//    {
//        //  Son qui s'activee lorseque l'ennemi est en etat idle

//        [FMODUnity.EventRef]
//        public string selectsound;

//        public KeyCode presstoplaysound;

//        void Start()
//        {
//            soundevent = FMODUnity.RuntimeManager.CreateInstance(selectsound);
//        }

//        void Update()
//        {
//            FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundevent, GetComponent<Transform>(), GetComponent<Rigidbody>());
//            Playsound();
//        }

//        void Playsound()
//        {
//            if (Input.GetKeyDown(pressplaysound)) // la condition ici ce serait quand l'ennemi est en etat IDLE
//            {
//                FMOD.Studio.PLAYBACK_STATE fmodPbState;
//                soundevent.getPlaybackState(out fmodPbState);
//                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
//                {
//                    soundevent.setParameterByName("Etat Idle", 0f);
//                    soundevent.start();
//                }
//            }
//            if (Input.GetKeyUp(presstoplaysound)) // Ici la condiiton est s'il sort de l'etat idle
//            {
//                soundevent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//                soundevent.setParameterByName("Etat Idle", 1f);
//        }
//        }

//    FMODUnity.RuntimeManager.PlayOneShot("event:/Ennemies/Ennemies Idle");
//    }

//    public void EnnemiDeath()
//    {
//        //  Son qui s'activee lorseque l'ennemi meurt

//        [FMODUnity.EventRef]
//        public string selectsound;

//        public KeyCode presstoplaysound;

//        void Start()
//        {
//            soundevent = FMODUnity.RuntimeManager.CreateInstance(selectsound);
//        }

//        void Update()
//        {
//            FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundevent, GetComponent<Transform>(), GetComponent<Rigidbody>());
//            Playsound();
//        }

//        void Playsound()
//        {
//            if (Input.GetKeyDown(pressplaysound)) // La condition ici serait quand l'ennemi meurt
//            {
//                FMOD.Studio.PLAYBACK_STATE fmodPbState;
//                soundevent.getPlaybackState(out fmodPbState);
//                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
//                {
//                    soundevent.start();
//                }
//            }
//            if (Input.GetKeyUp(presstoplaysound)) // 
//            {
//                soundevent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//            }
//        }

//    FMODUnity.RuntimeManager.PlayOneShot("event:/Ennemies/Ennemies death");
//    }

//    public void EnnemiDetection()
//    {
//    //  Son qui s'activee lorseque l'ennemi nous detecte

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
//        if (Input.GetKeyDown(pressplaysound)) // La condition ici serait quand l'ennemi detecte le joueur 
//        {
//            FMOD.Studio.PLAYBACK_STATE fmodPbState;
//            soundevent.getPlaybackState(out fmodPbState);
//            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
//            {
//                soundevent.start();
//            }
//        }
//        if (Input.GetKeyUp(presstoplaysound)) // 
//        {
//            soundevent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//        }
//    }

//    FMODUnity.RuntimeManager.PlayOneShot("event:/Ennemies/Ennemies détection");
//    }

//    public void EnnemiAttaque()
//    {
//    //  Son qui s'activee lorseque l'ennemi nous brule

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
//        if (Input.GetKeyDown(pressplaysound)) // La condition ici serait quand l'ennemi est en etat "attaque" (nous brule) 
//        {
//            FMOD.Studio.PLAYBACK_STATE fmodPbState;
//            soundevent.getPlaybackState(out fmodPbState);
//            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
//            {
//                soundevent.setParameterByName("Etat attaque", 0f);
//                soundevent.start();
//            }
//        }
//        if (Input.GetKeyUp(presstoplaysound)) // 
//        {
//            soundevent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//            soundevent.setParameterByName("Etat attaque", 1f);
//    }
//    }

//        FMODUnity.RuntimeManager.PlayOneShot("event:/Ennemies/Ennemies charge attaque");
//    }
//}
