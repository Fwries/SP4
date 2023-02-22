using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int index;
    public int Crit = 1;
    public int Damage;
    public bool active;

    void OnTriggerEnter(Collider other)
    {
        if (!active) { return; }
        if (other.gameObject.tag == "enemy")
        {
            Animator otherAnim = other.gameObject.GetComponent<Animator>();
            if (!otherAnim.GetBool("IsHit"))
            {
                otherAnim.SetBool("IsHit", true);
                int Health = otherAnim.GetInteger("Health");

                otherAnim.SetInteger("PrevHealth", otherAnim.GetInteger("Health"));
                otherAnim.SetInteger("Health", Health - (Damage * Crit));

                this.gameObject.GetComponentInParent<HitboxContainer>().OnHit(index);
            }
        }
        if(other.gameObject.tag=="BreakWall")
        {
            Destroy(other.gameObject);
            this.gameObject.GetComponentInParent<HitboxContainer>().OnHit(index);
            transform.gameObject.GetComponentInParent<HitboxContainer>().DestroyWeapon();
        }
    }
}
