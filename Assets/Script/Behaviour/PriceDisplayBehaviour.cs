using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceDisplayBehaviour : MonoBehaviour
{
    public float destroyTime = 3f;
    public Vector3 offset = new Vector3(0, 2, 0);
    public Vector3 randomizeIntensity = new Vector3(0.1f, 0f, 0f);

    public void destroyThis()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);
    }
}
