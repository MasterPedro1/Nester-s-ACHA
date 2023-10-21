using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{

    public string tag;
    public UnityEvent onEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            onEnter.Invoke();
        }
    }
}
