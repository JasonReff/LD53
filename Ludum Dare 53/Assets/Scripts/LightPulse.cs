using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _light;
    [SerializeField] private float _amplitude = 1.5f;
    private void Update()
    {
        var size = Mathf.Sin(Time.time);
        _light.transform.localScale = new Vector2(size * _amplitude, size * _amplitude);
    }
}
