using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static AudioClip orbHit;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        orbHit = Resources.Load<AudioClip>("orbHit");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "orbHit":
                audioSrc.PlayOneShot(orbHit);
                break;
        }
    }
}
