using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundInCar;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundInCar()
    {
        audioSource.PlayOneShot(soundInCar);
    }
}
