using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBehaviour : MonoBehaviour
{
    public string equipmentName;
    public ScEquipment scEquipment;
    public bool isShopItem = false;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isShopItem == false)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerStats>().EquipmentEquip(scEquipment);
                Destroy(this.gameObject);
            }
        }
    }

    public void broughtFromShop(GameObject player)
    {
        player.GetComponent<PlayerStats>().EquipmentEquip(scEquipment);
        DestroyImmediate(this.gameObject, true);
    }

}
