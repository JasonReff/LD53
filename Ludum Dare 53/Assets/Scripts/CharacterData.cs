using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _characterSprite;
    [SerializeField] private List<VoiceLines> _voiceLines = new List<VoiceLines>();
    [SerializeField] private List<AudioClip> _nonHints = new List<AudioClip>();

    public Sprite CharacterSprite { get => _characterSprite; }
    public string CharacterName { get => _characterName; }

    public AudioClip GetVoiceLine(Quality quality)
    {
        if (!_voiceLines.Any(t => t.Quality == quality.QualityName))
            return null;
        var voiceLines = _voiceLines.First(t => t.Quality == quality.QualityName);
        if (voiceLines.Clips.Count == 0)
            return null;
        var line = voiceLines.Clips.Rand();
        return line;
    }

    [Serializable]
    public class VoiceLines
    {
        [SerializeField] private string _quality;
        [SerializeField] private List<AudioClip> _clips;
        public string Quality { get => _quality; }
        public List<AudioClip> Clips { get => _clips; }
    }
}
