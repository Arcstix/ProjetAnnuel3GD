using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDirect : MonoBehaviour
{
    public string walkableLayer = "Walkable";  // Layer "Walkable"
    private Rigidbody rb;
    //timer entre lequel il va placer une balise
    //list de balise qui s'ajoute au fur et à mesure 
 
    public ListBalise listBalises;
    public SendBallDirect sendBallDirect;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet collisionné est sur le layer "Walkable"
        if (collision.gameObject.layer == LayerMask.NameToLayer(walkableLayer))
        {
            // Arrêter la balle en désactivant la physique
            rb.isKinematic = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet en collision a le tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            sendBallDirect.hasShoot = false;
            sendBallDirect.onMoveToBall = false;
            Debug.Log("Balise touchée par le joueur");
            // Retire cet objet de la liste (s'il est présent)
            listBalises.listDesBalises.Remove(this.gameObject); // "gameObject" fait référence à l'objet auquel ce script est attaché
            Destroy(gameObject); // Détruire l'objet
        }
    }
}
