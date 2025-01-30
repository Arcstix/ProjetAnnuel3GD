using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class DestructibleState : MonoBehaviour
{
    public abstract void Enter(GameObject refObject);
    public abstract void Tick(GameObject refObject);
    public abstract void Exit(GameObject refObject);
    
    private void Awake()
    {
       
    }
    // public bool PlayerIsDetected()
    // {
    //     // Cible potentielle, probablement le joueur dans ce contexte
    //     Transform target = playerTransform; // Supposons que playerTransform est d�j� d�fini
    //
    //     // 1. V�rifiez si le joueur est dans la plage de d�tection
    //     if (Vector3.Distance(transform.position, target.position) <= detectionRange)
    //     {
    //         // 2. V�rifiez si le joueur est dans le bon angle
    //         Vector3 directionToTarget = (target.position - transform.position).normalized;
    //         if (Vector3.Angle(transform.forward, directionToTarget) <= detectionAngle) // Angle divis� par 2 car c'est de chaque c�t�
    //         {
    //             // 3. Raycast pour v�rifier les obstacles
    //             RaycastHit hit;
    //             //Debug.DrawLine(transform.forward, directionToTarget, Color.red);
    //             if (Physics.Raycast(transform.position, directionToTarget, out hit, detectionRange))
    //             {
    //                 // 4. V�rifiez si le raycast a touch� le joueur sans obstacle entre les deux
    //                 if (hit.transform == target)
    //                 {
    //                     return true;
    //                     
    //                 }
    //             }
    //         }
    //     }
    //
    //     return false;
    // }
}
