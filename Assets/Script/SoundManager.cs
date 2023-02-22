using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource, effectSource;
    private float walkTimer;
    private bool canWalk;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        walkTimer = 0f;
        canWalk = false;
    }

    private void Update()
    {
        walkTimer += Time.deltaTime;
        if (walkTimer >= 0.5f)
        {
            canWalk = true;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void PlayWalk(AudioClip clip)
    {
        if (canWalk == true)
        {
            effectSource.PlayOneShot(clip);
            canWalk = false;
            walkTimer = 0f;
        }
        else
        {
            return;
        }
    }
}
