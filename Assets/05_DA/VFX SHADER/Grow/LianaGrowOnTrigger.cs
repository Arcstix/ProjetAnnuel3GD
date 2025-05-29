using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class LianaGrowOnTrigger : MonoBehaviour
{
    private List<Material> vineMaterials = new List<Material>();
    private bool hasGrown = false;

    private void Start()
    {
        // On récupère tous les matériaux des enfants de cet objet (lianes)
        var renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (var renderer in renderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_Grow"))
                {
                    mat.SetFloat("_Grow", 0f);
                    vineMaterials.Add(mat);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasGrown && other.CompareTag("Outils"))
        {
            hasGrown = true;

            foreach (var mat in vineMaterials)
            {
                StartCoroutine(GrowMaterial(mat));
            }
        }
    }

    private IEnumerator GrowMaterial(Material mat)
    {
        float t = 0f;
        float duration = 2f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float value = Mathf.Lerp(0f, 1f, t / duration);
            mat.SetFloat("_Grow", value);
            yield return null;
        }

        mat.SetFloat("_Grow", 1f);
    }
}