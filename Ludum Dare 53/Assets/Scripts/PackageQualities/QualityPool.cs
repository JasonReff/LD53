using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QualityPool")]
public class QualityPool : ScriptableObject
{
    [SerializeField] private int _minimumAdditionalQualities, _maximumAdditionalQualities;
    [SerializeField] private List<ShapeQuality> _shapes = new List<ShapeQuality>();
    [SerializeField] private List<ColorQuality> _colors = new List<ColorQuality>();
    [SerializeField] private List<SizeQuality> _sizes = new List<SizeQuality>();
    [SerializeField] private List<LayeredQuality> _layeredQualities = new List<LayeredQuality>();

    public Qualities RandomQualities()
    {
        var qualities = new Qualities();
        qualities.Shape = _shapes.Rand();
        qualities.Color = _colors.Rand();
        qualities.Size = _sizes.Rand();
        var layeredQualities = _layeredQualities.Pull(Random.Range(_minimumAdditionalQualities, _maximumAdditionalQualities + 1));
        qualities.AdditionalQualities = layeredQualities;
        return qualities;
    }

    
}
