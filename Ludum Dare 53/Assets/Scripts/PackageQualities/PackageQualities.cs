using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageQualities : MonoBehaviour
{
    [SerializeField] private Qualities _qualities;
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private SpriteMask _mask;
    [SerializeField] private SpriteRenderer _shapeImage;
    [SerializeField] private QualityPool _pool;

    public Qualities Qualities { get => _qualities; set => _qualities = value; }
    
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
        _qualities.Wetness.ShowQuality(this);
        _mask.sprite = _shapeImage.sprite;
        ResetCollider();
        foreach (var quality in _qualities.AdditionalQualities)
        {
            quality.ShowQuality(this);
        }
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingOrder = _shapeImage.sortingOrder + 1;
        }
    }

    private void ResetCollider()
    {
        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < _collider.pathCount; i++)
        {
            path.Clear();
            _shapeImage.sprite.GetPhysicsShape(i, path);
            _collider.SetPath(i, path.ToArray());
        }
    }
}

public struct Qualities
{
    public ShapeQuality Shape;
    public ColorQuality Color;
    public SizeQuality Size;
    public SlipperyQuality Wetness;
    public List<LayeredQuality> AdditionalQualities;
    public string Barcode;

    public Qualities(ShapeQuality shape, ColorQuality color, SizeQuality size, SlipperyQuality wetness, List<LayeredQuality> additionalQualities, string barcode)
    {
        Shape = shape;
        Color = color;
        Size = size;
        Wetness = wetness;
        AdditionalQualities = additionalQualities;
        Barcode = barcode;
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
        if (qualities1.Wetness != qualities2.Wetness)
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