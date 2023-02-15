using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallBehaviour : MonoBehaviour
{
    private Vector3 shootDir;
    public void SetUp(Vector3 shootDirection)
    {
        shootDir = shootDirection;
    }

    private void Update()
    {
        float moveSpeed = 0.4f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
