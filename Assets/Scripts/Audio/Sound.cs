using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string m_name;
    public AudioClip m_clip;
    [Range(0, 1)] public float m_volume;
    [Range(0, 5)] public float m_pitch;

    [HideInInspector] public AudioSource m_source;

    public bool m_loop;
    public float m_duration;
    public bool m_isPlaying;
    public bool m_isPaused;
}
