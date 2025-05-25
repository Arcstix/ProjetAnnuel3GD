using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TMPGlowEffect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public TMP_Text tmpText;
    private Material matInstance;

    void Start()
    {
        // Crée une instance du matériau pour ne pas affecter les autres textes
        matInstance = Instantiate(tmpText.fontMaterial);
        tmpText.fontMaterial = matInstance;
    }

    public void OnSelect(BaseEventData eventData)
    {
        matInstance.EnableKeyword("GLOW_ON");
        matInstance.SetColor("_GlowColor", Color.yellow); // ou une autre couleur
        matInstance.SetFloat("_GlowPower", 10f);         // intensité du glow
    }

    public void OnDeselect(BaseEventData eventData)
    {
        matInstance.SetFloat("_GlowPower", 0f);
    }
}