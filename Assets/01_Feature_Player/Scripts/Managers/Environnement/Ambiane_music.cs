//using System;
//using UnityEngine;

//public class Ambiane_music : MonoBehaviour
//{
//    void Awake()
//    {
//        // Cette fonction est appelée dès que le script est initialisé, même avant Start
//        Debug.Log("Awake: Le jeu vient de se lancer.");
//        MaFonction();
//    }

//    void Start()
//    {
//        // Cette fonction est appelée au début du premier frame actif
//        Debug.Log("Start: Début du jeu.");

//        //Musique qui se joue par défaut
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Musique/Musique ambiance");

//        //Son qui se joue toute la zone dans le désert
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/Vent");
//    }

//    public void MusiquePointNarratif()
//    {
//        //Son qui se dEclenche lorsqu'on touche un buisson/herbe
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/contact herbe");
//    }

//    public void MusiqueEnnemiProche()
//    {
//        //Musique lorsequ'on approche d'un ennemi
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/contact herbe");
//    }

//    void MaFonction()
//    {
//        Debug.Log("MaFonction a été lancée !");
//        // Ajoute ici le code que tu veux exécuter au lancement
//    }

//    void AccordDash()
//    {
//        private void Start()
//        {
//            SonAccordDash = FMODUnity.RuntimeManager.CreateInstance("event:/Musique/Accords dash");
//        }

//        SonAccordDash.setParameterByName("NB dash", 0f); // lancer ca quauand on a fait 2 transportations sans toucher le sol
//        SonFleurSaisie.start(); 

//        SonAccordDash.setParameterByName("NB dash", 2f); // lancer ca quauand on a fait 5 transportations sans toucher le sol
//        SonFleurSaisie.start(); 

//        SonAccordDash.setParameterByName("NB dash", 4f); // lancer ca quauand on a fait 8 transportations sans toucher le sol
//        SonFleurSaisie.start(); 
//    }

//    void AccordWallRun()
//    {
//        private void Start()
//        {
//            SonAccordWallrun = FMODUnity.RuntimeManager.CreateInstance("event:/Musique/Accords wall run");
//        }

//        SonAccordWallrun.setParameterByName("NB wallrun", 0f); // lancer ca quauand on a fait 2 wallruns sans toucher le sol
//        SonAccordWallrun.start();

//        SonAccordWallrun.setParameterByName("NB wallrun", 0.5f); // lancer ca quauand on a fait 3  Wallruns sans toucher le sol
//        SonAccordWallrun.start();

//        SonAccordWallrun.setParameterByName("NB wallrun", 1f); // lancer ca quauand on a fait 4 wallruns sans toucher le sol
//        SonAccordWallrun.start();
//    }

//    void AccordEnvole()
//    {
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Musique/Accords envole");
//    }

//}