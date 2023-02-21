using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPedastalBehaviour : MonoBehaviour
{
    private Vector3 equipOff = new Vector3(0f, 1.6f, 0f);

    private GameObject player;
    public GameObject prefabToSpawn;
    private GameObject pedastalItem;
    
    private bool HasBought;

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
        if (Input.GetKeyDown(KeyCode.F) && player != null && HasBought == false)
        {
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
        }
    }
}
