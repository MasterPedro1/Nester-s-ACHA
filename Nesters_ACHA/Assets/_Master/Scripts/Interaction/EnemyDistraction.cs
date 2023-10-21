using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDistraction : MonoBehaviour
{

    public GameObject[] enemies;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPunch"))
        {
           foreach (var enemy in enemies) 
                 {
                     enemy.GetComponent<EnemyStatesManager>().GoToSound(this.transform);
                 }

        }
    }
}
