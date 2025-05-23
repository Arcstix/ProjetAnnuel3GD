using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private bool activeOnAwake;
    [SerializeField] private Button defaultButton;
    [SerializeField] private Slider defaultSlider;
    
    private void OnEnable()
    {
        if (defaultButton)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
        }

        if (defaultSlider)
        {
            EventSystem.current.SetSelectedGameObject(defaultSlider.gameObject);
        }
    }
}
