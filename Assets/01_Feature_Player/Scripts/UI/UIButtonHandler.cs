using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    [SerializeField] private bool primaryButton = false;
    
    private void Awake()
    {
        //GetComponent<Button>().onClick.AddListener(OnButtonValidate);
        if (primaryButton)
        {
            GetComponent<Button>().Select();
        }
    }

    private void OnButtonValidate()
    {
        StartCoroutine(ReselectButton());
    }
    
    private IEnumerator ReselectButton()
    {
        Debug.Log(gameObject.name + " is pressed");
        yield return null; // attendre un frame
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
}
