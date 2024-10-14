using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//script attaché au balise qui va les ajouter et les enlever à la liste
public class Balises : MonoBehaviour
{
    public ListBalise listBalise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet en collision a le tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Balise touchée par le joueur");
            // Retire cet objet de la liste (s'il est présent)
            listBalise.listDesBalises.Remove(this.gameObject); // "gameObject" fait référence à l'objet auquel ce script est attaché
            Destroy(gameObject); // Détruire l'objet
        }
    }
}
