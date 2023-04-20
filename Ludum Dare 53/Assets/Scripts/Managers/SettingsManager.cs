using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider, _effectsSlider, _masterVolumeSlider;

    private void Awake()
    {
        _musicSlider.value = AudioManager.MusicVolume();
        _effectsSlider.value = AudioManager.EffectsVolume();
        _masterVolumeSlider.value = AudioManager.MasterVolume();
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.SetMusicVolume(value);
    }

    public void SetEffectsVolume(float value)
    {
        AudioManager.SetEffectsVolume(value);
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.SetMasterVolume(value);
    }
}