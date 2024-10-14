using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ce script sert au shoot et � suivre la balle
public class SendBall : MonoBehaviour
{
    public BallTrajectory ballPrefab; // Le prefab de votre balle
    public Transform shootPoint; // Le point d'origine du tir
    public float launchForce = 10f; // La force appliqu�e au projectile

    [SerializeField] private bool hasShoot = false;

    public ListBalise listBalises;
    private int currentBaliseList = 0; // L'indice de la balise actuelle
    public float moveSpeed; // La vitesse de d�placement du joueur
    public float distanceThreshold; // La distance minimum pour consid�rer que le joueur a atteint une balise

    public bool onMoveToBall = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        BallTrajectory newBall = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        newBall.listBalises = listBalises;
        Rigidbody rb = newBall.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.forward * launchForce, ForceMode.Impulse);
    }

    public void MoveToBall()
    {
        if (currentBaliseList <= listBalises.listDesBalises.Count)
        {
            Debug.Log("je cherche la balle");
            // Obtenir la position de la balise actuelle
            GameObject targetBalise = listBalises.listDesBalises[currentBaliseList];

            // D�placer le joueur vers la balise actuelle
            Vector3 direction = (targetBalise.transform.position - transform.position).normalized; // Calcul de la direction vers la balise
            
            Debug.Log("D�placer le joueur");

            // D�placement du joueur
            transform.position += direction * moveSpeed * Time.deltaTime;
            Debug.Log("D�placement le joueur");

            // V�rifier si le joueur est suffisamment proche de la balise pour la consid�rer comme atteinte
            if (Vector3.Distance(transform.position, targetBalise.transform.position) < distanceThreshold)
            {
                currentBaliseList++; // Passer � la balise suivante
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
