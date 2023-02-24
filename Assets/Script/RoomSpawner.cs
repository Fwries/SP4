using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using Photon.Pun;

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
    private bool spawned = false;
    public float waitTime = 50.0f;

    Vector3 origin = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        //Destroy(gameObject, waitTime);
        Templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("Spawn", 0.1f);
        }
    }

    private void CheckMinMaxBound(float x, float y)
    {
        Vector2 minBound = Templates.GetMinBound();
        Vector2 maxBound = Templates.GetMaxBound();

        if (x < minBound.x)
            Templates.SetMinBound(x);
        else if (x > maxBound.x)
            Templates.SetMaxBound(x);

        if (y < minBound.y)
            Templates.SetMinBound(float.NaN, y);
        else if (y > maxBound.y)
            Templates.SetMaxBound(float.NaN, y);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            GameObject NewRoom;

            if (DoorDirection == 1)
            {
                rand = Random.Range(0, Templates.BottomRooms.Length);
                NewRoom = PhotonNetwork.Instantiate(Templates.BottomRooms[rand].name, transform.position, Templates.BottomRooms[rand].transform.rotation);
                NewRoom.transform.SetParent(transform.parent);
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
            }
            else if (DoorDirection == 2)
            {
                rand = Random.Range(0, Templates.LeftRooms.Length);
                NewRoom = PhotonNetwork.Instantiate(Templates.LeftRooms[rand].name, transform.position, Templates.LeftRooms[rand].transform.rotation);
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                NewRoom.transform.SetParent(transform.parent);
            }
            else if (DoorDirection == 3)
            {
                rand = Random.Range(0, Templates.TopRooms.Length);
                NewRoom = PhotonNetwork.Instantiate(Templates.TopRooms[rand].name, transform.position, Templates.TopRooms[rand].transform.rotation);
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                NewRoom.transform.SetParent(transform.parent);
            }
            else //if (DoorDirection == 4)
            {
                rand = Random.Range(0, Templates.RightRooms.Length);
                NewRoom = PhotonNetwork.Instantiate(Templates.RightRooms[rand].name, transform.position, Templates.RightRooms[rand].transform.rotation);
                NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                NewRoom.transform.SetParent(transform.parent);
            }

            CheckMinMaxBound(NewRoom.transform.position.z, NewRoom.transform.position.x);

            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.CompareTag("SpawnPoint"))
            {
                if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    GameObject NewRoom = PhotonNetwork.Instantiate(Templates.SecretRoom.name, transform.position, Quaternion.identity);
                    NewRoom.GetComponent<NavMeshSurface>().BuildNavMesh();
                    NewRoom.transform.SetParent(transform);
                    CheckMinMaxBound(NewRoom.transform.position.z, NewRoom.transform.position.x);
                    Destroy(other.gameObject);
                    //Debug.Log("Secret room spawned");
                }
                spawned = true;
            }
        }
    }
}
