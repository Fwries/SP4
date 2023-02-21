using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomPrescence : MonoBehaviour
{
    public bool shouldTargetPlayer;
    public bool playerIn;
    public bool roomCleared;

    public float bufferTime;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        shouldTargetPlayer = false;
        playerIn = false;
        roomCleared = false;
        bufferTime = 4.7f;
        enemyCount = GetComponentsInChildren<Transform>().Count(GetComponentInChildren => GetComponentInChildren.CompareTag("enemy")) / 2;
    }

    private void Update()
    {
        if (playerIn == true)
        {
            bufferTime = 0f;
            playerIn = false;
        }

        if (bufferTime < 4.5f)
        {
            bufferTime += 0.05f;
        }
        else if ((bufferTime > 4.5f && bufferTime < 4.7f) && roomCleared == false && playerIn == true)
        {
            bufferTime = 4.7f;
            //Put up walls:
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Wall"))
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        //Logic to check if room is cleared
        if (enemyCount <= 0)
        {
            roomCleared = true;
        }
        if (roomCleared == true)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Wall"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldTargetPlayer = true;
            playerIn = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldTargetPlayer = false;
            playerIn = false;
        }
    }

    public void EnemyDestroyed()
    {
        enemyCount--;
    }
}
