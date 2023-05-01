using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider, _effectsSlider, _voicesSlider, _masterVolumeSlider;
    [SerializeField] private List<AudioClip> _effectsClips, _voiceClips;

    private void Awake()
    {
        _musicSlider.value = AudioManager.MusicVolume();
        _effectsSlider.value = AudioManager.EffectsVolume();
        _voicesSlider.value = AudioManager.VoiceVolume();
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.SetMusicVolume(value);
    }

    public void SetEffectsVolume(float value)
    {
        AudioManager.SetEffectsVolume(value);
    }

    public void SetVoicesVolume(float value)
    {
        AudioManager.SetVoicesVolume(value);
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.SetMasterVolume(value);
    }

    public void PlayEffectsClip()
    {
        AudioManager.PlaySoundEffect(_effectsClips.Rand());
    }

    public void PlayVoiceClip()
    {
        AudioManager.PlayVoiceLine(_voiceClips.Rand());
    }
}
