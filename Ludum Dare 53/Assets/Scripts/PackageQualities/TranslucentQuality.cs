using UnityEngine;

[CreateAssetMenu(menuName = "Qualities/Translucent")]
public class TranslucentQuality : LayeredQuality
{
    [SerializeField] private float _alpha;

    public override void ShowQuality(PackageQualities package)
    {
        Color color = package.GetComponent<SpriteRenderer>().color;
        color.a = _alpha;
        package.GetComponent<SpriteRenderer>().color = color;
    }
}