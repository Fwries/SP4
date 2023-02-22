using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPedastalBehaviour : MonoBehaviour
{
    private Vector3 equipOff = new Vector3(0f, 1.6f, 0f);
    private Vector3 weaponOff = new Vector3(-1f, 0f, 0f);

    private GameObject player;
    public GameObject prefabToSpawn;
    private GameObject pedastalItem;
    
    private bool HasBought;
    public Behaviour halo;

    // Start is called before the first frame update
    void Start()
    {
        prefabToSpawn = transform.GetComponent<RarietyList>().GetRandomGameObject();
        if (prefabToSpawn != null)
        {
           pedastalItem = Instantiate(prefabToSpawn, transform.position + equipOff, Quaternion.identity);
           pedastalItem.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && player != null && HasBought == false && pedastalItem != null)
        {
            // Logic for equipment
            if (prefabToSpawn.CompareTag("equipment"))
            {
                int playerCoins = player.GetComponent<PlayerStats>().Coin;
                int itemPrice = prefabToSpawn.GetComponent<EquipmentBehaviour>().scEquipment.shopPrice;
                if (playerCoins >= itemPrice)
                {
                    player.GetComponent<PlayerStats>().IncreaseCoins(-itemPrice);
                    pedastalItem.GetComponent<EquipmentBehaviour>().broughtFromShop(player);
                }
            }
            // Logic for weapon
            else if (pedastalItem.GetComponent<ThrowWeapon>() != null)
            {
                Debug.Log("Weapon buying");
                GameObject weaponInHand = player.GetComponentInChildren<WeaponBehaviour>().Weapon;
                weaponInHand = Instantiate(weaponInHand, (player.transform.position - weaponOff), Quaternion.identity);
                weaponInHand.GetComponent<MeshCollider>().enabled = true;
                player.GetComponentInChildren<WeaponBehaviour>().WeaponSwitch(pedastalItem.GetComponent<HitboxContainer>().scWeapon);
                pedastalItem.GetComponent<HitboxContainer>().DestroyWeapon();
            }
            // Logic for consumable (just potion tbh)
            else if (pedastalItem.GetComponent<HealthPotionBehaviour>() != null)
            {
                Debug.Log("Weapon buying");
                player.GetComponent<PlayerStats>().RecoverHealth(5);
                pedastalItem.GetComponent<HealthPotionBehaviour>().broughtFromShop(player);
            }
            HasBought = true;
            halo.enabled = false;
        }
    }
}
