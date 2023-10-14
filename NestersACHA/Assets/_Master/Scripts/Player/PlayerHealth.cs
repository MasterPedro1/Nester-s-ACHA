using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _maxHealth=100;
    [SerializeField] float _damagePerHit=20;
    public float currentHealth;
    public bool isHit = false;
   // [Header("Effect")]
  //  public CameraShake cameraShake;
   
    private void Start()
    {
        FillHealth();
    }
    
    public void FillHealth()
    {
        currentHealth = _maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            Debug.Log("hit");
            isHit = true;
            currentHealth -= _damagePerHit;
         //   cameraShake.Shake();
        }
    }
}
