using UnityEngine;

public class RotationYHead : MonoBehaviour, HeadBehaviour
{
    [Tooltip("Vitesse de rotation de la tête")]
    [SerializeField] private float EnemyRotationSpeed; 
    [Tooltip("Angle maximal de rotation")]
    [SerializeField] private float EnemyRotationAngle;
    
    [SerializeField] private Transform headTransform;
    
    private float idleTime;
    
    public void MoveHead()
    {
        if (headTransform == null) return; // Vérification de la tête

        idleTime += Time.deltaTime; // Incrémente le temps écoulé

        float rotationY = Mathf.Sin(idleTime * EnemyRotationSpeed * Mathf.Deg2Rad) * EnemyRotationAngle;
        headTransform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }

    public void ResetValues()
    {
        idleTime = 0;
    }
}
