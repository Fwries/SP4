using UnityEngine;

public class HitboxContainer : MonoBehaviour
{
    public Hitbox[] hitboxes;
    public ScWeapon scWeapon;

    // Update is called once per frame
    public void OnHit()
    {
        for (int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i].active = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            Transform playerHand = player.transform.Find("Hand");

            if (playerHand.gameObject.GetComponent<WeaponBehaviour>().Weapon == null)
            {
                playerHand.GetComponent<WeaponBehaviour>().WeaponSwitch(scWeapon);
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.tag == "Wall")
        {
            this.gameObject.GetComponent<ThrowWeapon>().StopThrow = true;
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        GameObject player = collision.gameObject;
    //        Transform playerHand = player.transform.Find("Hand");

    //        playerHand.GetComponent<WeaponBehaviour>().WeaponSwitch(scWeapon);
    //        Destroy(this.gameObject);
    //    }
    //}
}
