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
}
