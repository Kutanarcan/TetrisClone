using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    static AudioSource audioSource;

    public static AudioSource AudioSource => audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
