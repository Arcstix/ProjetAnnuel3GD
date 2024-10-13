using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO PlayerSO { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    public Camera Camera { get; private set; }

    public PlayerInput Input { get; private set; }

    public PlayerCameraManager playerCameraManager { get; private set; }

    public PlayerReusableStateData ReusableData { get; set; }

    protected virtual void Awake()
    {
        ReusableData = new PlayerReusableStateData();

        Input = GetComponent<PlayerInput>();

        Camera = Camera.main;

        Rigidbody = GetComponent<Rigidbody>();

        playerCameraManager = GetComponent<PlayerCameraManager>();
    }
}
