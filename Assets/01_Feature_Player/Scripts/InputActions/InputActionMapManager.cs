using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputActionMapManager : MonoBehaviour
{
    private PlayerInput playerInput;

    [Header("Optional Events")]
    public UnityEvent onSwitchToPlayer;
    public UnityEvent onSwitchToUI;

    private void Awake()
    {
        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();
    }

    private void SwitchToActionMap(string mapName)
    {
        if (playerInput.currentActionMap.name == mapName)
            return;

        playerInput.SwitchCurrentActionMap(mapName);

        // Trigger optional events
        if (mapName == "Player") onSwitchToPlayer?.Invoke();
        else if (mapName == "UI") onSwitchToUI?.Invoke();
    }

    public void SwitchToPlayer() => SwitchToActionMap("Player");

    public void SwitchToUI() => SwitchToActionMap("UI");

    public string GetCurrentActionMapName() => playerInput.currentActionMap.name;

    public bool IsInActionMap(string mapName) => playerInput.currentActionMap.name == mapName;
}
