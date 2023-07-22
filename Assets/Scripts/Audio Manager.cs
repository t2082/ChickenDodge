using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public GameObject prefab;

    public AudioClip backgroundSound;
    private AudioSource backgroundSoundSource;
    public AudioClip backgroundPlay;
    private AudioSource backgroundPlaySource;
    public AudioClip chickSound;
    private AudioSource chickSoundSource;
    public AudioClip hurtSound;
    private AudioSource hurtSoundSource;
    
    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip clip, float volume, bool isLoopBack)
    {
        if (clip == this.backgroundSound)
        {
            Play(clip, ref backgroundSoundSource, volume, isLoopBack);
        }
        if (clip == this.backgroundPlay)
        {
            Play(clip, ref backgroundPlaySource, volume, isLoopBack);
        }
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        if(clip == this.chickSound)
        {
            Play(clip, ref chickSoundSource, volume);
            return;
        }
        if (clip == this.hurtSound)
        {
            Play(clip, ref hurtSoundSource, volume);
            return;
        }
    }
    private void Play(AudioClip clip, ref AudioSource audioSouce, float volume, bool isLoopback = false)
    {
        if (audioSouce != null && audioSouce.isPlaying) return;
        audioSouce = Instantiate(instance.prefab).GetComponent<AudioSource>();
        audioSouce.volume = volume;
        audioSouce.loop = isLoopback;
        audioSouce.clip = clip;
        audioSouce.Play();
        Destroy(audioSouce.gameObject, audioSouce.clip.length);
    }

    public void StopSound(AudioClip clip)
    {
        if (clip == this.backgroundSound)
        {
            backgroundSoundSource?.Stop();
            return;
        }
        if (clip == this.backgroundPlay)
        {
            backgroundPlaySource?.Stop();
            return;
        }
        if (clip == this.chickSound)
        {
            chickSoundSource?.Stop();
            return;
        }
        if (clip == this.hurtSound)
        {
            hurtSoundSource?.Stop();
            return;
        }
    }
}
