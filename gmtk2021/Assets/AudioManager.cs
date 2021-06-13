using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip EndOfLevel;
    public AudioClip Flip;
    public AudioClip Patrol;
    public AudioClip Button;
    public AudioClip Possess;
    public AudioClip Translate;
    public AudioClip Trigger;

    public AudioClip LevelMusic;

    public AudioSource AudioSource;

    public void PlaySound(AudioClip sound, float volume = 1)
    {
        AudioSource newSource = Instantiate(AudioSource);
        StartCoroutine(playSoundCoroutine(newSource, sound, volume));
    }

    IEnumerator playSoundCoroutine(AudioSource source, AudioClip sound, float volume)
    {
        source.PlayOneShot(sound, volume);
        yield return new WaitForSeconds(sound.length);
        Destroy(source.gameObject);
    }
}
