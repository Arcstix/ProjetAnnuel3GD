using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionL : MonoBehaviour
{
    [SerializeField] private bool catalyseurLInPlace = false;
    public PlayingGameState playingGameState;
    // Start is called before the first frame update
    void Start()
    {
        playingGameState = gameObject.GetComponent<PlayingGameState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (catalyseurLInPlace)
        {
            playingGameState.LeftCheck = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("catalyseurLInPlace"))
        {
            catalyseurLInPlace = true;
                
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("catalyseurLInPlace"))
        {
            catalyseurLInPlace = false;
                
        }
    }
}
