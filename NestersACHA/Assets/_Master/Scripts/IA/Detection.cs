using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public bool isPlayerDetected = false;

    public float visionDistance = 10f;
    [SerializeField] private float _visionAngle = 60f;

    public Transform player;
    public Transform lastPlayerPosition; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerView").transform;
    }

    private void Update()
    {
        // Dirección del jugador desde el enemigo
        Vector3 directionToPlayer = player.position - transform.position;
       

        // Ángulo entre la dirección del enemigo y el jugador
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
       
        // Si el jugador está dentro del campo de visión y a una distancia menor a visionDistance
        if (angleToPlayer < _visionAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {    
                   // Debug.Log("¡Jugador detectado!");
                    isPlayerDetected=true;
                }else
                {
                  //  Debug.Log("No hay jugador");
                    isPlayerDetected = false;
                }
            }

          
            Debug.DrawRay(transform.position, directionToPlayer.normalized * visionDistance, Color.red);
           
        }
    }


}
