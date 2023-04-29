using UnityEngine;

[CreateAssetMenu(menuName = "Qualities/ShapeQuality")]
public class ShapeQuality : Quality
{
    [SerializeField] private Sprite _shapeSprite;
    public override void ShowQuality(PackageQualities package)
    {
        package.ShapeImage.sprite = _shapeSprite;
    }
}
