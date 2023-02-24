using UnityEngine;
using Photon.Pun;

public class HitboxContainer : MonoBehaviour
{
    public Hitbox[] hitboxes;
    public ScWeapon scWeapon;

    // Update is called once per frame
    public void OnHit(int index)
    {
        for (int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i].active = false;
        }

        if (transform.parent != null)
        {
            transform.gameObject.GetComponentInParent<WeaponBehaviour>().Attack(hitboxes[index].Damage * hitboxes[index].Crit);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            Transform playerHand = player.transform.Find("Hand");

            if (playerHand.gameObject.GetComponent<WeaponBehaviour>().Weapon == null)
            {
                GetComponent<PhotonView>().RPC("RPC_PickupWeapon", RpcTarget.All, playerHand.gameObject.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    public void DestroyWeapon()
    {
        Destroy(this.gameObject);
    }

    [PunRPC]
    void RPC_PickupWeapon(int Hand_id, int Weapon_id)
    {
        GameObject Hand = PhotonView.Find(Hand_id).gameObject;
        GameObject Weapon = PhotonView.Find(Weapon_id).gameObject;

        Hand.GetComponent<WeaponBehaviour>().WeaponSwitch(Weapon.GetComponent<HitboxContainer>().scWeapon);
        Destroy(Weapon);
    }

    [PunRPC]
    void RPC_Damage(int view_id, int _Damage)
    {
        Animator otherAnim = PhotonView.Find(view_id).gameObject.GetComponent<Animator>();

        otherAnim.SetBool("IsHit", true);
        int Health = otherAnim.GetInteger("Health");

        otherAnim.SetInteger("PrevHealth", otherAnim.GetInteger("Health"));
        otherAnim.SetInteger("Health", Health - _Damage);
    }

    [PunRPC]
    void RPC_WallBreak(int view_id, int Weapon_id)
    {
        GameObject Wall = PhotonView.Find(view_id).gameObject;
        Destroy(Wall);
        PhotonView.Find(Weapon_id).gameObject.GetComponent<HitboxContainer>().DestroyWeapon();
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        GameObject player = collision.gameObject;
    //        Transform playerHand = player.transform.Find("Hand");

    //        playerHand.GetComponent<WeaponBehaviour>().WeaponSwitch(scWeapon);
    //        Destroy(this.gameObject);
    //    }
    //}
}
