using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimation : MonoBehaviour {

    public Animator anim;

    public int idleState = 0;
    public int runState = 1;
    public int attackState = 2;

    public int currentState;
    private float timer = 0f;
    private bool readyToAnimate;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > anim.GetCurrentAnimatorClipInfo(0)[currentState].clip.length)
        {
            readyToAnimate = true;
        }
    }

    public void Idle()
    {
        currentState = anim.GetInteger("state");

        if (currentState != idleState)
        { 
            anim.SetInteger("state", idleState);
            Reset();
        }
    }

    public void Run()
    {
        currentState = anim.GetInteger("state");

        if (currentState != runState)
        {
            anim.SetInteger("state", runState);
            Reset();
        }
    }

    public void Attack()
    {
        currentState = anim.GetInteger("state");

        if (currentState != attackState)
        { 
            anim.SetInteger("state", attackState);
            Reset();
        }
    }

    private void Reset()
    {
        timer = 0;
        readyToAnimate = false;
    }



}
