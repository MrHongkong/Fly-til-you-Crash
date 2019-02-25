using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider slider;
    public Sound[] sounds;
    public static AudioManager instance;
    public AudioMixer audioMixer;

    void Awake()
    {
        if (instance == null)     
            instance = this;      
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
            s.source.outputAudioMixerGroup = s.Group;
        }
    }

    public void Start()
    {
        Play("MenuMusic");
    }
    public void Update()
    {
        if (slider == null) slider = GetComponentInChildren<Slider>();
        if (slider != null) slider.value = PlayerPrefs.GetFloat("Volume");
        GetMasterLevel();
    }


    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void SetVolume(float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.volume == volume);

        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetSlider()
    {
        slider = GameObject.Find("Menu").GetComponentInChildren<Slider>();
        if (slider != null) slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public float GetMasterLevel()
    {
        float value;
        bool result = audioMixer.GetFloat("Volume", out value);
        if (result)
        {
            return value;

        }
        else
        {
            return 0f;
        }

    }
}
