using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOnHit : MonoBehaviour
{


    public float timer;
    public GameObject damageText;
    public Slider bossBar;

    [SerializeField] private AudioClip hurt;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public void Start()
    {
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public void Update()
    {
        if (bossBar != null)
        {
            bossBar.value = transform.GetComponentInChildren<Animator>().GetInteger("Health");
        }

        timer += Time.deltaTime;
        if (timer > 0.02f && transform.GetComponentInChildren<Animator>().GetBool("IsHit"))
        {
            var go = Instantiate(damageText, transform.position, Quaternion.identity, transform);
            //indicator.SetDamageText(transform.GetComponentInChildren<Animator>().GetInteger("PrevHealth") - transform.GetComponentInChildren<Animator>().GetInteger("Health"));
            go.GetComponent<TextMesh>().text = (transform.GetComponentInChildren<Animator>().GetInteger("PrevHealth") - transform.GetComponentInChildren<Animator>().GetInteger("Health")).ToString();

            transform.GetComponentInChildren<Animator>().SetBool("IsHit", false);
            transform.GetComponentInChildren<Animator>().SetInteger("PrevHealth", transform.GetComponentInChildren<Animator>().GetInteger("Health"));
            SoundManager.Instance.PlaySound(hurt);
            timer = 0.0f;
        }
    }
}
