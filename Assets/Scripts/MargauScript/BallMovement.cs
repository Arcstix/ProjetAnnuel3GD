using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
   
    public float moveSpeed = 5f;           // Vitesse de déplacement
    public Rigidbody rb;                   // Référence au Rigidbody du joueur
    public Transform cameraTransform;      // Référence à la caméra qui va suivre le joueur
    public Vector3 cameraOffset = new Vector3(0, 5, -7);  // Offset de la caméra par rapport au joueur
    public float mouseSensitivity = 100f;   // Sensibilité de la souris
    private float pitch = 0f;               // Rotation verticale (haut/bas)
    private float yaw = 0f;                 // Rotation horizontale (gauche/droite)
    private Vector3 movement;              // Vector3 pour stocker le mouvement du joueur
    private bool ballCanMove = true;

    public string walkableLayer = "Walkable";  // Layer "Walkable"
 
    //timer entre lequel il va placer une balise
    //list de balise qui s'ajoute au fur et à mesure 
    [SerializeField] private float timer;
    [SerializeField] private float timerBalise;
    [SerializeField] private Balises balise;
    [SerializeField] private Transform poseBalise;

    public ListBalise listBalises;
    public SendBallControle sendBallControle;

    public PlayerMovement playerMovement;
    void Start()
    {
        ballCanMove = true;
        playerMovement.canMove = false;
        // Verrouiller le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ballCanMove = false;
            playerMovement.canMove = true;
            rb.isKinematic = true;
            GetComponentInChildren<Camera>().enabled = false;
            listBalises.listDesBalises.Add(this.gameObject);

        }
        if (ballCanMove)
        {
            // Récupérer les inputs des touches Z, Q, S, D
            float moveX = Input.GetAxisRaw("Horizontal"); // Pour Q et D
            float moveZ = Input.GetAxisRaw("Vertical");   // Pour Z et S

            // Créer un vecteur de mouvement
            movement = new Vector3(moveX, 0f, moveZ).normalized; // Normalisé pour garder une vitesse constante
                                                                 // Capturer les mouvements de la souris
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Mettre à jour la rotation en fonction des mouvements de la souris
            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -35f, 60f); // Limite la rotation verticale pour éviter les retournements de caméra

            // Appliquer la rotation de la caméra autour du joueur
            Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);

            // Mettre à jour la position de la caméra avec la rotation
            cameraTransform.position = rb.position + cameraRotation * cameraOffset;
            cameraTransform.LookAt(rb.position);  // La caméra regarde toujours vers le joueur
        }   
        if (rb.isKinematic == false)
        {
            timer += Time.deltaTime;

            if (timer >= timerBalise)
            {
                timer = 0;
                Balises newBalise = Instantiate(balise, poseBalise.position, Quaternion.identity);
                newBalise.listBalise = listBalises;
                //add la balise instancier à la liste des balises
                listBalises.listDesBalises.Add(newBalise.gameObject);
            }
        }
            
        else
        {
            // Arrêter la balle en désactivant la physique
            rb.isKinematic = true;
            playerMovement.canMove = true;
            ballCanMove = true;
            GetComponentInChildren<Camera>().enabled = false;
        }
       
    }
     void FixedUpdate()
     {
        // Appliquer le mouvement au Rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
       
     }
    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet collisionné est sur le layer "Walkable"
        if (collision.gameObject.layer == LayerMask.NameToLayer(walkableLayer))
        {
            listBalises.listDesBalises.Add(this.gameObject);
            // Arrêter la balle en désactivant la physique
            rb.isKinematic = true;
            playerMovement.canMove = true;
            ballCanMove = false;
            GetComponentInChildren<Camera>().enabled = false;
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    // Vérifie si l'objet en collision a le tag "Player"
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        sendBallControle.hasShoot = false;
    //        sendBallControle.onMoveToBall = false;
    //        Debug.Log("Balise touchée par le joueur");
    //        // Retire cet objet de la liste (s'il est présent)
    //        listBalises.listDesBalises.Remove(this.gameObject); // "gameObject" fait référence à l'objet auquel ce script est attaché
    //        Destroy(gameObject); // Détruire l'objet
    //    }
    //}
}

