using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrajectory : MonoBehaviour
{
    public string walkableLayer = "Walkable";  // Layer "Walkable"
    private Rigidbody rb;
    //timer entre lequel il va placer une balise
    //list de balise qui s'ajoute au fur et à mesure 
    [SerializeField] private float timer;
    [SerializeField] private float timerBalise;
    [SerializeField] private GameObject balise;
    [SerializeField] private Transform poseBalise;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        GameObject newBalise = Instantiate(balise, poseBalise.position, Quaternion.identity);

        // Si la balise a un Rigidbody, désactiver sa physique pour qu'elle reste en place
        Rigidbody rb = newBalise.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Désactiver la physique (optionnel si tu veux qu'elle flotte)
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet collisionné est sur le layer "Walkable"
        if (collision.gameObject.layer == LayerMask.NameToLayer(walkableLayer))
        {
            // Arrêter la balle en désactivant la physique
            //rb.isKinematic = true;
        }
    }
}
