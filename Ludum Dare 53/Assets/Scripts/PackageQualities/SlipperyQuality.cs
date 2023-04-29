using UnityEngine;
[CreateAssetMenu(menuName = "Qualities/Wetness")]
public class SlipperyQuality : LayeredQuality
{
    [SerializeField] private PhysicsMaterial2D _material;
    public override void ShowQuality(PackageQualities package)
    {
        base.ShowQuality(package);
        package.GetComponent<Collider2D>().sharedMaterial = _material;
    }
}