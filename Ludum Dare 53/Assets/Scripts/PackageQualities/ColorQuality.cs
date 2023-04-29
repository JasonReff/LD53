using UnityEngine;

[CreateAssetMenu(menuName = "Qualities/Color")]
public class ColorQuality : Quality
{
    [SerializeField] private Color _qualityColor;
    public override void ShowQuality(PackageQualities package)
    {
        package.ShapeImage.color = _qualityColor;
    }
}
