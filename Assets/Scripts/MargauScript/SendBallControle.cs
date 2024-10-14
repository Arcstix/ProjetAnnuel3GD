using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBallControle : MonoBehaviour
{
    public BallMovement ballPrefab; // Le prefab de votre balle
    public Transform shootPoint; // Le point d'origine du tir
    

    public bool hasShoot = false;

    public ListBalise listBalises;
    private int currentBaliseList = 0; // L'indice de la balise actuelle
    public float moveSpeed; // La vitesse de déplacement du joueur
    public float distanceThreshold; // La distance minimum pour considérer que le joueur a atteint une balise

    public bool onMoveToBall = false;
    public PlayerMovement playerMovement;

    

    // Start is called before the first frame update
    void Start()
    {
        shootPoint = GetComponentInChildren<Transform>().Find("BulletPts");
    }

    // Update is called once per frame
    void Update()
    {
        if (onMoveToBall)
        {

            MoveToBall();
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (!hasShoot)
            {
                Shoot();
                return;
            }

            if (hasShoot)
            {
                onMoveToBall = true;
                return;
            }

        }
    }

    public void Shoot()
    {
        hasShoot = true;
        BallMovement newBall = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        newBall.listBalises = listBalises;
        newBall.playerMovement = playerMovement;
        newBall.sendBallControle = this;
        //listBalises.listDesBalises.Add(newBall.gameObject);
        Rigidbody rb = newBall.GetComponent<Rigidbody>();
    }

    public void MoveToBall()
    {
        if (currentBaliseList <= listBalises.listDesBalises.Count)
        {
            Debug.Log("je cherche la balle");
            // Obtenir la position de la balise actuelle
            GameObject targetBalise = listBalises.listDesBalises[currentBaliseList];

            // Déplacer le joueur vers la balise actuelle
            Vector3 direction = (targetBalise.transform.position - transform.position).normalized; // Calcul de la direction vers la balise

            Debug.Log("Déplacer le joueur");

            // Déplacement du joueur
            transform.position += direction * moveSpeed * Time.deltaTime;
            Debug.Log("Déplacement le joueur");

            // Vérifier si le joueur est suffisamment proche de la balise pour la considérer comme atteinte
            if (Vector3.Distance(transform.position, targetBalise.transform.position) < distanceThreshold)
            {
                currentBaliseList++; // Passer à la balise suivante
                Debug.Log("++1");
            }
        }
        else
        {
            onMoveToBall = false;
            hasShoot = false;
        }

        //fonction pour suivre la balle
        //hasShoot = false;

    }
}
