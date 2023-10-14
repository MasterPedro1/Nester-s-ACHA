using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _anim;
    public string idleAnim, walkAnim, runAnim, lightAttackAnim, killAnim, getHitAnim,deathAnim, crouchAnim, crouchWalkAnim, crouchWalkBack,backWalk, GetHit;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public void SetAnimation(string animation)
    {

       // _anim.StopPlayback();
        _anim.Play(animation);
        
    }

    public void RootMotion()
    {
        _anim.applyRootMotion = true;
    }
    public void NoRootMotion()
    {
        _anim.applyRootMotion = false;
    }
}
