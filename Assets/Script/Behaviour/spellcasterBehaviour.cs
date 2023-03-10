using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class spellcasterBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    float timer;
    Transform player;
    Transform myTransform;

    [SerializeField] private Transform magicBall;
    [SerializeField] private AudioClip projectileSound;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponentInParent<NavMeshAgent>();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        List<GameObject> playerList = new List<GameObject>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            playerList.Add(player);
        }
        player = playerList[Random.Range(0, playerList.Count)].transform;
        myTransform = animator.GetComponent<Transform>();
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        animator.SetFloat("distance", (player.position - myTransform.position).magnitude * 2);

        if (timer > animator.GetFloat("AttackSpeed") && PhotonNetwork.IsMasterClient)
        {
            Vector3 offset = new Vector3(0f, -0.1f, -0.5f);
            Transform ballTransform = PhotonNetwork.Instantiate("MagicBall", myTransform.position - offset, Quaternion.identity).transform;
            Vector3 shootdir = (player.position - (myTransform.position - offset));
            shootdir.y = 0f;
            shootdir.Normalize();
            ballTransform.GetComponent<MagicBallBehaviour>().SetUp(shootdir, animator.GetInteger("Damage"), animator.GetFloat("ProjSpeed"));
            SoundManager.Instance.PlaySound(projectileSound);
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
