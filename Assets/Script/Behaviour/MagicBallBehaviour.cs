using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviour : MonoBehaviour
{
    private Vector3 shootDir;
    private int damage;
    public void SetUp(Vector3 shootDirection, int Damage)
    {
        shootDir = shootDirection;
        damage = Damage;
    }

    private void Update()
    {
        float moveSpeed = 0.4f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().health -= damage;
        }
    }
}
