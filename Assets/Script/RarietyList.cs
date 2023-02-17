using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarietyList : MonoBehaviour
{
    public GameObject[] Common;
    public GameObject[] Rare;
    public GameObject[] Epic;
    public GameObject[] Legendary;

    public void SetList(ScRarietyList scRarietyList)
    {
        Common = scRarietyList.Common;
        Rare = scRarietyList.Rare;
        Epic = scRarietyList.Epic;
        Legendary = scRarietyList.Legendary;
    }

    public GameObject GetRandomGameObject()
    {
        int RandRariety = Random.Range(0, 101);
        if (RandRariety < 51)
        {
            return Common[Random.Range(0, Common.Length)];
        }
        else if (RandRariety < 81)
        {
            return Rare[Random.Range(0, Common.Length)];
        }
        else if (RandRariety < 96)
        {
            return Epic[Random.Range(0, Common.Length)];
        }
        else if (RandRariety < 101)
        {
            return Legendary[Random.Range(0, Common.Length)];
        }
        return null;
    }
}
