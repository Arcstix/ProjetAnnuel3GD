using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonValidate);
    }

    private void OnButtonValidate()
    {
        StartCoroutine(ReselectButton());
    }
    
    private IEnumerator ReselectButton()
    {
        yield return null; // attendre un frame
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
}
