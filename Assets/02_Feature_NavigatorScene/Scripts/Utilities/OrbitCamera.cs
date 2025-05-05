using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    

public class OrbitCamera : MonoBehaviour
{
    public Transform target;          // L'objet autour duquel la caméra tourne
    public float distance = 5.0f;     // Distance initiale de la caméra
    public float xSpeed = 120.0f;     // Vitesse de rotation horizontale
    public float ySpeed = 120.0f;     // Vitesse de rotation verticale

    public float yMinLimit = -20f;    // Angle vertical minimum
    public float yMaxLimit = 80f;     // Angle vertical maximum

    public float distanceMin = 2f;    // Zoom minimum
    public float distanceMax = 15f;   // Zoom maximum

    private float x = 0.0f;           // Rotation horizontale
    private float y = 0.0f;           // Rotation verticale

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Verrouille le curseur si nécessaire
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target)
        {
            // Rotation avec clic droit (ou toujours actif si tu préfères)
            if (Input.GetMouseButton(1)) 
            {
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            }

            // Zoom avec molette
            distance -= Input.GetAxis("Mouse ScrollWheel") * 5f;
            distance = Mathf.Clamp(distance, distanceMin, distanceMax);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
