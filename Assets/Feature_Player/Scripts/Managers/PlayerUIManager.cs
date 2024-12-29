using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Envoie les infos aux UI pour l'instant
public class PlayerUIManager : MonoBehaviour
{
    private Rigidbody playerRb;
    private PlayerInput input;
    private PlayerAbilityManager abilityManager;

    [Header("Reference UI")]
    [SerializeField] private TMP_Text speedText;

    [SerializeField] private GameObject rightChargeObject;
    [SerializeField] private GameObject leftChargeObject;
    
    [SerializeField] private Image rightCharge;
    [SerializeField] private Image leftCharge;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (speedText != null)
        {
            speedText.text = "Speed : " + playerRb.velocity.magnitude.ToString("F2");
        }
        
        // TODO : L'update de la position doit être réalisé à part car on sort de la State quand la charge est finit
        
        // if (input.PlayerActions.AttractionLeft.IsPressed() && abilityManager.ReusableData.LeftObject != null)
        // {
        //     IncreaseLeftCharge();
        // }
        //
        // if (input.PlayerActions.AttractionRight.IsPressed() && abilityManager.ReusableData.RightObject != null)
        // {
        //     IncreaseRightCharge();
        // }
        //
        // if (!input.PlayerActions.AttractionLeft.IsPressed() && abilityManager.ReusableData.LeftObject != null)
        // {
        //     DecreaseLeftCharge();
        // }
        //
        // if (!input.PlayerActions.AttractionRight.IsPressed() && abilityManager.ReusableData.RightObject != null)
        // {
        //     DecreaseRightCharge();
        // }
    }
    
    private void IncreaseLeftCharge()
    {
        Vector3 coordinate = Camera.main.WorldToScreenPoint(abilityManager.ReusableData.LeftObject.transform.position); 
        coordinate = BoundCoordinate(coordinate);
        
        if (GetLeftCharge() >= 1)
        {
            abilityManager.ReusableData.LeftInput = true;
        }
        
        UpdateLeftCharge(Time.deltaTime, coordinate);
    }

    private void IncreaseRightCharge()
    {
        Vector3 coordinate = Camera.main.WorldToScreenPoint(abilityManager.ReusableData.RightObject.transform.position);
        coordinate = BoundCoordinate(coordinate);
        
        if (GetRightCharge() >= 1)
        {
            abilityManager.ReusableData.RightInput = true;
            return;
        }
        
        UpdateRightCharge(Time.deltaTime, coordinate);
    }

    private void DecreaseLeftCharge()
    {
        if (GetLeftCharge() >= 0)
        {
            Vector3 coordinate = Camera.main.WorldToScreenPoint(abilityManager.ReusableData.LeftObject.transform.position); 
            coordinate = BoundCoordinate(coordinate);
            UpdateLeftCharge(-Time.deltaTime * 0.25f, coordinate);
        }
    }
    
    private void DecreaseRightCharge()
    {
        if (GetRightCharge() >= 0)
        {
            Vector3 coordinate = Camera.main.WorldToScreenPoint(abilityManager.ReusableData.RightObject.transform.position);
            coordinate = BoundCoordinate(coordinate);
            UpdateRightCharge(-Time.deltaTime * 0.25f, coordinate);
        }
    }
    
    private static Vector3 BoundCoordinate(Vector3 coordinate)
    {
        if (coordinate.x > Screen.width - 1)
        {
            coordinate.x = Screen.width - 1;
        }

        if (coordinate.x < 1)
        {
            coordinate.x = 1;
        }

        if (coordinate.y > Screen.height - 1)
        {
            coordinate.y = Screen.height - 1;
        }

        if (coordinate.y < 1)
        {
            coordinate.y = 1;
        }

        return coordinate;
    }
    public float GetLeftCharge()
    {
        return leftCharge.fillAmount;
    }

    public float GetRightCharge()
    {
        return rightCharge.fillAmount;
    }
    
    public void UpdateRightCharge(float value, Vector3 coordinate)
    {
        rightCharge.rectTransform.position = coordinate;
        rightCharge.fillAmount += value;

        if (rightCharge.fillAmount == 0)
        {
            rightChargeObject.gameObject.SetActive(false);
        }
        else
        {
            rightChargeObject.gameObject.SetActive(true);
        }
    }

    public void UpdateLeftCharge(float value, Vector3 coordinate)
    {
        leftCharge.rectTransform.position = coordinate;
        leftCharge.fillAmount += value;
        if (leftCharge.fillAmount == 0)
        {
            rightChargeObject.gameObject.SetActive(false);
        }
        else
        {
            leftChargeObject.gameObject.SetActive(true);
        }
    }
    
    public void FadeOutLeftCharge()
    {
        //TODO : Faire une animation de fade
        
        //leftChargeObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0f, 1f);
    }
    
    public void FadeOutRightCharge()
    {
        //rightChargeObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0f, 1f);
    }
}
