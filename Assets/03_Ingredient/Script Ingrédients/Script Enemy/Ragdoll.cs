using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Collider[] _allColliders;
    private Rigidbody[] _allRigidbodies;

    private void Awake()
    {
        _allColliders = GetComponentsInChildren<Collider>(true);
        _allRigidbodies = GetComponentsInChildren<Rigidbody>(true);
        
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach(Collider col in _allColliders)
        {
            if (col.gameObject.CompareTag("Ragdoll"))
            {
                col.enabled = isRagdoll;
            }
        }

        foreach (Rigidbody rb in _allRigidbodies)
        {
            if (rb.gameObject.CompareTag("Ragdoll"))
            {
                rb.isKinematic = !isRagdoll;
                rb.useGravity = isRagdoll;
            }
        }
    }
}
