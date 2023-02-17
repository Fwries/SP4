using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dieBehaviour : StateMachineBehaviour
{
<<<<<<< Updated upstream:Assets/dieBehaviour.cs
    float timer;
    NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        agent = animator.GetComponentInParent<NavMeshAgent>();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
=======
    NavMeshAgent agent;
    float timer;
    float bulletReset;
    Transform player;
    public Transform myTransform;


    public float spacing = 1.0f;

    public int numProjectiles2;
    public float fireRate = 0.2f;
    private float nextFireTime;

    [SerializeField] private Transform fireBall;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponentInParent<NavMeshAgent>();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        myTransform = animator.GetComponent<Transform>();
        timer = 0;
        bulletReset = 0;

        nextFireTime = Time.time;
        numProjectiles2 = 10;
>>>>>>> Stashed changes:Assets/hellSpawnAttack.cs
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
<<<<<<< Updated upstream:Assets/dieBehaviour.cs
        if (timer > 1f)
        {
            Destroy(animator.gameObject);
=======
        bulletReset += Time.deltaTime;
        animator.SetFloat("distance", (player.position - myTransform.position).magnitude * 2);
        float pDistance = animator.GetFloat("distance");

        //Attacks for hellspawn boss
        if (timer > 2f && myTransform.parent.parent.GetComponent<RoomPrescence>().shouldTargetPlayer && pDistance < animator.GetFloat("Range"))
        {
            if (animator.GetInteger("AttackDecider") == 1)
            {
                Vector3 spawnPosition = myTransform.position;
                spawnPosition.y = fireBall.position.y;

                int numProjectiles = 15;
                float angleStep = 360.0f / numProjectiles;

                for (int i = 0; i < numProjectiles; i++)
                {
                    Transform projectile = Instantiate(fireBall);
                    float angle = i * angleStep;
                    Vector3 offset = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward * spacing;
                    Vector3 projectilePosition = spawnPosition + offset;

                    projectile.position = projectilePosition;
                    projectile.rotation = Quaternion.LookRotation(offset, Vector3.up);

                    Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                    projectileRb.velocity = animator.GetFloat("ProjSpeed") * offset.normalized;
                }
                animator.SetInteger("AttackDecider", 0);
            }
            else
            {
                if (Time.time >= nextFireTime && numProjectiles2 > 0)
                {
                    var offset = new Vector3(0f, -0.1f, -0.5f);
                    var proj = GameObject.Instantiate(fireBall, myTransform.position - offset, myTransform.rotation);
                    Vector3 shootdir = (player.position - (myTransform.position - offset));
                    shootdir.y = 0f;
                    shootdir.Normalize();
                    proj.GetComponent<MagicBallBehaviour>().SetUp(shootdir, animator.GetInteger("Damage"), animator.GetFloat("ProjSpeed"));
                    numProjectiles2--;
                    nextFireTime = Time.time + fireRate;
                }
                if (numProjectiles2 <= 0)
                {
                    Debug.Log("ATTACK2");
                    animator.SetInteger("AttackDecider", 0);
                }
            }
>>>>>>> Stashed changes:Assets/hellSpawnAttack.cs
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
