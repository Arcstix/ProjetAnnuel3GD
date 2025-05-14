using FMODUnity;
using UnityEngine;

public class PlayerStateSound : MonoBehaviour
{
    /// Que des sons liés à l'état du joueur en dehors du mouvement et de l'abilité ///
    
    private PlayerMetricsManager metricsManager;
    private HealthPlayer playerHealth;
    
    private void Awake()
    {
        metricsManager = GetComponent<PlayerMetricsManager>();
        playerHealth = GetComponent<HealthPlayer>();
    }

    private void Start()
    {
        metricsManager.OnRightChargeReady += RightChargeReady;
        metricsManager.OnLeftChargeReady += LeftChargeReady;
        metricsManager.OnNoCharge += NoCharge;
        playerHealth.OnDanger += DangerSound;
    }

    private void OnDisable()
    {
        metricsManager.OnRightChargeReady -= RightChargeReady;
        metricsManager.OnLeftChargeReady -= LeftChargeReady;
        metricsManager.OnNoCharge -= NoCharge;
        playerHealth.OnDanger -= DangerSound;
    }
    
    public void RightChargeReady()
    {
        // Son qui s'active lorsque la charge de stamina droite est pleine. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready D");
    }

    public void LeftChargeReady()
    {
        // Son qui s'active lorsque la charge de stamina gauche est pleine. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready G");
    }

    public void NoCharge()
    {
        // Son qui s'active lorsque le joueur veut s'attirer à un outil mais qu'il n'a pas encore récupéré sa stamina. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/No Stamina");
    }
    
    public void DangerSound()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!! Son qui s'active lorsque la barre de vie du joueur ( la jauge de vision ennemis ) atteint 30%
        Debug.Log("Danger !!!!");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/ah (degats)");
    }
    
    public void AccordEnvole()
    {
        RuntimeManager.PlayOneShot("event:/Musique/Accords envole"); // lorse que le joueur fait une gagne beaucoup de hauteur
    }
}
