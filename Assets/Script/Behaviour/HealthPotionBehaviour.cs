using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionBehaviour : MonoBehaviour
{
    private int healAmount;
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
            Destroy(this.gameObject);
        }
    }
}
