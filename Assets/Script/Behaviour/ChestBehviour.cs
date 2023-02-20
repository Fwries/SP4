using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehviour : MonoBehaviour
{
    public Transform coinPos;
    private Vector3 coinOff = new Vector3(0f, 0.2f, -1.2f);
    private Vector3 equipOff = new Vector3(1f, 0.3f, -1.2f);

    private GameObject player;
    public GameObject prefabToSpawn;

    private bool HasOpen;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && player != null && HasOpen == false)
        {
            Transform chestTop = transform.GetChild(0);
            chestTop.rotation = Quaternion.Euler(45f, 0f, 0f);
            // Coins spawning (guaranteed)
            Transform coin = Instantiate(coinPos, transform.position + coinOff, Quaternion.identity);
            coin.GetComponent<coinBehaviour>().SetUp(Random.Range(1, 10));

            // Weapon spawning (chance)

            // Equipment spawning (percentage chance done in rarityList script)
            prefabToSpawn =transform.GetComponent<RarietyList>().GetRandomGameObject();
            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, transform.position + equipOff, Quaternion.identity);
            }

            HasOpen = true;
        }
    }
}
