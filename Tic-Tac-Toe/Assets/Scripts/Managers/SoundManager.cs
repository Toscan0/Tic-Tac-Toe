using UnityEngine;

// I dont expect having more than one sound played at each time

[RequireComponent(typeof(AudioSource))]
public class SoundManager : GenericSingleton<SoundManager>
{
    private AudioSource audioSorce;

    internal override void Init()
    {
        audioSorce = GetComponent<AudioSource>();
    }

    internal void PlaySound(AudioClip audioClip)
    {
        audioSorce.clip = audioClip;
        audioSorce.Play();
    }
}
