using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrajectory : MonoBehaviour
{
    public string walkableLayer = "Walkable";  // Layer "Walkable"
    private Rigidbody rb;
    //timer entre lequel il va placer une balise
    //list de balise qui s'ajoute au fur et � mesure 
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

        // Si la balise a un Rigidbody, d�sactiver sa physique pour qu'elle reste en place
        Rigidbody rb = newBalise.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // D�sactiver la physique (optionnel si tu veux qu'elle flotte)
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rifie si l'objet collisionn� est sur le layer "Walkable"
        if (collision.gameObject.layer == LayerMask.NameToLayer(walkableLayer))
        {
            // Arr�ter la balle en d�sactivant la physique
            //rb.isKinematic = true;
        }
    }
}
