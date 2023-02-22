using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionBehaviour : MonoBehaviour
{
    private int healAmount;
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
            Destroy(this.gameObject);
        }
    }

    public void broughtFromShop(GameObject player)
    {
        SoundManager.Instance.PlaySound(glug);
        DestroyImmediate(this.gameObject, true);
    }
}
