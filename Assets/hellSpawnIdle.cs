using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hellSpawnIdle : StateMachineBehaviour
{
    NavMeshAgent agent;
    float timer;
    Transform player;
    Transform myTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponentInParent<NavMeshAgent>();
        agent.velocity = Vector3.zero;
        agent.isStopped = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        myTransform = animator.GetComponent<Transform>();
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        animator.SetFloat("distance", (player.position - myTransform.position).magnitude * 2);
        float pDistance = animator.GetFloat("distance");

        if (myTransform.parent.parent.GetComponent<RoomPrescence>().shouldTargetPlayer)
        {
            agent.SetDestination(player.position);
        }

        //Attacks for hellspawn boss
        if (timer > 3.0f)
        {
            int attackToDo = Random.Range(0, 2);
            if (attackToDo == 0)
            {
                animator.SetInteger("AttackDecider", 1);
            }
            else
            {
                animator.SetInteger("AttackDecider", 1);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
