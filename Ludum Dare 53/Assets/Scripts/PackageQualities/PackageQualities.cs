using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageQualities : MonoBehaviour
{
    [SerializeField] private Qualities _qualities;
    [SerializeField] private SpriteRenderer _shapeImage;
    [SerializeField] private QualityPool _pool;

    public Qualities Qualities { get => _qualities; }
    
    public SpriteRenderer ShapeImage { get => _shapeImage; }

    private void Start()
    {
        ShowQualities(_pool.RandomQualities());
    }

    public void ShowQualities(Qualities qualities)
    {
        _qualities = qualities;
        _qualities.Shape.ShowQuality(this);
        _qualities.Color.ShowQuality(this);
        _qualities.Size.ShowQuality(this);
        foreach (var quality in _qualities.AdditionalQualities)
        {
            quality.ShowQuality(this);
        }
    }
}

public struct Qualities
{
    public ShapeQuality Shape;
    public ColorQuality Color;
    public SizeQuality Size;
    public List<LayeredQuality> AdditionalQualities;

    public Qualities(ShapeQuality shape, ColorQuality color, SizeQuality size, List<LayeredQuality> additionalQualities)
    {
        Shape = shape;
        Color = color;
        Size = size;
        AdditionalQualities = additionalQualities;
    }

    public static bool operator == (Qualities qualities1, Qualities qualities2)
    {
        if (qualities1.Shape != qualities2.Shape)
        {
            return false;
        }
        if (qualities1.Color != qualities2.Color)
        {
            return false;
        }
        if (qualities1.Size != qualities2.Size)
            return false;
        foreach (var quality in qualities1.AdditionalQualities)
        {
            if (!qualities2.AdditionalQualities.Contains(quality))
                return false;
        }
        foreach (var quality in qualities2.AdditionalQualities)
            if (!qualities1.AdditionalQualities.Contains(quality))
                return false;
        return true;
    }

    public static bool operator != (Qualities qualities1, Qualities qualities2)
    {
        return !(qualities1 == qualities2);
    }
}