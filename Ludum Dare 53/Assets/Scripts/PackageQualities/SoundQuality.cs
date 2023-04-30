using UnityEngine;

[CreateAssetMenu(menuName = "Qualities/Sound")]
public class SoundQuality : Quality
{
    [SerializeField] private AudioClip _sound;
    public override void ShowQuality(PackageQualities package)
    {
        package.ShakeSound = _sound;
    }
}
