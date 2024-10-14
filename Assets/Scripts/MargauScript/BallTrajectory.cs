using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ce script est attach� � l aballe lanc� et sert � poser les balises au cours de sa course
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

    public ListBalise listBalises;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (rb.isKinematic == false)
        {
            timer += Time.deltaTime;

            if (timer >= timerBalise)
            {
                timer = 0;
                GameObject newBalise = Instantiate(balise, poseBalise.position, Quaternion.identity);
                //add la balise instancier � la liste des balises
                listBalises.listDesBalises.Add(newBalise);
            }
        }      
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rifie si l'objet collisionn� est sur le layer "Walkable"
        if (collision.gameObject.layer == LayerMask.NameToLayer(walkableLayer))
        {
            // Arr�ter la balle en d�sactivant la physique
            rb.isKinematic = true;
        }
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    listDesBalises.listDesBalises.Clear();
        //    Destroy(gameObject);
        //}
    }
}
