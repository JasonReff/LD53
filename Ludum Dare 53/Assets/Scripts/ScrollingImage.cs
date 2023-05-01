using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollingImage : MonoBehaviour
{
    private RawImage _image;
    [SerializeField] private Vector2 _scrollVelocity;
    private float _scrollTime = 0f;
    private void OnEnable()
    {
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        _scrollTime += Time.deltaTime;
        _image.material.mainTextureOffset = _scrollVelocity * _scrollTime;
    }

}
