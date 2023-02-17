using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehaviour : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().Coin++;
            Destroy(this.gameObject);
        }
    }
}
