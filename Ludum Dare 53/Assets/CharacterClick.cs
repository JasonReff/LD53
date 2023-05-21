using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CharacterClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CharacterSelect _characterSelect;
    [SerializeField] private CharacterData _character;
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private float _lift, _shakeIntensity;
    private float _startingY, _hoverY, _moveTime = 0.25f;

    private void Start()
    {
        _startingY = transform.position.y;
        _hoverY = _startingY + _lift;
    }

    private void OnMouseEnter()
    {
        //transform.DOMoveY(_hoverY, _moveTime);
    }

    private void OnMouseExit()
    {
        //transform.DOMoveY(_startingY, _moveTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_character != null)
        {
            var clip = _clips.Rand();
            transform.DOShakePosition(clip.length, _shakeIntensity);
            AudioManager.PlayVoiceLine(clip);
        }
        _characterSelect.SetCharacter(_character);
    }
}
