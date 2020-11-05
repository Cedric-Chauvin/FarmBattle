using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixer;

    [Range(0.1f, 1f)]
    public float pitch;
    [Range(0.1f, 3.0f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

[Serializable]
public class Sounds
{
    public string name;
    public Sound[] sounds;

    private int lastIndex = -1;

    public float PlaySound(int index)
    {
        lastIndex = index;
        sounds[index].source.Play();
        return sounds[index].clip.length;
    }

    public void StopSound()
    {
        sounds[lastIndex].source.Stop();
    }

    public bool IsPlaying()
    {
        return sounds[lastIndex].source.isPlaying;
    }
}
