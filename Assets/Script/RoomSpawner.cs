using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class RoomSpawner : MonoBehaviour
{
    public int DoorDirection;
    /*=====================
     1-Need bottom door
     2-Need left door
     3-Need top door
     4-Need right door
    =======================*/

    private RoomTemplates Templates;
    private int rand;
    private int enemyRand;
    private bool spawned = false;
    public float waitTime = 4.0f;

    Vector3 origin = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        //Destroy(gameObject, waitTime);
        Templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (DoorDirection == 1)
            {
                rand = Random.Range(0, Templates.BottomRooms.Length);
                GameObject NewRoom = Instantiate(Templates.BottomRooms[rand], transform.position, Templates.BottomRooms[rand].transform.rotation);
                
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                NewRoom.transform.SetParent(this.transform);


                rand = Random.Range(0, Templates.RoomDecor.Length);
                enemyRand = Random.Range(0, 5);
                GameObject NewDecor = Instantiate(Templates.RoomDecor[rand], transform.position, Templates.RoomDecor[rand].transform.rotation);
                NewDecor.transform.SetParent(this.transform);

                GameObject enemies = Instantiate(Templates.EnemySetsEasy[enemyRand], transform.position, Quaternion.identity);
                enemies.transform.SetParent(this.transform);
            }
            else if (DoorDirection == 2)
            {
                rand = Random.Range(0, Templates.LeftRooms.Length);
                enemyRand = Random.Range(0, 5);
                GameObject NewRoom = Instantiate(Templates.LeftRooms[rand], transform.position, Templates.LeftRooms[rand].transform.rotation);
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                NewRoom.transform.SetParent(this.transform);

                GameObject enemies = Instantiate(Templates.EnemySetsEasy[enemyRand], transform.position, Quaternion.identity);
                enemies.transform.SetParent(this.transform);

            }
            else if (DoorDirection == 3)
            {
                rand = Random.Range(0, Templates.TopRooms.Length);
                enemyRand = Random.Range(0, 5);
                GameObject NewRoom = Instantiate(Templates.TopRooms[rand], transform.position, Templates.TopRooms[rand].transform.rotation);
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                NewRoom.transform.SetParent(this.transform);

                GameObject enemies = Instantiate(Templates.EnemySetsEasy[enemyRand], transform.position, Quaternion.identity);
                enemies.transform.SetParent(this.transform);
            }
            else if (DoorDirection == 4)
            {
                rand = Random.Range(0, Templates.RightRooms.Length);
                enemyRand = Random.Range(0, 5);
                GameObject NewRoom = Instantiate(Templates.RightRooms[rand], transform.position, Templates.RightRooms[rand].transform.rotation);
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                NewRoom.transform.SetParent(this.transform);

                GameObject enemies = Instantiate(Templates.EnemySetsEasy[enemyRand], transform.position, Quaternion.identity);
                enemies.transform.SetParent(this.transform);
            }

            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                GameObject NewRoom = Instantiate(Templates.SecretRoom, transform.position, Quaternion.identity);
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                NewRoom.transform.SetParent(this.transform);
                Destroy(other.gameObject);
                Debug.Log("Secret room spawned");
            }
            spawned = true;
        }
    }
}
