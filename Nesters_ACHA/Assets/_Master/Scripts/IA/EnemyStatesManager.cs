using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStatesManager : MonoBehaviour
{
    [SerializeField] private float _distanceToAttack = 1;
    [SerializeField] private bool _isListening=true;

    [Header("Time values")]
    [SerializeField] private float _waitingTime = 8f;
    [SerializeField] private float _searchingTime = 10f;
    [SerializeField] private float _stunTime = 3f;
    [SerializeField] private float _pursuitTime = 3f;
    [SerializeField] private float _attackTime = 2f;

    [Header("Scripts References")]
    public EnemyAnimations enemyAnimations;
    public EnemyMovement enemyMovement;
    public EnemyGetDamage enemyGetDamage;
    public Detection detection;
    

    public enum EnemyState
    {
        Idle, Wander, FollowPlayer, Pursuit, Searching, Death, Stun, Attack
    }

    public EnemyState currentState;

    private void Start()
    {
        currentState = EnemyState.Idle;
    }

    private void Update()
    {
       
        if (enemyGetDamage.isStun == true) currentState = EnemyState.Stun;
        if (enemyGetDamage.isDeath == true) currentState = EnemyState.Death;
        
        switch (currentState)
        {
            case EnemyState.Idle:
                OnIdle();
                break;

            case EnemyState.Wander:
                OnWander();
                break;

            case EnemyState.FollowPlayer:
                OnFollowPlayer();
                break;

            case EnemyState.Pursuit:
                OnPursuit();
                break;

            case EnemyState.Searching:
                OnSearching();
                break;
            
            case EnemyState.Stun:
                OnStunning();
                break;

            case EnemyState.Death:
                OnDeath();
                break;
            
            case EnemyState.Attack:
                OnAttack();
                break;
            
        }
    }
    void OnIdle()
    {
        GetPlayerDetection();
        enemyAnimations.SetAnimation(enemyAnimations.idleAnim);
        enemyMovement.Idle();
        StartCoroutine(WaitToState(EnemyState.Wander, _waitingTime, enemyMovement.WanderDestination()));

    }

    void OnWander()
    {
        GetPlayerDetection();
        enemyAnimations.SetAnimation(enemyAnimations.walkAnim);
        enemyMovement.SetDestination(enemyMovement.walkSpeed, enemyMovement.walkAcceleration);
        if (Vector3.Distance(transform.position, enemyMovement.currentDestination.position) <= enemyMovement.distanceToDestination)
        {
            ChangeState(EnemyState.Idle);
        }
    }

    void OnFollowPlayer()
    {
        enemyMovement.currentDestination = detection.player;
        enemyAnimations.SetAnimation(enemyAnimations.walkAnim);
        enemyMovement.SetDestination(enemyMovement.walkSpeed, enemyMovement.walkAcceleration);

        _isListening = false;    

        if (Vector3.Distance(transform.position, enemyMovement.currentDestination.position) >= detection.visionDistance)
        {
            detection.isPlayerDetected = false;
            detection.lastPlayerPosition = detection.player;
            ChangeState(EnemyState.Pursuit);
        }
        if (Vector3.Distance(transform.position, enemyMovement.currentDestination.position) <=_distanceToAttack)
        {
            enemyMovement.Idle();
            ChangeState(EnemyState.Attack);
        }
        
        Debug.Log(Vector3.Distance(transform.position, enemyMovement.currentDestination.position));
    }

    void OnPursuit()
    {

        enemyAnimations.SetAnimation(enemyAnimations.runAnim);
        enemyMovement.SetDestination(enemyMovement.pursuitSpeed, enemyMovement.runAcceleration);
        StartCoroutine(WaitToState(EnemyState.Searching,_pursuitTime,transform));

          if (Vector3.Distance(transform.position, enemyMovement.currentDestination.position) <=_distanceToAttack)
        {
            enemyMovement.Idle();
            ChangeState(EnemyState.Attack);
        }
    }

    void OnSearching()
    {
        _isListening = true;
        GetPlayerDetection();
        enemyMovement.Idle();
        enemyAnimations.SetAnimation(enemyAnimations.search);

        StartCoroutine(WaitToState(EnemyState.Wander, _searchingTime, enemyMovement.WanderDestination()));
    }

    void OnStunning()
    {
        _isListening = false;
        enemyGetDamage.isStun = false;
        enemyMovement.Idle();
        enemyAnimations.SetAnimation(enemyAnimations.stunAnim);

        StartCoroutine(WaitToState(EnemyState.Searching,_stunTime, enemyMovement.WanderDestination()));
    }

    void OnDeath()
    {
        enemyMovement.Death();
        enemyAnimations.SetAnimation(enemyAnimations.deathAnim);
        this.enabled = false;
        detection.enabled = false;
        CapsuleCollider cC = GetComponent<CapsuleCollider>();
        Rigidbody rb = GetComponent<Rigidbody>();
        cC.enabled = false;
        Destroy(rb);
    }

    void OnAttack()
    {
        enemyMovement.SetDestination(0,0);
        enemyAnimations.SetAnimation(enemyAnimations.attackAnim);

        if (Vector3.Distance(transform.position, enemyMovement.currentDestination.position) > _distanceToAttack*1.5f)
        {
            ChangeState(EnemyState.FollowPlayer);
        }
    }
    public IEnumerator WaitToState(EnemyState state, float time, Transform destination)
    {
        yield return new WaitForSecondsRealtime(time);
      //  currentState = state;
        ChangeState(state);
      enemyMovement.currentDestination= destination;
        StopAllCoroutines();
    }



    void ChangeState(EnemyState state)
    {
        currentState = state;
    }

    void GetPlayerDetection()
    {
        if (detection.isPlayerDetected == true) currentState = EnemyState.FollowPlayer;
    }


     public void GoToSound(Transform location)
    {
        enemyMovement.currentDestination = location;
        ChangeState(EnemyState.Pursuit);
    }


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Noise")&& _isListening)
        {
            ChangeState(EnemyState.FollowPlayer);
        }
    }

}