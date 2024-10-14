using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDirect : MonoBehaviour
{
    public string walkableLayer = "Walkable";  // Layer "Walkable"
    private Rigidbody rb;
    //timer entre lequel il va placer une balise
    //list de balise qui s'ajoute au fur et � mesure 
 
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
        // V�rifie si l'objet collisionn� est sur le layer "Walkable"
        if (collision.gameObject.layer == LayerMask.NameToLayer(walkableLayer))
        {
            // Arr�ter la balle en d�sactivant la physique
            rb.isKinematic = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet en collision a le tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            sendBallDirect.hasShoot = false;
            sendBallDirect.onMoveToBall = false;
            Debug.Log("Balise touch�e par le joueur");
            // Retire cet objet de la liste (s'il est pr�sent)
            listBalises.listDesBalises.Remove(this.gameObject); // "gameObject" fait r�f�rence � l'objet auquel ce script est attach�
            Destroy(gameObject); // D�truire l'objet
        }
    }
}
