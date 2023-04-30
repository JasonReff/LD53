using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private Transform _hintGiver;
    [SerializeField] private float _characterDown, _characterUp, _hintDelay;
    [SerializeField] private TextMeshProUGUI _hintTextbox;
    private List<Quality> _hintsGiven = new List<Quality>();
    private Qualities _packageQualities;

    public void SetQualities(Qualities qualities)
    {
        _packageQualities = qualities;
    }

    public void GetNextHint()
    {
        var hints = GetRemainingHints();
        var randomHint = hints.Rand();
        _hintsGiven.Add(randomHint);
    }

    public void DisplayHint(Quality quality)
    {
        StartCoroutine(HintCoroutine());

        IEnumerator HintCoroutine()
        {
            _hintGiver.DOMoveY(_characterUp, _hintDelay);
            yield return new WaitForSeconds(_hintDelay);
            _hintTextbox.text = quality.QualityName;
            yield return new WaitForSeconds(_hintDelay);
            _hintGiver.DOMoveY(_characterDown, _hintDelay);
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