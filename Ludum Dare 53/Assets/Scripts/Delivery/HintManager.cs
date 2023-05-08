using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    [SerializeField] protected CharacterData _character;
    [SerializeField] protected CharacterPool _pool;
    [SerializeField] private Transform _hintGiver;
    [SerializeField] private float _characterDown, _characterUp, _characterBob, _hintLength;
    [SerializeField] private TextMeshProUGUI _hintTextbox;
    [SerializeField] private GameObject _textbox;
    [SerializeField] private float _hintTimer = 20f, _shakeIntensity = 10f, _hurryTimer = 15f;
    [SerializeField] private Image _speechBubble;
    [SerializeField] private Animator _checkmark;
    private Coroutine _hintCoroutine;
    private bool _isReady, _sayingHint;
    private float _timer = 0f, _hurryTime = 0f;
    private List<Quality> _hintsGiven = new List<Quality>();
    private Qualities _packageQualities;

    private void Start()
    {
        GetCharacter();
    }

    protected virtual void GetCharacter()
    {
        _character = _pool.CurrentCharacter;
        if (_character == null)
        {
            _character = _pool.Characters.Rand();
        }
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
        if (_hurryTime <= _hurryTimer)
        {
            _hurryTime += Time.deltaTime;
        }
        else if (!_sayingHint)
        {
            if (_packageQualities != null)
            {
                SayVoiceLine(_character.HurryLines.Rand());
                _hurryTime = 0;
            }
            
        }
    }

    public void SetQualities(Qualities qualities)
    {
        _hintsGiven.Clear();
        _packageQualities = qualities;
    }

    public void GetNextHint(PackageQualities incorrectPackage = null)
    {
        _timer = 0f;
        var hints = GetRemainingHints();
        if (incorrectPackage != null)
        {
            var differentHints = new List<Quality>(hints);
            foreach (var quality in incorrectPackage.Qualities.GetAllQualities())
                differentHints.Remove(quality);
            if (differentHints.Count > 0)
            {
                hints = differentHints;
            }
        }
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
        if (_hintCoroutine != null)
            StopCoroutine(_hintCoroutine);
        _hintCoroutine = StartCoroutine(HintCoroutine());


        IEnumerator HintCoroutine() 
        {
            _textbox.SetActive(true);
            _hintTextbox.text = _character.GetOverride(quality);
            _sayingHint = true;
            if (_character.GetVoiceLine(quality) != null)
            {
                var clip = _character.GetVoiceLine(quality);
                SayVoiceLine(clip);
            }
            yield return new WaitForSeconds(_hintLength);
            _sayingHint = false;
            _hintTextbox.text = "";
            _textbox.SetActive(false);
        }
        
    }

    private void SayVoiceLine(AudioClip clip)
    {
        AudioManager.PlayVoiceLine(clip);
        ShakeCharacter(clip.length);
    }

    public virtual void MoveCharacterUp()
    {
        _timer = _hintTimer - 2f;
        _hurryTime = 0f;
        _isReady = true;
        _textbox.SetActive(false);
        _hintGiver.DOLocalMoveY(_characterUp, _characterBob);
    }

    public void MoveCharacterDown()
    {
        _hintTextbox.text = "";
        _textbox.SetActive(false);
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
        _checkmark.CrossFade("No", 0f);
        if (CorrectSoFar(package.Qualities))
        {
            GetNextHint(package);
        }
        else
        {
            _timer = 0f;
            if (_character.IncorrectGuesses.Count > 0)
                SayVoiceLine(_character.IncorrectGuesses.Rand());
        }
    }

    public IEnumerator CorrectDelivery()
    {
        _isReady = false;
        _hurryTime = 0f;
        _timer = 0;
        var clip = _character.CorrectGuesses.Rand();
        _checkmark.CrossFade("Yes", 0f);
        SayVoiceLine(clip);
        yield return new WaitForSeconds(clip.length);
    }

    public bool CorrectSoFar(Qualities qualities)
    {
        var allQualities = qualities.GetAllQualities();
        var usefulHints = _hintsGiven.Where(t => _character.GetOverride(t) == t.QualityName).ToList();
        foreach (var hint in usefulHints)
            if (!allQualities.Contains(hint))
                return false;
        return true;
    }
}
