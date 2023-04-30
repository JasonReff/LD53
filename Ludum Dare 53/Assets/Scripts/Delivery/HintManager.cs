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
    [SerializeField] private float _hintTimer = 20f;
    private float _timer = 0f;
    private List<Quality> _hintsGiven = new List<Quality>();
    private Qualities _packageQualities;

    private void Start()
    {
        _character = _pool.Characters.Rand();
        GetComponent<Image>().sprite = _character.CharacterSprite;
    }

    private void Update()
    {
        if (_timer <= _hintTimer)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0f;
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
        var hints = GetRemainingHints();
        if (hints.Count == 0)
            return;
        var randomHint = hints.Rand();
        _hintsGiven.Add(randomHint);
        DisplayHint(randomHint);
        if (_character.GetVoiceLine(randomHint) != null)
        {
            var clip = _character.GetVoiceLine(randomHint);
            AudioManager.PlaySoundEffect(clip);
        }
    }

    public void DisplayHint(Quality quality)
    {
        StartCoroutine(HintCoroutine());

        IEnumerator HintCoroutine()
        {
            _textbox.SetActive(false);
            _hintGiver.DOLocalMoveY(_characterUp, _characterBob);
            yield return new WaitForSeconds(_characterBob);
            _textbox.SetActive(true);
            _hintTextbox.text = quality.QualityName;
            yield return new WaitForSeconds(_hintLength);
            _hintGiver.DOLocalMoveY(_characterDown, _characterBob);
            _textbox.SetActive(false);
        }
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
}