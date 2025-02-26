using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineCapsule : MonoBehaviour
{
    [SerializeField] private CapsuleState defaultStateWaves;
    public CapsuleState currentStateWaves;


    

    // Start is called before the first frame update
    void Start()
    {
        currentStateWaves = defaultStateWaves;
        currentStateWaves.Enter(gameObject);
    }
    public void ChangeState(CapsuleState newState)
    {
        if (currentStateWaves != null)
        {
            currentStateWaves.Exit(gameObject);
        }

        currentStateWaves = newState;
        currentStateWaves.Enter(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStateWaves != null)
        {
            currentStateWaves.Tick(gameObject);
        }
    }
}
