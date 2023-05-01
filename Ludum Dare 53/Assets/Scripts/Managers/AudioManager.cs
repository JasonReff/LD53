using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [SerializeField] private AudioSource _music, _effects, _voice;
    [SerializeField] private float _masterVolume, _musicVolume, _effectsVolume;
    private static float _minPitch = 0.9f, _maxpitch = 1.1f;

    public static void PlaySoundEffect(AudioClip audioClip)
    {
        float randomPitch = Random.Range(_minPitch, _maxpitch);
        Instance._effects.pitch = randomPitch;
        Instance._effects.PlayOneShot(audioClip);
    }

    public static void PlayVoiceLine(AudioClip clip)
    {
        Instance._voice.PlayOneShot(clip);
    }

    public static void LoopSoundEffect(AudioClip audioClip)
    {
        float randomPitch = Random.Range(_minPitch, _maxpitch);
        Instance._effects.pitch = randomPitch;
        Instance._effects.clip = audioClip;
        Instance._effects.Play();
    }

    public static void EndSoundEffect()
    {
        Instance._effects.Stop();
    }

    public static void SetMusicVolume(float volume)
    {
        Instance._musicVolume = volume;
        Instance._music.volume = volume * Instance._masterVolume;
    }

    public static void SetEffectsVolume(float volume)
    {
        Instance._effectsVolume = volume;
        Instance._effects.volume = volume * Instance._masterVolume;
    }

    public static void SetMasterVolume(float volume)
    {
        Instance._masterVolume = volume;
        SetEffectsVolume(Instance._effectsVolume);
        SetMusicVolume(Instance._musicVolume);
    }

    public static float MusicVolume()
    {
        return Instance._musicVolume;
    }

    public static float EffectsVolume()
    {
        return Instance._effectsVolume;
    }

    public static float MasterVolume()
    {
        return Instance._masterVolume;
    }
}
