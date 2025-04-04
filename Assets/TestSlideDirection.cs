using UnityEngine;

public class TestSlideDirection : MonoBehaviour
{
    [SerializeField] float debugDist = 2;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            Vector3 normal = hit.normal;
            
            Vector3 velocity = transform.forward;
            float maxAngle = 170f;
            
            Vector3 slideDirection = GetSlideDirection(normal, velocity, maxAngle);
            
            Debug.DrawRay(transform.position, normal * debugDist, Color.blue);
            Debug.DrawRay(transform.position, velocity * debugDist, Color.green);
            Debug.DrawRay(transform.position, slideDirection * debugDist, Color.red);
        }
    }

    public static Vector3 GetSlideDirection(Vector3 normal, Vector3 velocity, float maxAngle)
    {
        Vector3 tangent = Vector3.Cross(normal, Vector3.up);
        float signedAngle = Vector3.SignedAngle(normal, velocity.normalized, -Vector3.up);
        float multiplier = Mathf.Abs(signedAngle) >= maxAngle ? 0 : Mathf.Sign(signedAngle);

        return multiplier * tangent;
    }
}