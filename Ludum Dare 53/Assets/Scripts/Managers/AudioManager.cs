using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [SerializeField] private AudioSource _music, _effects, _voice;
    private static float _masterVolume = 0.75f, _musicVolume = 0.5f, _effectsVolume = 0.5f, _voiceVolume = 1f;
    private static float _minPitch = 0.9f, _maxpitch = 1.1f;

    private void Start()
    {
        _music.volume = _musicVolume;
        _effects.volume = _effectsVolume;
        _voice.volume = _voiceVolume;
    }

    public static void PlayMusic(AudioClip song)
    {
        Instance._music.clip = song;
        Instance._music.Play();
    }

    public static void PlaySoundEffect(AudioClip audioClip)
    {
        float randomPitch = Random.Range(_minPitch, _maxpitch);
        Instance._effects.pitch = randomPitch;
        Instance._effects.PlayOneShot(audioClip);
    }

    public static void PlayVoiceLine(AudioClip clip)
    {
        Instance._voice.clip = clip;
        Instance._voice.Play();
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
        _musicVolume = volume;
        Instance._music.volume = volume * _masterVolume;
    }

    public static void SetEffectsVolume(float volume)
    {
        _effectsVolume = volume;
        Instance._effects.volume = volume * _masterVolume;
    }

    public static void SetVoicesVolume(float volume)
    {
        _voiceVolume = volume;
        Instance._voice.volume = volume * _masterVolume;
    }
    public static void SetMasterVolume(float volume)
    {
        _masterVolume = volume;
        SetEffectsVolume(_effectsVolume);
        SetMusicVolume(_musicVolume);
        SetVoicesVolume(_voiceVolume);
    }

    public static float MusicVolume()
    {
        return _musicVolume;
    }

    public static float EffectsVolume()
    {
        return _effectsVolume;
    }

    public static float VoiceVolume()
    {
        return _voiceVolume;
    }

    public static float MasterVolume()
    {
        return _masterVolume;
    }
}
