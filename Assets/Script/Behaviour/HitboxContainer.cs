using UnityEngine;

public class HitboxContainer : MonoBehaviour
{
    public Hitbox[] hitboxes;

    // Update is called once per frame
    public void OnHit()
    {
        for (int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i].active = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check whether the gameObject name has "Player.." in it
        if (collision.gameObject.name.Contains("Player"))
        {
            GameObject player = collision.gameObject;
            Transform playerHand = player.transform.Find("Hand");

            // If so, set the weapon's parent to the player's hand
            transform.SetParent(playerHand);
            // The weapon's parent is now the player's hand
            // Update the weaponDir
            playerHand?.GetComponent<ThrowWeapon>().SetWeaponDir(player.GetComponent<PlayerController>().GetCharacterDir());
        }
    }
}
