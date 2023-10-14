using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Doors : ObjectInteraction
{
    public UnityEvent onUse;
    public bool isOpen;
    [SerializeField] private string _openTag, _closedTag;

   /* private void Start()
    {
        if (!isOpen) transform.gameObject.tag = _closedTag;
        else transform.gameObject.tag = _openTag;
    }*/

    public override void Use()
    {
        base.Use();
        onUse.Invoke();
    }

    public void SetOpenDoor()
    {
        transform.gameObject.tag = _openTag;
    }
}
