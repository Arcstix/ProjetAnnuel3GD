using UnityEngine;
using UnityEngine.Events;
//ce script sert à répertorier les fonctions qui affiche/supprime l'ui 
public class OnBoardingUI : MonoBehaviour
{
    [SerializeField] private GameObject UIDeplacementSol;
    [SerializeField] private GameObject UIJump;
    [SerializeField] private GameObject UIRecall;
    [SerializeField] private GameObject UIInputLancerEtTransportation;

    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private float timer = 0f;

    void Start()
    {
       
    }
    
    public void HideUI()
    {
        
    }

    #region Déplacement (joueur et caméra)
    public void EnableUIDeplacementSol()
    {
        UIDeplacementSol.SetActive(true);
    }
    public void DisableUIDeplacementSol()
    {
        UIDeplacementSol.SetActive(false);
    }
    #endregion
    
    #region Saut (et double saut) 
    public void EnableUIJump()
    {
        UIJump.SetActive(true);
    }
    public void DisableUIJump()
    {
        UIJump.SetActive(false);
    }
    #endregion
    
    #region Lancer et Rappel

    

    #endregion
    
    #region Lancer et Transportation 

    

    #endregion
    
    #region SlowTime

    

    #endregion
    
    #region Aggriper et repousser projectile

    

    #endregion
    
    #region FixeWall 

    

    #endregion
    
    #region Air Controle

    

    #endregion
    
    #region Anchor

    

    #endregion
    
    #region CheckPoint

    

    #endregion
    
    #region Platform

    

    #endregion
    
    #region WallRun

    

    #endregion
    
    #region Springboard

    

    #endregion
    
    #region WindTunnel

    

    #endregion
}
