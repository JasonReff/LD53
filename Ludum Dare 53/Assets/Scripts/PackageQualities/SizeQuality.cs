using UnityEngine;

[CreateAssetMenu(menuName = "Qualities/Size")]
public class SizeQuality : Quality
{
    [SerializeField] private float _scale;
    public override void ShowQuality(PackageQualities package)
    {
        package.transform.localScale *= _scale;
    }
} 
