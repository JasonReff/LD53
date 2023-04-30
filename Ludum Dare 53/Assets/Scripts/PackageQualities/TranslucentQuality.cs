using UnityEngine;

[CreateAssetMenu(menuName = "Qualities/Translucent")]
public class TranslucentQuality : LayeredQuality
{
    [SerializeField] private float _alpha;

    public override void ShowQuality(PackageQualities package)
    {
        if (_additionalQualityPrefab != null)
        {
            var childUI = Instantiate(_additionalQualityPrefab, package.transform);
            childUI.GetComponent<SpriteRenderer>().sprite = _qualitySprite;
            Color childColor = childUI.GetComponent<SpriteRenderer>().color;
            childColor.a = _alpha;
            childUI.GetComponent<SpriteRenderer>().color = childColor;
        }
        else
        {
            Color color = package.GetComponent<SpriteRenderer>().color;
            color.a = _alpha;
            package.GetComponent<SpriteRenderer>().color = color;
        }
    }
}