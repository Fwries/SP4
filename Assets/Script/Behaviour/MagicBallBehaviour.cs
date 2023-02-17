using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviour : MonoBehaviour
{
    private Vector3 shootDir;
    private int damage;
    float projSpeed;
    public void SetUp(Vector3 shootDirection, int Damage, float ProjSpeed)
    {
        shootDir = shootDirection;
        damage = Damage;
        projSpeed = ProjSpeed;
    }

    private void Update()
    {
        transform.position += shootDir * projSpeed * Time.deltaTime;
        if (transform.position.y < -0.1)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
