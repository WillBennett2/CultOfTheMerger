using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] m_sounds;
    [SerializeField] private GameObject m_container;
    [SerializeField] private AudioMixerGroup m_mixer;

    private void Awake()
    {
        foreach (Sound sound in m_sounds)
        {
            sound.m_source = m_container.AddComponent<AudioSource>();
            sound.m_source.clip = sound.m_clip;
            sound.m_source.outputAudioMixerGroup = m_mixer;

            sound.m_source.volume = sound.m_volume;
            sound.m_source.pitch = sound.m_pitch;
            sound.m_source.loop = sound.m_loop;
        }
    }
    private void Start()
    {
        Play("Theme");
    }
    public void Play(string soundName)
    {
        Sound sound = Array.Find(m_sounds, sound => sound.m_name == soundName);
        if(sound==null)
        {
            Debug.LogWarning("Sound Name Incorrect Attempted To Use" + soundName);
            return;
        }
        sound.m_source.Play();

    }
}

