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
        // Check whether the gameObject name has "Player.." in it
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            Transform playerHand = player.transform.Find("Hand");

            playerHand.GetComponent<WeaponBehaviour>().WeaponSwitch(scWeapon);
            Destroy(this.gameObject);
        }
    }
}
