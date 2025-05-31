using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RuntimeManager.PlayOneShot("event:/Musique/Musique Hub");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
