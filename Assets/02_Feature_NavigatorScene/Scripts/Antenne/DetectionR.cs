using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionR : MonoBehaviour
{
    [SerializeField] private bool catalyseurRInPlace = false;
    public PlayingGameState playingGameState;

    // Start is called before the first frame update
    void Start()
    {
        playingGameState = gameObject.GetComponent<PlayingGameState>(); //pas sur de celle-ci comme c'ets relié à une state machine
    }

    // Update is called once per frame
    void Update()
    {
        if (catalyseurRInPlace)
        {
            playingGameState.RightCheck = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("catalyseurRInPlace"))
        {
            catalyseurRInPlace = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("catalyseurRInPlace"))
        {
            catalyseurRInPlace = false;
        }
    }
}
