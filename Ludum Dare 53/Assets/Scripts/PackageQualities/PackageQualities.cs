using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageQualities : MonoBehaviour
{
    [SerializeField] private ShapeQuality _shape;
    [SerializeField] private ColorQuality _color;
    [SerializeField] private List<LayeredQuality> _additionalQualities = new List<LayeredQuality>();
    [SerializeField] private SpriteRenderer _shapeImage;
    
    public SpriteRenderer ShapeImage { get => _shapeImage; }

    private void Start()
    {
        ShowQualities();
    }

    public void ShowQualities(ShapeQuality shape = null, ColorQuality color = null, List<LayeredQuality> qualities = null)
    {
        if (shape != null)
            _shape = shape;
        if (color != null)
            _color = color;
        if (qualities != null)
            _additionalQualities = qualities;
        _shape.ShowQuality(this);
        _color.ShowQuality(this);
        foreach (var quality in _additionalQualities)
        {
            quality.ShowQuality(this);
        }
    }
}
