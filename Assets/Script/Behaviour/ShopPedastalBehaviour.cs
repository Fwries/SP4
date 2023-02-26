using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPedastalBehaviour : MonoBehaviour
{
    private PhotonView m_PhotonView;

    private Vector3 equipOff = new Vector3(0f, 1.6f, 0f);
    private Vector3 weaponOff = new Vector3(-1f, 0f, 0f);
    private Vector3 textOff = new Vector3(0f, 2.7f, 0f);

    private GameObject player;
    public GameObject prefabToSpawn;
    private GameObject pedastalItem;
    
    private bool HasBought;
    public Behaviour halo;
    public GameObject priceDisplay;
    public GameObject realPriceDisplay;

    // Start is called before the first frame update
    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();

        if (!PhotonNetwork.IsMasterClient)
            return;

        prefabToSpawn = transform.GetComponent<RarietyList>().GetRandomGameObject();
        if (prefabToSpawn != null)
        {
            pedastalItem = PhotonNetwork.Instantiate(prefabToSpawn.name, transform.position + equipOff, Quaternion.identity);
            m_PhotonView.RPC("InitPedastalItem", RpcTarget.AllBuffered,
                             pedastalItem.GetComponent<PhotonView>().ViewID, m_PhotonView.ViewID);
        }
        if (pedastalItem.GetComponent<HealthPotionBehaviour>() != null)
        {
            realPriceDisplay = PhotonNetwork.Instantiate(priceDisplay.name, transform.position + textOff, Quaternion.identity);
            m_PhotonView.RPC("InitRealPriceDisplay", RpcTarget.AllBuffered,
                             realPriceDisplay.GetComponent<PhotonView>().ViewID, m_PhotonView.ViewID,
                             pedastalItem.GetComponent<HealthPotionBehaviour>().shopPrice.ToString());
        }
        else if (prefabToSpawn.CompareTag("equipment"))
        {
            realPriceDisplay = PhotonNetwork.Instantiate(priceDisplay.name, transform.position + textOff, Quaternion.identity);
            m_PhotonView.RPC("InitRealPriceDisplay", RpcTarget.AllBuffered,
                             realPriceDisplay.GetComponent<PhotonView>().ViewID, m_PhotonView.ViewID,
                             prefabToSpawn.GetComponent<EquipmentBehaviour>().scEquipment.shopPrice.ToString());
        }
        else
        {
            realPriceDisplay = PhotonNetwork.Instantiate(priceDisplay.name, transform.position + textOff, Quaternion.identity);
            m_PhotonView.RPC("InitRealPriceDisplay", RpcTarget.AllBuffered,
                             realPriceDisplay.GetComponent<PhotonView>().ViewID, m_PhotonView.ViewID,
                             prefabToSpawn.GetComponent<HitboxContainer>().scWeapon.shopPrice.ToString());
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
        if (pedastalItem != null)
        {
            if (pedastalItem.GetComponent<ThrowWeapon>() != null)
            {
                pedastalItem.transform.Rotate(0, Time.deltaTime * 100, 0);
            }
        }
             
        if (Input.GetKeyDown(KeyCode.F) && player != null && HasBought == false && pedastalItem != null)
        {
            // Logic for equipment
            if (pedastalItem.CompareTag("equipment"))
            {
                int playerCoins = player.GetComponent<PlayerStats>().Coin;
                int itemPrice = pedastalItem.GetComponent<EquipmentBehaviour>().scEquipment.shopPrice;
                if (playerCoins >= itemPrice)
                {
                    player.GetComponent<PlayerStats>().IncreaseCoins(-itemPrice);
                    pedastalItem.GetComponent<EquipmentBehaviour>().BroughtFromShop(player);
                    m_PhotonView.RPC("ItemBought", RpcTarget.AllBuffered, "EquipmentBehaviour");
                }
            }
            // Logic for weapon
            else if (pedastalItem.GetComponent<ThrowWeapon>() != null)
            {
                int playerCoins = player.GetComponent<PlayerStats>().Coin;
                int itemPrice = pedastalItem.GetComponent<HitboxContainer>().scWeapon.shopPrice;
                if (playerCoins >= itemPrice)
                {
                    if (player.GetComponentInChildren<WeaponBehaviour>().Weapon == null)
                    {
                        player.GetComponent<PlayerStats>().IncreaseCoins(-itemPrice);
                        player.GetComponentInChildren<WeaponBehaviour>().WeaponSwitch(pedastalItem.GetComponent<HitboxContainer>().scWeapon);
                        m_PhotonView.RPC("ItemBought", RpcTarget.AllBuffered, "WeaponBehaviour");
                    }
                }
            }
            // Logic for consumable (just potion tbh)
            else if (pedastalItem.GetComponent<HealthPotionBehaviour>() != null)
            {
                int playerCoins = player.GetComponent<PlayerStats>().Coin;
                int itemPrice = pedastalItem.GetComponent<HealthPotionBehaviour>().shopPrice;
                if (playerCoins >= itemPrice)
                {
                    player.GetComponent<PlayerStats>().IncreaseCoins(-itemPrice);
                    player.GetComponent<PlayerStats>().RecoverHealth(5);
                    pedastalItem.GetComponent<HealthPotionBehaviour>().BroughtFromShop();
                    m_PhotonView.RPC("ItemBought", RpcTarget.AllBuffered, "HealthPotionBehaviour");
                }
            }
        }
    }

    [PunRPC]
    public void ItemBought(string itemType)
    {
        HasBought = true;
        halo.enabled = false;

        if (PhotonNetwork.IsMasterClient)
        {
            if (itemType == "EquipmentBehaviour")
                pedastalItem.GetComponent<EquipmentBehaviour>().DestroyObject();
            else if (itemType == "WeaponBehaviour")
                pedastalItem.GetComponent<HitboxContainer>().DestroyWeapon();
            else
                pedastalItem.GetComponent<HealthPotionBehaviour>().DestroyObject();

            realPriceDisplay.GetComponent<PriceDisplayBehaviour>().destroyThis();
        }
    }

    [PunRPC]
    public void InitPedastalItem(int pedastalItemID, int shopPedastalID)
    {
        GameObject pedastalItem = PhotonView.Find(pedastalItemID).gameObject;
        GameObject shopPedastal = PhotonView.Find(shopPedastalID).gameObject;
        pedastalItem.transform.SetParent(shopPedastal.transform);
        this.pedastalItem = pedastalItem;
    }

    [PunRPC]
    public void InitRealPriceDisplay(int realPriceDisplayID, int shopPedastalID, string text)
    {
        GameObject realPriceDisplay = PhotonView.Find(realPriceDisplayID).gameObject;
        GameObject shopPedastal = PhotonView.Find(shopPedastalID).gameObject;
        realPriceDisplay.transform.SetParent(shopPedastal.transform);
        realPriceDisplay.GetComponent<TextMesh>().text = text;
        this.realPriceDisplay = realPriceDisplay;
    }
}
