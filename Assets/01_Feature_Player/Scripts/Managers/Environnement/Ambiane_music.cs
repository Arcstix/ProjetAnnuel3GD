using System;
using UnityEngine;

public class Ambiane_music : MonoBehaviour
{
    void Awake()
    {
        // Cette fonction est appelée dès que le script est initialisé, même avant Start
        Debug.Log("Awake: Le jeu vient de se lancer.");
        MaFonction();
    }

    void Start()
    {
        // Cette fonction est appelée au début du premier frame actif
        Debug.Log("Start: Début du jeu.");

        //Musique qui se joue par défaut
        FMODUnity.RuntimeManager.PlayOneShot("event:/Musique/Musique ambiance");

        //Son qui se joue toute la zone dans le désert
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/Vent");
    }

    public void MusiquePointNarratif()
    {
        //Son qui se dEclenche lorsqu'on touche un buisson/herbe
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/contact herbe");
    }

    public void MusiqueEnnemiProche()
    {
        //Musique lorsequ'on approche d'un ennemi
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/contact herbe");
    }

    void MaFonction()
    {
        Debug.Log("MaFonction a été lancée !");
        // Ajoute ici le code que tu veux exécuter au lancement
    }

    public void BuissonCollision()
    {
        //Son qui se dEclenche lorsqu'on touche un buisson/herbe
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/contact herbe");
    }


}