using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

public class Ambiane_music : MonoBehaviour
{
    public EventReference baseMusic;
    private EventInstance BaseMusicEvent;
    
    private EventInstance SonAccordWallrun;
    private EventInstance SonAccordDash;
    
    public KeyCode pressToPlayDynamiquePhaseSound;
    
    void Awake()
    {
        // Cette fonction est appel�e d�s que le script est initialis�, m�me avant Start
        Debug.Log("Awake: Le jeu vient de se lancer.");
        //MaFonction();
    }

    void Start()
    {
        // Cette fonction est appel�e au d�but du premier frame actif
        Debug.Log("Start: D�but du jeu.");
        BaseMusicEvent = RuntimeManager.CreateInstance(baseMusic);

        SonAccordWallrun = RuntimeManager.CreateInstance("event:/Musique/Accords wall run");
        SonAccordDash = RuntimeManager.CreateInstance("event:/Musique/Accords dash");
    }
    
    void Update()
    {
        PlaysoundMusiqueDeBase();
    }
    
    void PlaysoundMusiqueDeBase()
    {
        BaseMusicEvent.getPlaybackState(out var fmodPbState);
        if (fmodPbState != PLAYBACK_STATE.PLAYING)
        {
            BaseMusicEvent.start();
        }

        if (Input.GetKeyUp(pressToPlayDynamiquePhaseSound)) // lorse que le joueur entre dans un triggerBox (avec le tag 'PhaseDynamique')
        {
            BaseMusicEvent.setParameterByName("Phase dynamique", 1f);
        }
            
        if (Input.GetKeyUp(pressToPlayDynamiquePhaseSound)) // lorse que le joueur SORS de la triggerBox 
        {
            BaseMusicEvent.setParameterByName("Phase dynamique", 0f);
        }
    }



    public void AccordDash()
    {
        SonAccordDash.setParameterByName("NB dash", 0f); // lancer ca quauand on a fait 2 transportations sans toucher le sol
        SonAccordDash.start();

        SonAccordDash.setParameterByName("NB dash", 2f); // lancer ca quauand on a fait 5 transportations sans toucher le sol
        SonAccordDash.start();

        SonAccordDash.setParameterByName("NB dash", 4f); // lancer ca quauand on a fait 8 transportations sans toucher le sol
        SonAccordDash.start();
    }

    public void AccordWallRun()
    {
        SonAccordWallrun.setParameterByName("NB wallrun", 0f); // lancer ca quauand on a fait 2 wallruns sans toucher le sol
        SonAccordWallrun.start();

        SonAccordWallrun.setParameterByName("NB wallrun", 0.5f); // lancer ca quauand on a fait 3  Wallruns sans toucher le sol
        SonAccordWallrun.start();

        SonAccordWallrun.setParameterByName("NB wallrun", 1f); // lancer ca quauand on a fait 4 wallruns sans toucher le sol
        SonAccordWallrun.start();
    }

    public void AccordEnvole()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Musique/Accords envole"); // lorse que le joueur fait une gagne beaucoup de hauteur
    }

}
