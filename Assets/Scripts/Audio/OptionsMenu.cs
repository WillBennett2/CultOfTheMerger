using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer m_audioMixer;
    public void SetVolume(float volume)
    {
        m_audioMixer.SetFloat("Volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
