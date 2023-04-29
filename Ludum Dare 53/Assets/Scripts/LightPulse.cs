using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    [SerializeField] private Light2D _light;
    private void Update()
    {
        _light.intensity = Mathf.Sin(Time.deltaTime);
    }
}
