using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHit : MonoBehaviour
{


    public float timer;
    public GameObject damageText;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public void Start()
    {
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.05f && transform.GetComponentInChildren<Animator>().GetBool("IsHit"))
        {
            var go = Instantiate(damageText, transform.position, Quaternion.identity, transform);
            //indicator.SetDamageText(transform.GetComponentInChildren<Animator>().GetInteger("PrevHealth") - transform.GetComponentInChildren<Animator>().GetInteger("Health"));
            go.GetComponent<TextMesh>().text = (transform.GetComponentInChildren<Animator>().GetInteger("PrevHealth") - transform.GetComponentInChildren<Animator>().GetInteger("Health")).ToString();

            transform.GetComponentInChildren<Animator>().SetBool("IsHit", false);
            transform.GetComponentInChildren<Animator>().SetInteger("PrevHealth", transform.GetComponentInChildren<Animator>().GetInteger("Health"));
            timer = 0.0f;
        }
    }
}
