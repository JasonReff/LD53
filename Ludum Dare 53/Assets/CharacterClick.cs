using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterClick : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private float _lift, _shakeIntensity;
    private float _startingY, _hoverY, _moveTime = 0.25f;

    private void Start()
    {
        _startingY = transform.position.y;
        _hoverY = _startingY + _lift;
    }

    private void OnMouseDown()
    {
        var clip = _clips.Rand();
        transform.DOShakePosition(clip.length, _shakeIntensity).OnComplete(() =>
        {
            transform.DOMoveY(_startingY, _moveTime);
        });
        AudioManager.PlayVoiceLine(clip);
    }

    private void OnMouseEnter()
    {
        transform.DOMoveY(_hoverY, _moveTime);
    }

    private void OnMouseExit()
    {
        transform.DOMoveY(_startingY, _moveTime);
    }
}
