using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private List<Transform> _navigationPoints;

    public Transform currentDestination;

    [Header("Physic Values")]
    public float walkSpeed = 4;
    public float pursuitSpeed = 8;
    public float distanceToDestination = 1;
    public float walkAcceleration = 1;
    public float runAcceleration = 3;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Death()
    {
        _navMeshAgent.enabled = false;
    }

    public void Idle()
    {
        _navMeshAgent.enabled = false;
        _navMeshAgent.enabled = true;
        _navMeshAgent.speed = 0;
        _navMeshAgent.acceleration = 0;
    }
    public void SetDestination(float currentSpeed, float currentAcceleration)
    {

        _navMeshAgent.speed = currentSpeed;
        _navMeshAgent.acceleration = currentAcceleration;
        _navMeshAgent.destination = currentDestination.position;

    }

    public Transform WanderDestination()
    {
        int _randomIndex = Random.Range(0, _navigationPoints.Count);

       return _navigationPoints[_randomIndex];
    }

    public System.Action CustomFunction;

}
