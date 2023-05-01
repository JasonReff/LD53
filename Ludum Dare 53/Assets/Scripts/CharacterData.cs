using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _characterSprite, _speechBubbleSprite;
    [SerializeField] private List<VoiceLines> _voiceLines = new List<VoiceLines>();
    [SerializeField] private List<AudioClip> _hurryLines = new List<AudioClip>(), _incorrectGuesses = new List<AudioClip>(), _correctGuesses = new List<AudioClip>();
    [SerializeField] private QualityPool _qualityPool;

    public Sprite CharacterSprite { get => _characterSprite; }
    public Sprite SpeechBubble { get => _speechBubbleSprite; }
    public string CharacterName { get => _characterName; }
    public List<AudioClip> IncorrectGuesses { get => _incorrectGuesses; }
    public List<AudioClip> CorrectGuesses { get => _correctGuesses; }
    public List<AudioClip> HurryLines { get => _hurryLines; }

    public string GetOverride(Quality quality)
    {
        if (!_voiceLines.Any(t => t.Quality == quality.QualityName))
            return quality.QualityName;
        var voiceLines = _voiceLines.First(t => t.Quality == quality.QualityName);
        if (voiceLines.Override != "")
        {
            return voiceLines.Override;
        }
        return quality.QualityName;
    }

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
#if UNITY_EDITOR
    public void GetVoiceLines()
    {
        _voiceLines.Clear();
        foreach (var quality in _qualityPool.GetAllQualities())
        {
            var qualityName = quality.QualityName.ToLower();
            var characterName = _characterName.ToLower();
            string[] guids = AssetDatabase.FindAssets($"t:AudioClip {qualityName} {characterName}");
            var voiceLines = new VoiceLines();
            voiceLines.Clips = new List<AudioClip>();
            voiceLines.Quality = quality.QualityName;
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                AudioClip clip = (AudioClip)AssetDatabase.LoadAssetAtPath<AudioClip>(path);
                voiceLines.Clips.Add(clip);
            }
            _voiceLines.Add(voiceLines);
        }
    }
#endif

    [Serializable]
    public class VoiceLines
    {
        [SerializeField] private string _quality, _qualityOverride;
        [SerializeField] private List<AudioClip> _clips;
        public string Quality { get => _quality; set => _quality = value; }
        public string Override { get => _qualityOverride; }
        public List<AudioClip> Clips { get => _clips; set => _clips = value; }
    }
}
