using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator _anim;
    public Animator anim;
    public string idleAnim, walkAnim, runAnim,attackAnim,stunAnim, deathAnim, getHit, search;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public void SetAnimation(string animation)
    {

        // _anim.StopPlayback();
       // _anim.Play(animation);
 
        anim.Play(animation);

    }
}
