using System;
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
    public AudioClip ShakeSound;
    private float _soundTimer = 0f, _soundDelay = 0.5f;

    public static event Action<PackageQualities> OnPackageHeld;
    public static event Action OnPackageDropped;

    public Qualities Qualities { get => _qualities; set => _qualities = value; }
    
    public SpriteRenderer ShapeImage { get => _shapeImage; }

    private void Start()
    {
        //ShowQualities(_pool.RandomQualities());
    }

    private void Update()
    {
        if (_soundTimer < _soundDelay)
        {
            _soundTimer += Time.deltaTime;
        }
    }

    public void ShowQualities(Qualities qualities)
    {
        _qualities = qualities;
        _qualities.Shape.ShowQuality(this);
        _qualities.Color.ShowQuality(this);
        _qualities.Size.ShowQuality(this);
        _qualities.Sound.ShowQuality(this);
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

    public void OnClick()
    {
        OnPackageHeld?.Invoke(this);
    }

    public void OnUnclick()
    {
        OnPackageDropped?.Invoke();
    }

    public void OnShake()
    {
        if (_soundTimer >= _soundDelay)
        {
            _soundTimer = 0f;
            AudioManager.PlaySoundEffect(ShakeSound);
        }
        
    }
}

public class Qualities
{
    public ShapeQuality Shape;
    public ColorQuality Color;
    public SizeQuality Size;
    public SoundQuality Sound;
    public SlipperyQuality Wetness;
    public List<LayeredQuality> AdditionalQualities;
    public string Barcode;
}