using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip shootSound, slideSound, runSound, rollSound;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        shootSound = Resources.Load<AudioClip>("Audio/Pistol");
        slideSound = Resources.Load<AudioClip>("Audio/Slide");
        runSound = Resources.Load<AudioClip>("Audio/Run");
        rollSound = Resources.Load<AudioClip>("Audio/Roll");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Pistol":
                audioSrc.PlayOneShot(shootSound);
                break;
            case "Run":
                audioSrc.PlayOneShot(runSound);
                break;
            case "Slide":
                audioSrc.PlayOneShot(slideSound);
                break;
            case "Roll":
                audioSrc.PlayOneShot(rollSound);
                break;
        }
    }
}
