using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dieBehaviour : StateMachineBehaviour
{
    float timer;
    NavMeshAgent agent;
    public Transform coinPos;
    private Transform myTransform;
    private Vector3 coinOff = new Vector3(0f, 0.3f, 0f);

    public GameObject portal;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        agent = animator.GetComponentInParent<NavMeshAgent>();
        myTransform = animator.GetComponent<Transform>();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            Transform coin = Instantiate(coinPos, myTransform.position + coinOff, Quaternion.identity);
            coin.GetComponent<coinBehaviour>().SetUp(Random.Range(1, animator.GetInteger("CoinAmount")));

            RoomPrescence roomPrescence = animator.GetComponentInParent<RoomPrescence>();
            roomPrescence.EnemyDestroyed();
            if (portal != null)
            {
                portal = Instantiate(portal, myTransform.position, Quaternion.identity,this.myTransform.parent.parent);
            }

            Destroy(animator.GetComponent<Transform>().parent.gameObject);
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
