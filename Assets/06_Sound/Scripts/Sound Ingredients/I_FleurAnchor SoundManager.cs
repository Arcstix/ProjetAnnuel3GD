using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class I_FleurAnchorSoundManager : MonoBehaviour
{
    private EventInstance flowerSoundEvent;
    
    private ProjectileInteraction projectileInteraction;

    private void Awake()
    {
        projectileInteraction = GetComponent<ProjectileInteraction>();
        flowerSoundEvent = RuntimeManager.CreateInstance("event:/Ingredients/Fleur saisie");
        RuntimeManager.AttachInstanceToGameObject(flowerSoundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    private void Start()
    {
        projectileInteraction.OnToolInteract += EnterFlowerSaisie;
        projectileInteraction.OnPlatformInteract += OpenFlower;
        projectileInteraction.OnExitToolInteract += ExitFlowerSaisie;
        projectileInteraction.OnExitToolInteract += ThrowFlower;
    }

    private void Update()
    {
        flowerSoundEvent.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
    }

    public void EnterFlowerSaisie()
    {
        flowerSoundEvent.setParameterByName("FinSaisie", 0f); // lancer ca quand on a saisi le bourgeon avec notre outil
        flowerSoundEvent.start(); // lancer ca quand on a saisi le bourgeon avec notre outil
    }

    public void ExitFlowerSaisie()
    {
        flowerSoundEvent.setParameterByName("FinSaisie", 1f); // lancer �a au moment ou on balance la fleur pour arreter le son de saisie
    }

    public void ThrowFlower()
    {
        //  Son qui s'active lorsqu'on lance le bourgeon
        RuntimeManager.PlayOneShot("event:/Ingredients/Fleur lancée");
    }

    public void OpenFlower()
    {
        //  Son qui s'active lorsque le bourgeon s'ouvre et deviens un anchor (s'accroche a un mur gris en gros)
        RuntimeManager.PlayOneShot("event:/Ingredients/Fleur ouverture");
    }
    
    public void CollisionFlower()
    {
        //  Son qui s'active lorsque le bourgeon collisionne avec un autre objet (mur, sol, etc...)
        RuntimeManager.PlayOneShot("event:/Ingredients/Objet collision");
    }

}
