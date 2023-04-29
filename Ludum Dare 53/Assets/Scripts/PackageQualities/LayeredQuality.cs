using UnityEngine;

[CreateAssetMenu(menuName = "Qualities/Additional")]
public class LayeredQuality : Quality
{
    [SerializeField] private Sprite _qualitySprite;
    public override void ShowQuality(PackageQualities package)
    {
        var childUI = Instantiate(_additionalQualityPrefab, package.transform);
        childUI.sprite = _qualitySprite;
    }
}
