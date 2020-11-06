using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public Sounds[] voices;
    public Sounds[] fx;
    //public Sound[] ambient;
    public Sound[] music;
    public int startMusicIndex;

    private string lastVoice;
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
        foreach (Sounds ss in voices)
        {
            foreach (Sound s in ss.sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.outputAudioMixerGroup = s.mixer;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
        foreach (Sounds ss in fx)
        {
            foreach (Sound s in ss.sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.outputAudioMixerGroup = s.mixer;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
        /*foreach (Sound s in ambient)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }*/
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

    public float PlaySound(string name)
    {
        Sounds s = Array.Find(fx, sounds => sounds.name == name);
        if (s == null)
        {
            s = Array.Find(voices, sounds => sounds.name == name);
            if (s == null)
            {
                Debug.LogError("Sound" + name + "not found");
                return 0;
            }
        }
        int index = Random.Range(0, s.sounds.Length);
        return s.PlaySound(index);
    }

    public void StopSound(string name)
    {
        Sounds s = Array.Find(fx, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(voices, sounds => sounds.name == name);
            if (s == null)
            {
                Debug.LogError("Sound" + name + "not found");
                return;
            }
        }

        s.StopSound();
    }

    public bool isPlaying(string name)
    {
        Sounds s = Array.Find(fx, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(voices, sounds => sounds.name == name);
            if (s == null)
            {
                Debug.LogError("Sound" + name + "not found");
                return false;
            }
        }
        return s.IsPlaying();
    }

    public void CutSounds()
    {
        music[startMusicIndex].source.Stop();
    }

    public void changeMusicIndex(int index)
    {
        music[startMusicIndex].source.Stop();
        startMusicIndex = index;
        music[startMusicIndex].source.Play();
    }
}
