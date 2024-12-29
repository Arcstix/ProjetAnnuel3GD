using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleEntity : MonoBehaviour
{
    public void DestructionEvent()
    {
        Destroy(gameObject);
    }
}
