using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hitbox : MonoBehaviour
{
    public int index;
    public int Crit = 1;
    public int Damage;
    public bool active;

    [SerializeField] private GameObject Weapon;
    [SerializeField] private HitboxContainer hitBoxContainer;
    private PhotonView photonview;

    void Awake()
    {
        photonview = Weapon.GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!active) { return; }
        if (other.gameObject.tag == "enemy")
        {
            Animator otherAnim = other.gameObject.GetComponent<Animator>();
            if (!otherAnim.GetBool("IsHit"))
            {
                Debug.Log(other.gameObject.GetComponent<PhotonView>().ViewID);
                photonview.GetComponent<PhotonView>().RPC("RPC_Damage", RpcTarget.All, other.gameObject.GetComponent<PhotonView>().ViewID, Damage * Crit);
                hitBoxContainer.OnHit(index);
            }
        }
        if (other.gameObject.tag == "BreakWall")
        {
            hitBoxContainer.OnHit(index);
            photonview.GetComponent<PhotonView>().RPC("RPC_WallBreak", RpcTarget.Others, other.gameObject.GetComponent<PhotonView>().ViewID, Weapon.GetComponent<PhotonView>().ViewID);
        }
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
}
