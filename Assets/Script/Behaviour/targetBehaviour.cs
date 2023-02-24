using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class targetBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Transform myTransform;
    float timer;
    private PhotonView photonView;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        agent = animator.GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myTransform = animator.GetComponent<Transform>();
        agent.speed = animator.GetFloat("Speed");
        agent.isStopped = false;
        photonView = animator.GetComponentInParent<PhotonView>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetInteger("Health") < animator.GetInteger("PrevHealth"))
        {
            animator.SetBool("IsHit", true);
        }
        timer += Time.deltaTime;

        // Find the nearest player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearestPlayer = null;
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject player in players)
        {
            float distance = (player.transform.position - myTransform.position).magnitude;
            if (distance < nearestDistance)
            {
                nearestPlayer = player;
                nearestDistance = distance;
            }
        }

        if (nearestPlayer != null)
        {
            myTransform.LookAt(nearestPlayer.transform.position);
            if (timer > Random.Range(3f, 6f))
            {
                animator.SetBool("isTargetting", false);
            }

            //Update the animation vars
            animator.SetFloat("MoveX", (nearestPlayer.transform.position - myTransform.position).normalized.x);
            animator.SetFloat("distance", (nearestPlayer.transform.position - myTransform.position).magnitude * 2);

            //Check if should attack
            if ((nearestPlayer.transform.position - myTransform.position).magnitude * 2 < animator.GetFloat("Range"))
            {
                animator.SetBool("attack", true);
            }

            // Call the RPC function to set the navmesh agent's destination to the nearest player
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_SetDestination", RpcTarget.AllBuffered, nearestPlayer.transform.position);
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
    [PunRPC]
    public void RPC_SetDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }

}
