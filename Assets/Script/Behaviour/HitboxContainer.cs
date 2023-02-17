using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxContainer : MonoBehaviour
{
    public Hitbox[] hitboxes;

    // Update is called once per frame
    public void OnHit()
    {
        for (int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i].active = false;
        }
    }
}
