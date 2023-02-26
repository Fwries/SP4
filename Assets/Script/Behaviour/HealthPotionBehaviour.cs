using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionBehaviour : MonoBehaviour
{
    private int healAmount;
    public int shopPrice = 10;

    [SerializeField] private AudioClip glug;
    public void SetUp(int amount)
    {
        healAmount = amount;
    }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().RecoverHealth(5);
            SoundManager.Instance.PlaySound(glug);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void broughtFromShop(GameObject player)
    {
        SoundManager.Instance.PlaySound(glug);
        PhotonNetwork.Destroy(gameObject);
    }
}
