using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int Damage;
    private bool OnHit;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!active) { return; }
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponentInChildren<Animator>().SetBool("IsHit", true);
            Animator otherAnim = other.gameObject.GetComponentInChildren<Animator>();
            int Health = otherAnim.GetInteger("Health");
            
            otherAnim.SetInteger("PrevHealth", otherAnim.GetInteger("Health"));
            otherAnim.SetInteger("Health", Health - Damage);
        }
    }
}
