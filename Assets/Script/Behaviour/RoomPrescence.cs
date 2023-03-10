using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class RoomPrescence : MonoBehaviour
{
    private Color m_ACTIVE_COLOR = new Color(0.5f, 0.5f, 0.5f);

    public bool shouldTargetPlayer;
    public bool playerIn;
    public bool playerInIn;
    public bool roomCleared;

    public float bufferTime;
    public int enemyCount;
    [SerializeField] private AudioClip wallSound;
    private bool playSound;

    private bool NewRoom = true;

    // Start is called before the first frame update
    void Start()
    {
        shouldTargetPlayer = false;
        playerIn = false;
        playerInIn = false;
        roomCleared = false;
        bufferTime = 4.7f;
        enemyCount = GetComponentsInChildren<Transform>().Count(GetComponentInChildren => GetComponentInChildren.CompareTag("enemy")) / 2;
        playSound = false;
    }

    private void SetRoomActiveOnMinimap()
    {
        Transform minimapObjects = transform.parent.Find("MinimapObjects");
        minimapObjects.Find("MinimapObject").GetComponent<SpriteRenderer>().color = m_ACTIVE_COLOR;
        minimapObjects.Find("RoomType").GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Update()
    {
        if (playerIn == true)
        {
            bufferTime = 0f;
            playerIn = false;
            playerInIn = true;
        }

        if (bufferTime < 4.5f)
        {
            bufferTime += 0.05f;
        }
        else if ((bufferTime > 4.5f && bufferTime < 4.7f) && roomCleared == false && playerInIn == true)
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
            SoundManager.Instance.PlaySound(wallSound);

            SetRoomActiveOnMinimap();
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
            if (playSound == false)
            {
                SoundManager.Instance.PlaySound(wallSound);
                playSound = true;
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIn = true;
            shouldTargetPlayer = true;

            if (NewRoom)
            {
                NewRoom = false;

                List<GameObject> playerList = new List<GameObject>();
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject player in players)
                {
                    if (player != other.gameObject)
                    {
                        player.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                    }
                }
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldTargetPlayer = false;
            playerIn = false;
            playerInIn = false;
        }
    }

    public void EnemyDestroyed()
    {
        enemyCount--;
    }
}
