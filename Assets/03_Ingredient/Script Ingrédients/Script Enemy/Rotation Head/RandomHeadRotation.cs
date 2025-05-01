using UnityEngine;

public class RandomHeadRotation : MonoBehaviour, HeadBehaviour
{
    [Header("Référence Mesh Ennemi")]
    [Tooltip("Référence à la zone mobile de l'ennemi")]
    [SerializeField] private Transform headTransform;
    
    [Header("MoveHead")]
    [Header("Angles en degrés")]
    [SerializeField] private float minHorizontalAngle = -45f;
    [SerializeField] private float maxHorizontalAngle = 45f;
    [SerializeField] private float minVerticalAngle = -20f;
    [SerializeField] private float maxVerticalAngle = 20f;

    [Header("Timing")]
    [SerializeField] private float interval = 2f;        // Temps entre deux nouvelles cibles
    [SerializeField] private float rotationSpeed = 2f;   // Vitesse de rotation vers la cible

    [Header("Gizmos Parameter")]
    [SerializeField] private float arcRadius;
    
    private Quaternion targetRotation;
    private float timer;
    
    public void MoveHead()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            SetNewTargetRotation();
            timer = 0f;
        }

        // Lerp vers la rotation cible
        headTransform.localRotation = Quaternion.Slerp(headTransform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    
    void SetNewTargetRotation()
    {
        float randomYaw = Random.Range(minHorizontalAngle, maxHorizontalAngle);
        float randomPitch = Random.Range(minVerticalAngle, maxVerticalAngle);

        targetRotation = Quaternion.Euler(randomPitch, randomYaw, headTransform.eulerAngles.z);
    }

    public void ResetValues()
    {
        
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        if (headTransform)
        {
            Vector3 origin = headTransform.position;
            Vector3 forward = headTransform.forward;

            // Dessin de l'arc horizontal
            DrawArc(origin, Vector3.up, forward, minHorizontalAngle, maxHorizontalAngle, arcRadius);

            // Dessin de l'arc vertical
            DrawArc(origin, transform.right, forward, minVerticalAngle, maxVerticalAngle, arcRadius);
        }
    }

    void DrawArc(Vector3 origin, Vector3 axis, Vector3 forward, float minAngle, float maxAngle, float radius)
    {
        int segments = 20;
        Vector3 lastPoint = origin + Quaternion.AngleAxis(minAngle, axis) * (forward * radius);

        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            float angle = Mathf.Lerp(minAngle, maxAngle, t);
            Vector3 nextPoint = origin + Quaternion.AngleAxis(angle, axis) * forward * radius;
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
    }
}
