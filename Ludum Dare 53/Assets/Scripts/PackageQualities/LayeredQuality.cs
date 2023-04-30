using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Qualities/Additional")]
public class LayeredQuality : Quality
{
    [SerializeField] private Sprite _qualitySprite;
    [SerializeField] private List<LayeredQuality> _incompatibleQualities;
    public List<LayeredQuality> IncompatibleQualities { get => _incompatibleQualities; }
    public override void ShowQuality(PackageQualities package)
    {
        var childUI = Instantiate(_additionalQualityPrefab, package.transform);
        if (childUI.TryGetComponent(out SpriteRenderer renderer))
        {
            renderer.sprite = _qualitySprite;
        }
    }
}