using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QualityPool")]
public class QualityPool : ScriptableObject
{
    [SerializeField] private int _minimumAdditionalQualities, _maximumAdditionalQualities;
    [SerializeField] private List<ShapeQuality> _shapes = new List<ShapeQuality>();
    [SerializeField] private List<ColorQuality> _colors = new List<ColorQuality>();
    [SerializeField] private List<SizeQuality> _sizes = new List<SizeQuality>();
    [SerializeField] private List<SlipperyQuality> _wetnesses = new List<SlipperyQuality>();
    [SerializeField] private List<LayeredQuality> _layeredQualities = new List<LayeredQuality>();

    public Qualities RandomQualities()
    {
        var qualities = new Qualities();
        qualities.Shape = _shapes.Rand();
        qualities.Color = _colors.Rand();
        qualities.Size = _sizes.Rand();
        qualities.Wetness = _wetnesses.Rand();
        qualities.AdditionalQualities = GetLayeredQualities();
        return qualities;
    }

    public List<LayeredQuality> GetLayeredQualities()
    {
        var count = Random.Range(_minimumAdditionalQualities, _maximumAdditionalQualities + 1);
        var qualities = new List<LayeredQuality>();
        var qualitiesToChooseFrom = new List<LayeredQuality>(_layeredQualities);
        for (int i = 0; i < count; i++)
        {
            var quality = qualitiesToChooseFrom.Rand();
            qualities.Add(quality);
            qualitiesToChooseFrom.Remove(quality);
            foreach (var incompatible in quality.IncompatibleQualities)
                qualitiesToChooseFrom.Rand();
            if (qualitiesToChooseFrom.Count == 0)
                return qualities;
        }
        return qualities;
    }

    
}
