using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Envoie les infos aux UI pour l'instant
public class PlayerUIManager : MonoBehaviour
{
    private Rigidbody playerRb;

    [Header("Reference UI")]
    [SerializeField] private TMP_Text speedText;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        speedText.text = "Speed : " + playerRb.velocity.magnitude.ToString("F2");
    }
}
