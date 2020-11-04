using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public Sound[] fx;
    public Sound[] ambient;
    public Sound[] music;
    public int startMusicIndex;

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in fx)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach (Sound s in ambient)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        //foreach (Sound s in ambient)
        //{
        //    s.source.Play();
        //}
        music[startMusicIndex].source.Play();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(fx, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(ambient, sound => sound.name == name);
            if (s == null)
            {

                Debug.Log("Sound" + name + "not found");
                return;
            }
        }
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(fx, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(ambient, sound => sound.name == name);
            if (s == null)
                return;
        }

        s.source.Stop();
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(fx, sound => sound.name == name);
        if (s != null)
        {
            return s.source.isPlaying;
        }
        else
            return false;
    }

    public void CutSounds()
    {
        foreach (Sound s in ambient)
        {
            s.source.Stop();
        }
        foreach(Sound s in ambient)
        {
            s.source.Stop();
        }
        music[startMusicIndex].source.Stop();
    }

    public void PlayAmbiant()
    {
        foreach (Sound s in ambient)
        {
            s.source.Play();
        }
    }

    public void changeMusicIndex(int index)
    {
        music[startMusicIndex].source.Stop();
        startMusicIndex = index;
        music[startMusicIndex].source.Play();
    }
}
