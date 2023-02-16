using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPrescence : MonoBehaviour
{
    public bool shouldTargetPlayer;
    // Start is called before the first frame update
    void Awaken()
    {
        shouldTargetPlayer = false;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldTargetPlayer = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldTargetPlayer = false;
        }
    }
}
