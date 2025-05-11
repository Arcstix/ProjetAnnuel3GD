using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambiane_music : MonoBehaviour
{
    void Awake()
    {
        // Cette fonction est appelée dès que le script est initialisé, même avant Start
        Debug.Log("Awake: Le jeu vient de se lancer.");
        //MaFonction();
    }

    void Start()
    {
        // Cette fonction est appelée au début du premier frame actif
        Debug.Log("Start: Début du jeu.");

        //Son qui se joue toute la zone dans le désert
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/Vent"); // CA FAUT LAISSER !
    }

    public void MusiqueDeBase()
    {
        public string selectsoundMusiqueDeBase;
        FMOD.Studio.EventInstance soundeventMusiqueDeBase;

        void start()
        {
            soundeventMusiqueDeBase = FMODUnity.RuntimeManager.CreateInstance(selectsoundMusiqueDeBase);
        }

        void Update()
        {
            PlaysoundMusiqueDeBase();
        }

        void PlaysoundMusiqueDeBase()
        {
            FMOD.Studio.PLAYBACK_STATE fmodPbState;
            soundeventMusiqueDeBase.getPlaybackState(out fmodPbState);
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundeventMusiqueDeBase.start();
            }

            if (Input.GetKeyUp(presstoplaysound)) // lorse que le joueur entre dans un triggerBox (avec le tag 'PhaseDynamique')
            {
                soundeventMusiqueDeBase.setParameterByName("Phase dynamique", 1f);
            }

            if (Input.GetKeyUp(presstoplaysound)) // lorse que le joueur SORS de la triggerBox 
            {
                soundeventMusiqueDeBase.setParameterByName("Phase dynamique", 0f);
            }
        }
    }



    public void AccordDash()
    {
        void Start()
        {
            SonAccordDash = FMODUnity.RuntimeManager.CreateInstance("event:/Musique/Accords dash");
        }

        SonAccordDash.setParameterByName("NB dash", 0f); // lancer ca quauand on a fait 2 transportations sans toucher le sol
        SonAccordDash.start();

        SonAccordDash.setParameterByName("NB dash", 2f); // lancer ca quauand on a fait 5 transportations sans toucher le sol
        SonAccordDash.start();

        SonAccordDash.setParameterByName("NB dash", 4f); // lancer ca quauand on a fait 8 transportations sans toucher le sol
        SonAccordDash.start();
    }

    public void AccordWallRun()
    {
        void Start()
        {
            SonAccordWallrun = FMODUnity.RuntimeManager.CreateInstance("event:/Musique/Accords wall run");
        }

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
