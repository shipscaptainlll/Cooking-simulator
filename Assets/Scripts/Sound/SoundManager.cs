using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    public Sound[] sounds;

    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else { 
            Destroy(gameObject);
            return;             
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.rolloffMode = sound.audioRolloffMode;
            if (sound.spatialBlend == 1) { sound.source.SetCustomCurve(sound.audioSourceCurveType, sound.audioSourceAnimationCurve); }
            sound.source.minDistance = sound.minDistance;
            sound.source.maxDistance = sound.maxDistance;

            if (sound.audioMixer == null)
            {
                sound.source.outputAudioMixerGroup = _audioMixer;
            } else
            {
                sound.source.outputAudioMixerGroup = sound.audioMixer;
            }
            
        }
    }

    public void Play (string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);
            
            sound.source.Play();
        }
        
    }

    public void Stop (string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);

            sound.source.Stop();
        }
    }

    public float GetSoundLength(string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);

            return sound.clip.length;
        } else
        {
            return 0f;
        }
    }

    public AudioSource LocateAudioSource(string name, Transform newParent)
    {
        //Debug.Log(Array.Find(sounds, sound => sound.name == name).name);
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            string oldName = newParent.gameObject.name;
            Sound sound = Array.Find(sounds, sound => sound.name == name);

            AudioSource newAudioSource = newParent.gameObject.AddComponent<AudioSource>();
            newAudioSource.name = sound.name;
            newAudioSource.clip = sound.clip;

            newAudioSource.volume = sound.volume;
            newAudioSource.pitch = sound.pitch;
            newAudioSource.loop = sound.loop;
            newAudioSource.spatialBlend = sound.spatialBlend;
            newAudioSource.rolloffMode = sound.audioRolloffMode;
            newAudioSource.SetCustomCurve(sound.audioSourceCurveType, sound.audioSourceAnimationCurve);
            newAudioSource.minDistance = sound.minDistance;
            newAudioSource.maxDistance = sound.maxDistance;
            newAudioSource.outputAudioMixerGroup = _audioMixer;

            newParent.gameObject.name = oldName;
            return newAudioSource;
        }
        return null;
    }

    public AudioSource FindSound (string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            return Array.Find(sounds, sound => sound.name == name).source;
        }

        return null;
    }
}
