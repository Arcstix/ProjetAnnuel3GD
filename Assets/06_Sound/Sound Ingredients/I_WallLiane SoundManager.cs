//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class I_WallLianeSoundManager : MonoBehaviour
//{
//    public void WallLiane()
//    {
//        //  Son qui s'activee lorsequ'on a lancé notre outil sur l'anchor d'un wallrun

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
//        if (Input.GetKeyDown(pressplaysound)) // La condition ici serait quand l'outil touche l'anchor du wall run
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

//    FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Lianes wall run");
//    }
//}
