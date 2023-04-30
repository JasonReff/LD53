using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    [SerializeField] private CharacterData _character;
    [SerializeField] private CharacterPool _pool;
    [SerializeField] private Transform _hintGiver;
    [SerializeField] private float _characterDown, _characterUp, _characterBob, _hintLength;
    [SerializeField] private TextMeshProUGUI _hintTextbox;
    [SerializeField] private GameObject _textbox;
    [SerializeField] private float _hintTimer = 20f, _shakeIntensity = 10f;
    [SerializeField] private Image _speechBubble;
    private bool _isReady;
    private float _timer = 0f;
    private List<Quality> _hintsGiven = new List<Quality>();
    private Qualities _packageQualities;

    private void Start()
    {
        _character = _pool.Characters.Rand();
        GetComponent<Image>().sprite = _character.CharacterSprite;
        _speechBubble.sprite = _character.SpeechBubble;
    }

    private void Update()
    {
        if (!_isReady)
            return;
        if (_timer <= _hintTimer)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            if (_packageQualities != null)
            {
                GetNextHint();
            }
        }
    }

    public void SetQualities(Qualities qualities)
    {
        _packageQualities = qualities;
    }

    public void GetNextHint()
    {
        _timer = 0f;
        var hints = GetRemainingHints();
        if (hints.Count == 0)
        {
            _hintsGiven.Clear();
            hints = GetRemainingHints();
        }
        var randomHint = hints.Rand();
        _hintsGiven.Add(randomHint);
        DisplayHint(randomHint);
    }

    public void DisplayHint(Quality quality)
    {
        StartCoroutine(HintCoroutine());


        IEnumerator HintCoroutine() 
        {
            _textbox.SetActive(true);
            _hintTextbox.text = quality.QualityName;
            ShakeCharacter(1);
            if (_character.GetVoiceLine(quality) != null)
            {
                var clip = _character.GetVoiceLine(quality);
                SayVoiceLine(clip);
            }
            yield return new WaitForSeconds(_hintLength);
            _hintTextbox.text = "";
            _textbox.SetActive(false);
        }
        
    }

    private void SayVoiceLine(AudioClip clip)
    {
        AudioManager.PlaySoundEffect(clip);
        //ShakeCharacter(clip.length);
    }

    public void MoveCharacterUp()
    {
        _isReady = true;
        _timer = _hintTimer - 2f;
        _textbox.SetActive(false);
        _hintGiver.DOLocalMoveY(_characterUp, _characterBob);
    }

    public void MoveCharacterDown()
    {
        _isReady = false;
        _hintGiver.DOLocalMoveY(_characterDown, _characterBob);
        _textbox.SetActive(false);
    }

    private void ShakeCharacter(float duration)
    {
        transform.DOShakePosition(duration, _shakeIntensity);
    }

    private List<Quality> GetRemainingHints()
    {
        List<Quality> qualities = new List<Quality>();
        qualities.Add(_packageQualities.Shape);
        qualities.Add(_packageQualities.Color);
        qualities.Add(_packageQualities.Sound);
        qualities.Add(_packageQualities.Wetness);
        qualities.Add(_packageQualities.Size);
        foreach (var additional in _packageQualities.AdditionalQualities)
            qualities.Add(additional);
        foreach (var hint in _hintsGiven)
            qualities.Remove(hint);
        return qualities;
    }

    public void IncorrectDelivery(PackageQualities package)
    {
        if (CorrectSoFar(package.Qualities))
        {
            GetNextHint();
        }
        else
        {
            _timer = 0f;
            SayVoiceLine(_character.IncorrectGuesses.Rand());
        }
    }

    public bool CorrectSoFar(Qualities qualities)
    {
        var allQualities = qualities.GetAllQualities();
        foreach (var hint in _hintsGiven)
            if (!allQualities.Contains(hint))
                return false;
        return true;
    }
}