using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//script attach� au balise qui va les ajouter et les enlever � la liste
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
        // V�rifie si l'objet en collision a le tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Balise touch�e par le joueur");
            // Retire cet objet de la liste (s'il est pr�sent)
            listBalise.listDesBalises.Remove(this.gameObject); // "gameObject" fait r�f�rence � l'objet auquel ce script est attach�
            Destroy(gameObject); // D�truire l'objet
        }
    }
}
