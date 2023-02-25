using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class attackBehaviour : StateMachineBehaviour
{
    public GameObject slamParticles;
    NavMeshAgent agent;
    float timer;
    Transform player;
    Transform myTransform;
    private PhotonView photonView;

    [SerializeField] private AudioClip slamEffect;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponentInParent<NavMeshAgent>();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        myTransform = animator.GetComponent<Transform>();
        photonView = animator.GetComponentInParent<PhotonView>();
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (animator.GetInteger("Health") < animator.GetInteger("PrevHealth"))
        {
            animator.SetBool("IsHit", true);
        }

        animator.SetFloat("distance", (player.position - myTransform.position).magnitude * 2);

        if (timer > animator.GetFloat("AttackSpeed"))
        {
            SpriteRenderer spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
            // Change the sprite color to the tint color
            spriteRenderer.color = Color.red;

            if (slamParticles != null && slamEffect != null)
            {
                Instantiate(slamParticles, player.position, Quaternion.identity);
                SoundManager.Instance.PlaySound(slamEffect);
            }
            if (player.GetComponent<PhotonView>().IsMine)
            {
                player.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, animator.GetInteger("Damage"));
            }
            timer = 0;
        }

        if (animator.GetFloat("distance") > animator.GetFloat("Range"))
        {
            animator.SetBool("attack", false);
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
