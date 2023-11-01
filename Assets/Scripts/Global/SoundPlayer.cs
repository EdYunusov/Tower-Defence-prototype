using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : SingletonBase<SoundPlayer>
{
    [SerializeField] private Sounds m_Sounds;
    [SerializeField] private AudioClip m_BGM;
    private AudioSource m_AS;

    private new void Awake()
    {
        base.Awake();
        m_AS = GetComponent<AudioSource>();
        Instance.m_AS.clip = m_BGM;
        Instance.m_AS.Play();
        
    }
    public void Play(Sound sound)
    {
        m_AS.PlayOneShot(m_Sounds[sound]);
    }
}
