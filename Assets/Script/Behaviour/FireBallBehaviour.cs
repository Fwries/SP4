using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireBallBehaviour : MonoBehaviour
{
    public GameObject explosionParticles;
    public GameObject fireBallParticles;
    public GameObject fireBallParticlesReal;

    public GameObject player;
    private Vector3 shootDir;
    private int damage;
    float projSpeed;
    private PhotonView photonView;

    private void Awake()
    {
        fireBallParticlesReal = Instantiate(fireBallParticles, transform.position, Quaternion.identity);
        fireBallParticlesReal.GetComponent<Transform>().SetParent(this.transform);

        photonView = this.transform.GetComponent<PhotonView>();
    }

    public void SetUp(Vector3 shootDirection, int Damage, float ProjSpeed)
    {
        shootDir = shootDirection;
        damage = Damage;
        projSpeed = ProjSpeed;
    }
    public void SetUp(int Damage)
    {
        damage = Damage;
    }

    private void Update()
    {
        transform.position += shootDir * projSpeed * Time.deltaTime;
        if (transform.position.y < -0.1)
        {
            fireBallParticlesReal.GetComponent<ParticleSystem>().Stop();
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);

        fireBallParticlesReal.GetComponent<ParticleSystem>().Stop();
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            StartCoroutine(TintSpriteRed());
            player.transform.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private IEnumerator TintSpriteRed()
    {
        SpriteRenderer spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();

        // Save the original color
        Color originalColor = spriteRenderer.color;

        // Change the sprite color to the tint color
        spriteRenderer.color = Color.red;

        // Wait for 0.3 seconds
        yield return new WaitForSeconds(0.3f);

        // Change the sprite color back to the original color
        spriteRenderer.color = originalColor;
    }
}
