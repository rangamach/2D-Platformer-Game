using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance { get { return instance; } }

    [SerializeField] AudioSource sound_effect;
    [SerializeField] AudioSource sound_music;
    [SerializeField] bool Mute;
    [SerializeField] float volume = 1f;
    [SerializeField] SoundType[] sounds;    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlaySoundMusic(SoundTypes.BackgroundMusic);
    }

    public void MuteSound(bool status)
    {
        Mute = status;
    }

    public void SetVolume(float vol)
    {
        volume = vol;
        sound_effect.volume = volume;
        sound_music.volume = volume;
    }

    public void PlaySoundMusic(SoundTypes sound_type)
    {
        if (Mute)
            return;
        AudioClip clip = GetSoundClip(sound_type);
        if (clip != null)
        {
            sound_music.clip = clip;
            sound_music.Play();
        }
        else
            Debug.Log("Clip not found for " + sound_type + ".");
    }

    public void PlaySoundEffect(SoundTypes sound_type)
    {
        sound_effect.loop = false;
        if (Mute)
            return;
        AudioClip clip = GetSoundClip(sound_type);
        if (clip != null)
        {
            sound_effect.PlayOneShot(clip);
        }
        else
            Debug.Log("Clip not found for " + sound_type + ".");
    }

    private AudioClip GetSoundClip(SoundTypes type_of_sound)
    {
        SoundType item = Array.Find(sounds, i => i.sound_type == type_of_sound);
        if (item != null)
            return item.sound_clip;
        return null;
    }
}

[Serializable]
public class SoundType
{
    public SoundTypes sound_type;
    public AudioClip sound_clip;
}

public enum SoundTypes
{
    ButtonClick,
    LockedLevel,
    BackgroundMusic,
    PlayerMove,
    PlayerSpawn,
    PlayerJump,
    EnemyHit,
    PlayerDeath,
    EnemyDeath,
    LevelComplete,
    Pickup
}