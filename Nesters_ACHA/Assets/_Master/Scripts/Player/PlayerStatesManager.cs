using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatesManager : MonoBehaviour
{
    [Header ("Scripts References")]
    public PlayerMovement playerMovement;
    public PlayerAnimations playerAnimations;
    public PlayerInteractions playerInteractions;
    public PlayerHealth playerHealth;

    [Header("Time Values")]
    [SerializeField] private float _attackTime;
    [SerializeField] private float _executeTime=1.55f;
    [SerializeField] private float _GethitTime=0.5f;

   public enum PlayerState
    {
        Idle, CrouchIdle, Walking,BackWalking, CrouchWalking, CrouchWalkBack, Running, Attack, Execution, Death, GetHit
    }

    public PlayerState currentState;

    private void Start()
    {
        currentState = PlayerState.Idle;
    }

    private Dictionary<string, KeyCode> keyMap = new Dictionary<string, KeyCode>()
        {
            { "Walking", KeyCode.W },
            { "BackWalk", KeyCode.S },
            {"Crouch", KeyCode.C },
            { "Run", KeyCode.LeftShift },
            { "LightAttack", KeyCode.Mouse0 },
            { "HeavyAttack", KeyCode.Mouse1 },
        };


    private void Update()
    {
        GetAKey("LightAttack", PlayerState.Attack);
        if (playerInteractions.currentKillFill>=95)
        {
            UpKey("HeavyAttack", PlayerState.Execution);
        }
        if (playerHealth.currentHealth<=0)
        {
            currentState = PlayerState.Death;
        }
        else if (playerHealth.isHit==true)
        {
            currentState = PlayerState.GetHit;
        }

        switch (currentState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.CrouchIdle:

                CrouchIdle();
                break;
            case PlayerState.Walking:
                Walking();

                break; 
            case PlayerState.BackWalking:

                BackWalking();

                break;
            case PlayerState.CrouchWalking:
                CrouchWalking();
                break;

            case PlayerState.CrouchWalkBack:
                CrouchBackWalking();
                break;
                 
            case PlayerState.Running:
                Running();
                break; 
            
            case PlayerState.Attack:
                Attack();
                break;
            
            case PlayerState.Execution:
                OnExecute();
                break;

            case PlayerState.Death:
                OnDeath();
                break;

            case PlayerState.GetHit:
                OnGetHit();
                break;
            default:
                break;
        }
    }

    void GetAKey(string key, PlayerState state)
    {
        if (keyMap.ContainsKey(key) && Input.GetKeyDown(keyMap[key]))
        {
            currentState = state;
        }
    }
    void GetKeepKey(string key, PlayerState state)
    {
        if (keyMap.ContainsKey(key) && Input.GetKey(keyMap[key]))
        {
            currentState = state;
        }
    }
    void UpKey(string key, PlayerState state)
    {
        if (keyMap.ContainsKey(key) && Input.GetKeyUp(keyMap[key]))
        {
            currentState = state;
        }
    }

    void Idle()
    {
       
        playerMovement.currentSpeed = playerMovement.walkSpeed;
        playerMovement.currentRotationSpeed = playerMovement.standRotationSpeed;
        playerAnimations.SetAnimation(playerAnimations.idleAnim);

        GetKeepKey("BackWalk", PlayerState.BackWalking);
        GetAKey("Crouch", PlayerState.CrouchIdle);
        GetKeepKey("Walking", PlayerState.Walking);
    }

    void CrouchIdle()
    {

        playerMovement.currentSpeed = playerMovement.crouchSpeed;
        playerMovement.currentRotationSpeed = playerMovement.standRotationSpeed;

        playerAnimations.SetAnimation(playerAnimations.crouchAnim);

        GetAKey("Crouch", PlayerState.Idle);
        GetKeepKey("Walking", PlayerState.CrouchWalking);
        GetKeepKey("BackWalk", PlayerState.CrouchWalkBack);
    }

    void Walking()
    {
        playerMovement.currentSpeed = playerMovement.walkSpeed;
        playerMovement.currentRotationSpeed = playerMovement.standRotationSpeed;

        playerAnimations.SetAnimation(playerAnimations.walkAnim);

        UpKey("Walking", PlayerState.Idle);

        GetKeepKey("Run", PlayerState.Running);
        GetAKey("Crouch", PlayerState.CrouchWalking);     
    }

    void BackWalking()
    {  
        playerAnimations.SetAnimation(playerAnimations.backWalk);
        playerMovement.currentRotationSpeed = playerMovement.moveRotationSpeed;

        UpKey("BackWalk", PlayerState.Idle);

        GetAKey("Crouch", PlayerState.CrouchWalkBack);
    }

    void CrouchWalking()
    {
        playerMovement.currentRotationSpeed = playerMovement.standRotationSpeed;
        playerMovement.currentSpeed = playerMovement.crouchSpeed;
        playerAnimations.SetAnimation(playerAnimations.crouchWalkAnim);

        UpKey("Walking", PlayerState.CrouchIdle);

        GetAKey("Run", PlayerState.Running);
        GetAKey("Walking", PlayerState.Walking);
        GetAKey("Crouch", PlayerState.Walking);
    }

    void CrouchBackWalking()
    {
        playerMovement.currentSpeed = playerMovement.walkSpeed;
        playerMovement.currentRotationSpeed = playerMovement.standRotationSpeed;
        playerAnimations.SetAnimation(playerAnimations.crouchWalkBack);

        UpKey("BackWalk", PlayerState.CrouchIdle);
        GetAKey("Crouch", PlayerState.BackWalking);
    }

    void Running()
    {
        playerAnimations.SetAnimation(playerAnimations.runAnim);
        playerMovement.currentSpeed = playerMovement.runningSpeed;
        playerMovement.currentRotationSpeed = playerMovement.moveRotationSpeed;

        UpKey("Walking", PlayerState.Idle);
        UpKey("Run", PlayerState.Walking);

        GetAKey("Crouch", PlayerState.CrouchWalking);
    }

    void Attack()
    {
        playerAnimations.SetAnimation(playerAnimations.lightAttackAnim);
        playerMovement.currentSpeed = 0;
        playerMovement.currentRotationSpeed = 0;

        StartCoroutine(WaitToState(PlayerState.Idle, _attackTime));
    }

    void OnExecute()
    {
        playerAnimations.SetAnimation(playerAnimations.killAnim);
        playerMovement.currentSpeed = 0;
        playerMovement.currentRotationSpeed = 0;

        StartCoroutine(WaitToState(PlayerState.Idle, _executeTime));
    }

    void OnDeath()
    {
        playerAnimations.SetAnimation(playerAnimations.deathAnim);
        playerAnimations.RootMotion();
        playerMovement.Death();
        playerMovement.enabled = false;
    }

    void OnGetHit()
    {
        playerHealth.isHit = false;

        playerAnimations.SetAnimation(playerAnimations.getHitAnim);
        playerAnimations.RootMotion();

        playerMovement.currentSpeed = 0;
        playerMovement.currentRotationSpeed = 0;

        StartCoroutine(WaitToState(PlayerState.Idle, _GethitTime));
    }

    public IEnumerator WaitToState(PlayerState state, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        currentState = state;
        playerAnimations.NoRootMotion();
        StopAllCoroutines();
    }
}
