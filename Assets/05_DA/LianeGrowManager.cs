using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LianeGrowManager : MonoBehaviour
{
    [Tooltip("Courbe de progression de la pousse (0 -> 1)")]
    public AnimationCurve growthCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Tooltip("Durée totale de la pousse")]
    public float growDuration = 2f;

    [Tooltip("Valeur initiale de la pousse (_Grow), entre 0 et 1")]
    [Range(0f, 1f)]
    public float startGrowValue = 0.2f;

    private List<Material> vineMaterials = new List<Material>();
    private bool hasGrown = false;

    private void Awake()
    {
        AnchorInteraction anchor = GetComponent<AnchorInteraction>();
        if (anchor != null)
        {
            anchor.OnToolInteraction += TriggerLianeGrowth;
        }

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in renderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_Grow"))
                {
                    mat.SetFloat("_Grow", startGrowValue);
                    vineMaterials.Add(mat);

                    // Debug : afficher la valeur initiale appliquée
                    Debug.Log($"Material {mat.name} _Grow initialisé à {startGrowValue}");
                }
            }
        }
    }

    void TriggerLianeGrowth()
    {
        if (hasGrown) return;
        hasGrown = true;

        foreach (var mat in vineMaterials)
        {
            StartCoroutine(GrowMaterial(mat));
        }
    }

    private IEnumerator GrowMaterial(Material mat)
    {
        float time = 0f;

        while (time < growDuration)
        {
            time += Time.deltaTime;
            float progress = Mathf.Clamp01(time / growDuration);
            float curveValue = growthCurve.Evaluate(progress);

            // La croissance part de startGrowValue vers 1 selon la courbe
            float value = Mathf.Lerp(startGrowValue, 1f, curveValue);

            mat.SetFloat("_Grow", value);
            yield return null;
        }

        mat.SetFloat("_Grow", 1f);
    }
}
