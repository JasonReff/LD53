using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _light;
    private void Update()
    {
        var size = Mathf.Sin(Time.deltaTime);
        _light.transform.localScale = new Vector2(size, size);
    }
}
