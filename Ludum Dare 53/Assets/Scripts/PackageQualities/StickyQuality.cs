public class StickyQuality : LayeredQuality
{
    public override void ShowQuality(PackageQualities package)
    {
        base.ShowQuality(package);
        package.gameObject.AddComponent<StickToObjects>();
    }
}
