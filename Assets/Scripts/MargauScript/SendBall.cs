using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBall : MonoBehaviour
{
    public GameObject ballPrefab; // Le prefab de votre balle
    public Transform shootPoint; // Le point d'origine du tir
    public float launchForce = 10f; // La force appliquée au projectile

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        ballPrefab = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = ballPrefab.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.forward * launchForce, ForceMode.Impulse);
    }

    public void MoveToBall()
    {

    }
}
