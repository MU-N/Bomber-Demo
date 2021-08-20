using System;
using System.Linq;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // lives through transitioning
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }
    public void Play(string name)
    {
        Sound snd = Array.Find(sounds, sound => sound.name == name);
        try
        {
            snd.source.Play();
        }
        catch (Exception e)
        {
            Debug.LogWarning("sound not found");
        }


    }
}