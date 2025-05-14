using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AmbianceMusic : MonoBehaviour
{
    public EventReference baseMusic;
    
    private EventInstance BaseMusicEvent;
    
    public KeyCode pressToPlayDynamiquePhaseSound;
    public KeyCode pressToPlayBasePhaseSound;
    
    private int counter = 0;
    
    void Start()
    {
        BaseMusicEvent = RuntimeManager.CreateInstance(baseMusic);
    }
    
    void Update()
    {
        PlayMusic();
    }
    
    void PlayMusic()
    {
        BaseMusicEvent.getPlaybackState(out var fmodPbState);
        if (fmodPbState != PLAYBACK_STATE.PLAYING)
        {
            PlayBaseMusic();
            BaseMusicEvent.start();
        }

        if (Input.GetKeyDown(pressToPlayDynamiquePhaseSound)) // lorsque le joueur entre dans un triggerBox (avec le tag 'PhaseDynamique')
        {
            PlayDynamicMusic();
        }

        if (Input.GetKeyDown(pressToPlayBasePhaseSound)) // lorsque le joueur SORS de la triggerBox 
        {
            PlayBaseMusic();
        }
    }

    public void PlayBaseMusic()
    {
        BaseMusicEvent.setParameterByName("Phase dynamique", 0f);
    }

    public void PlayDynamicMusic()
    {
        BaseMusicEvent.setParameterByName("Phase dynamique", 1f);
    }
}
