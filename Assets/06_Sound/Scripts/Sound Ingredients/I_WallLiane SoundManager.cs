using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

public class I_WallLianeSoundManager : MonoBehaviour
{
    public EventReference wallLianeSound;

    public KeyCode pressToPlayWallLianeSound;
    
    private EventInstance wallLianeEvent;
    private AnchorInteraction anchorInteraction;

    private void Awake()
    {
        anchorInteraction = GetComponent<AnchorInteraction>();
    }

    void Start()
    {
        wallLianeEvent = RuntimeManager.CreateInstance(wallLianeSound);
        RuntimeManager.AttachInstanceToGameObject(wallLianeEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        anchorInteraction.OnToolInteraction += PlayWallLianeSound;
    }

    private void Update()
    {
        if (Input.GetKeyUp(pressToPlayWallLianeSound))
        {
            wallLianeEvent.start();
        }
    }

    void PlayWallLianeSound()
    {
        //  Son qui s'activee lorsequ'on a lanc� notre outil sur l'anchor d'un wallrun
        wallLianeEvent.getPlaybackState(out var fmodPbState);
        if (fmodPbState != PLAYBACK_STATE.PLAYING)
        {
            wallLianeEvent.start();
        }
    }
}
