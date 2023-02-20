using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBehaviour : MonoBehaviour
{
    public string equipmentName;
    public ScEquipment scEquipment;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().EquipmentEquip(scEquipment);
            Destroy(this.gameObject);
        }
    }

}
