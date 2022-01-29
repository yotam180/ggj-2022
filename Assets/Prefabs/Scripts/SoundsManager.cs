using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static AudioClip OrbHit;
    public static AudioClip Goal;

    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        OrbHit = Resources.Load<AudioClip>("orbHit");
        Goal = Resources.Load<AudioClip>("Goal");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }
}
