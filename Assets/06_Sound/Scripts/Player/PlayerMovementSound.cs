using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
    [SerializeField] private float footstepInterval = 0.35f;
    [SerializeField] private int level1WallRunAccord = 2;
    [SerializeField] private int level2WallRunAccord = 3;
    [SerializeField] private int level3WallRunAccord = 4;
    public KeyCode pressToPlaySonAccordWallRunSound;
    
	private PlayerMovementManager movementManager;
    private GroundCheck groundCheck;
    private EventInstance SonAccordWallrun;
    private int counter = 0;
    private PlayerReusableStateData reusableData;
	
	private void Awake()
	{
		movementManager = GetComponent<PlayerMovementManager>();
		movementManager.OnMovementStarted += InitMovement;
        groundCheck = GetComponent<GroundCheck>();
	}

    private void Start()
    {
        SonAccordWallrun = RuntimeManager.CreateInstance("event:/Musique/Accords wall run");
    }

    private void InitMovement()
    {
        movementManager.StateMachine.JumpState.OnJump += Jump;
        movementManager.StateMachine.JumpState.OnDoubleJump += DoubleJump;
        movementManager.StateMachine.RunningState.OnRunning += StartRunning;
        movementManager.StateMachine.RunningState.OnStopRunning += StopRunning;
        movementManager.StateMachine.WallRunState.OnWallRun += StartWallRunning;
        movementManager.StateMachine.WallRunState.EnterWallRun += ConsecutiveWallRun;
        movementManager.StateMachine.WallRunState.ExitWallRun += StopWallRun;
        movementManager.StateMachine.SoftLandingState.OnSoftLanding += SoftLanding;
        movementManager.StateMachine.HardLandingState.OnHardLanding += HardLanding;
        movementManager.StateMachine.SoftLandingState.OnNoStaminaLanding += NoStaminaLanding;
        movementManager.StateMachine.HardLandingState.OnNoStaminaLanding += NoStaminaLanding;
        reusableData = movementManager.ReusableData;
    }

    private void OnDisable()
	{
		movementManager.StateMachine.JumpState.OnJump -= Jump;
        movementManager.StateMachine.JumpState.OnDoubleJump -= DoubleJump;
        movementManager.StateMachine.RunningState.OnRunning -= StartRunning;
        movementManager.StateMachine.RunningState.OnStopRunning -= StopRunning;
        movementManager.StateMachine.WallRunState.OnWallRun -= StartWallRunning;
        movementManager.StateMachine.WallRunState.EnterWallRun -= ConsecutiveWallRun;
        movementManager.StateMachine.WallRunState.ExitWallRun -= StopWallRun;
        movementManager.StateMachine.SoftLandingState.OnSoftLanding -= SoftLanding;
        movementManager.StateMachine.HardLandingState.OnHardLanding -= HardLanding;
        movementManager.StateMachine.SoftLandingState.OnNoStaminaLanding -= NoStaminaLanding;
        movementManager.StateMachine.HardLandingState.OnNoStaminaLanding -= NoStaminaLanding;
	}

    private void Update()
    {
        if (Input.GetKeyDown(pressToPlaySonAccordWallRunSound))
        {
            if (counter == 0)
            {
                Level1WallRun();
            }

            if (counter == 1)
            {
                Level2WallRun();
            }

            if (counter == 2)
            {
                Level3WallRun();
            }
            counter++;
            if (counter == 3)
            {
                counter = 0;
            }
        }
    }

    public void Jump()
	{
		// Son qui s'active lorsque le joueur saute.
		RuntimeManager.PlayOneShot("event:/Player sounds/Jump");
	}

	public void DoubleJump()
	{
		// Son qui s'active lorsque le joueur utilise son deuxieme saut. 
		RuntimeManager.PlayOneShot("event:/Player sounds/Double jump");
	}
	
	private void StopRunning()
        {
            CancelInvoke();
        }
    
        private void StopWallRun()
        {
            CancelInvoke();
        }
    
        private void StartRunning()
        {
            if (groundCheck)
            {
                InvokeRepeating(nameof(PlayRunningSound), 0, footstepInterval);
            }
        }
    
        private void StartWallRunning(float value)
        {
            InvokeRepeating(nameof(WallRun), 0, footstepInterval);
        }
    
        private void PlayRunningSound()
        {
            switch (groundCheck.DetectSurface())
            {
                case GroundTypeEnum.None:
                    RockFootstep();
                    break;
                case GroundTypeEnum.Rock:
                    RockFootstep();
                    break;
                case GroundTypeEnum.Sable:
                    SableFootstep();
                    break;
            }
        }
    
        public void WallRun()
        {
            // Son qui s'active lorsque le joueur se déplace sur un wall run (est en état wall run) 
            // Fonctionnement comme la marche  
            RuntimeManager.PlayOneShot("event:/Player sounds/Wall Run");
        }
        
        private void ConsecutiveWallRun(int numberOfWallRun)
        {
            if (numberOfWallRun >= level1WallRunAccord && numberOfWallRun < level2WallRunAccord)
            {
                Level1WallRun();
            }

            if (numberOfWallRun >= level2WallRunAccord && numberOfWallRun < level3WallRunAccord)
            {
                Level2WallRun();
            }

            if (numberOfWallRun >= level3WallRunAccord)
            {
                Level3WallRun();
            }
        }
        
        private void Level1WallRun()
        {
            SonAccordWallrun.setParameterByName("NB wallrun", 0f); // lancer ca quand on a fait 2 wallruns sans toucher le sol
            SonAccordWallrun.start();
        }
    
        private void Level2WallRun()
        {
            SonAccordWallrun.setParameterByName("NB wallrun", 0.5f); // lancer ca quand on a fait 3  Wallruns sans toucher le sol
            SonAccordWallrun.start();
        }
    
        private void Level3WallRun()
        {
            SonAccordWallrun.setParameterByName("NB wallrun", 1f); // lancer ca quand on a fait 4 wallruns sans toucher le sol
            SonAccordWallrun.start();
        }

    
        public void SoftLanding()
        {
            // Son qui s'active lorsque le joueur collisionne le sol
            RuntimeManager.PlayOneShot("event:/Player sounds/player collision sol faible");
        }
    
        public void HardLanding()
        {
            // !!!!!!! ON VERRA !!!!!!!!! Son qui s'active lorsque le joueur collisionne le sol avec une vitesse et hauteur importante 
            RuntimeManager.PlayOneShot("event:/Player sounds/player collision sol fort");
        }
    
        public void NoStaminaLanding()
        {
            // !!!! PEUT ETRE LONG !!!!!!!!! Son qui s'active lorsque le joueur atterri sur un sol avec ses deux charges vides 
            RuntimeManager.PlayOneShot("event:/Player sounds/Essouflement");
        }
        
        public void RockFootstep()
        {
            // !!!!!!!! ON VERRA !!!!!!!! Son qui s'active lorsque le joueur marche au sol dans la zone grotte 
            RuntimeManager.PlayOneShot("event:/Player sounds/Marche roche");
        }

        public void SableFootstep()
        {
            // Son qui s'active lorsque le joueur marche au sol ... 
            // EN GROS il faudrait que tu crées une variable walkSpeed qui puisse se modifier (0.5 par exemple), et cette fonction BruitPasSable
            // est appelée (tous les 0.5 secondes dans ce cas) quand le joueur est en etat "walk", pour simuler des bruits de marche
            RuntimeManager.PlayOneShot("event:/Player sounds/Marche sable");
        }

}
