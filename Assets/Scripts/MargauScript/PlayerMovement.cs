using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float mouseSensitivity = 100f; // Sensibilité de la souris
    public float verticalRotationLimit = 80f; // Limite d'angle vertical

    private float cameraYaw = 0f; // Rotation horizontale (autour de Y)
    private float cameraPitch = 0f; // Rotation verticale (autour de X)

    public float jumpHeight = 1.5f; // Hauteur du saut
    public float gravity = -9.81f;  // Force de la gravité
    private Vector3 velocity;       // Vitesse verticale pour le saut et la chute

    void Start()
    {
        // Verrouiller le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Vérification si controller et caméra sont assignés
        if (controller == null || cameraTransform == null)
        {
            Debug.LogWarning("CharacterController ou CameraTransform n'est pas assigné.");
            return;
        }

        // Contrôle de la caméra avec la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraYaw += mouseX; // Accumuler la rotation horizontale
        cameraPitch -= mouseY; // Accumuler la rotation verticale
        cameraPitch = Mathf.Clamp(cameraPitch, -verticalRotationLimit, verticalRotationLimit); // Limiter la rotation verticale

        // Appliquer les rotations à la caméra
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);

        // Obtenir les axes de mouvement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Si on a du mouvement
        if (direction.magnitude >= 0.1f)
        {
            // Calculer l'angle cible basé sur la direction de la caméra
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraYaw;

            // Lisser la rotation du joueur
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            // Calculer la direction finale du déplacement en fonction de la rotation de la caméra
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Déplacer le joueur
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

       
            if (Input.GetButtonDown("Jump"))
            {
                // Calculer la vitesse verticale nécessaire pour atteindre la hauteur de saut désirée
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        

        // Appliquer la gravité (même si le personnage n'est pas au sol)
        velocity.y += gravity * Time.deltaTime;

        // Appliquer la vitesse verticale au personnage
        controller.Move(velocity * Time.deltaTime);
    }
}
