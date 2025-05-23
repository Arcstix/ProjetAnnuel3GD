using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingSection : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI valueText;

    public void UpdateSectionValue(float value)
    {
        slider.value = value;
        valueText.text = value.ToString();
    }
}
