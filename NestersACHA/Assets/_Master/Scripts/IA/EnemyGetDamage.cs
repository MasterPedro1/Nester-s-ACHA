using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetDamage : MonoBehaviour
{
    public bool isStun= false;
    public bool isDeath= false;

    [SerializeField] string _punch;
    [SerializeField] string _execution;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_punch))
        {
            isStun = true;
        }
        if (other.CompareTag(_execution))
        {
            isDeath = true;
        }
    }

}
