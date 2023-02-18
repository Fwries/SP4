using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBehaviour : MonoBehaviour
{
    public string equipmentName;
    public ScEquipment scEquipment;
    public ScRarietyList scRarietyList;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }
}
