using UnityEngine;
using UnityEngine.UI;

public abstract class Quality : ScriptableObject
{
    public string QualityName { get => _qualityName; }
    [SerializeField] private string _qualityName;
    [SerializeField] protected GameObject _additionalQualityPrefab;

    public abstract void ShowQuality(PackageQualities package);
}
