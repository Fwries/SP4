using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class targetBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Transform myTransform;
    float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        agent = animator.GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myTransform = animator.GetComponent<Transform>();
        agent.speed = animator.GetFloat("Speed");
        agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetInteger("Health") < animator.GetInteger("PrevHealth"))
        {
            animator.SetBool("IsHit", true);
        }
        agent.isStopped = !myTransform.parent.parent.GetComponent<RoomPrescence>().shouldTargetPlayer;
        timer += Time.deltaTime;
        agent.SetDestination(player.position);
        myTransform.LookAt(player.position);
        if (timer > Random.Range(3f, 6f))
        {
            animator.SetBool("isTargetting", false);
        }

        //Update the animation vars
        animator.SetFloat("MoveX", (player.position - myTransform.position).normalized.x);
        animator.SetFloat("distance", (player.position - myTransform.position).magnitude * 2);

        //Check if should attack
        if ((player.position - myTransform.position).magnitude * 2 < animator.GetFloat("Range"))
        {
            animator.SetBool("attack", true);
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
