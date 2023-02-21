using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviour : MonoBehaviour
{
    public GameObject explosionParticles;
    public GameObject projParticles1;
    public GameObject projParticlesReal;
    public GameObject player;
    private Vector3 shootDir;
    Color originalColor;
    private int damage;
    float projSpeed;
    private void Awake()
    {
        projParticlesReal = Instantiate(projParticles1, transform.position, Quaternion.identity);
        projParticlesReal.GetComponent<Transform>().SetParent(this.transform);
    }

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
            projParticlesReal.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);

        projParticlesReal.GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject);
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            StartCoroutine(TintSpriteRed());

            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }

    private IEnumerator TintSpriteRed()
    {
        SpriteRenderer spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        // Change the sprite color to the tint color
        spriteRenderer.color = Color.red;

        // Wait for 0.3 seconds
        yield return new WaitForSeconds(0.3f);
        //Debug.Log("turn back");

        // Change the sprite color back to the original color
        spriteRenderer.color = originalColor;
    }
}
