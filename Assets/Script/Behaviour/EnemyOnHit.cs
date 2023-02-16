using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHit : MonoBehaviour
{


    public float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public void Start()
    {
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            transform.GetComponentInChildren<Animator>().SetBool("IsHit", false);
            transform.GetComponentInChildren<Animator>().SetInteger("PrevHealth", transform.GetComponentInChildren<Animator>().GetInteger("Health"));
            timer = 0.0f;
        }
    }
}
