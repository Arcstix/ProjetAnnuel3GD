using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private bool activeOnAwake;
    [SerializeField] private Button defaultButton;

    private void Awake()
    {
        gameObject.SetActive(activeOnAwake);
    }

    private void OnEnable()
    {
        if (defaultButton)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
        }
    }
}
