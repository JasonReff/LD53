using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "QualityPool")]
public class QualityPool : ScriptableObject
{
    [SerializeField] private int _minimumAdditionalQualities, _maximumAdditionalQualities;
    [SerializeField] private QualityPool _limitedPool;
    [SerializeField] private List<ShapeQuality> _shapes = new List<ShapeQuality>();
    [SerializeField] private List<ColorQuality> _colors = new List<ColorQuality>();
    [SerializeField] private List<SizeQuality> _sizes = new List<SizeQuality>();
    [SerializeField] private List<SoundQuality> _sounds = new List<SoundQuality>();
    [SerializeField] private List<SlipperyQuality> _wetnesses = new List<SlipperyQuality>();
    [SerializeField] private List<LayeredQuality> _layeredQualities = new List<LayeredQuality>();

    public Qualities RandomQualities(bool limited = true)
    {
        if (limited == true)
        {
            return _limitedPool.RandomQualities(false);
        }
        var qualities = new Qualities();
        qualities.Shape = _shapes.Rand();
        qualities.Color = _colors.Rand();
        qualities.Size = _sizes.Rand();
        qualities.Wetness = _wetnesses.Rand();
        qualities.Sound = _sounds.Rand();
        qualities.AdditionalQualities = GetLayeredQualities();
        return qualities;
    }

    public List<LayeredQuality> GetLayeredQualities()
    {
        var count = Random.Range(_minimumAdditionalQualities, _maximumAdditionalQualities + 1);
        var qualities = new List<LayeredQuality>();
        if (_layeredQualities.Count == 0)
            return qualities;
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

    public List<Quality> GetAllQualities()
    {
        var qualities = new List<Quality>();
        qualities.AddRange(_shapes);
        qualities.AddRange(_colors);
        qualities.AddRange(_sizes);
        qualities.AddRange(_wetnesses);
        qualities.AddRange(_sounds);
        qualities.AddRange(_layeredQualities);
        return qualities.Distinct().ToList();
    }

    public void LimitPool(int nonbaseQualities)
    {
        _limitedPool.SetPool(this, nonbaseQualities);
    }

    public void SetPool(QualityPool parentPool, int nonBaseQualities)
    {
        _shapes.Clear();
        _sizes.Clear();
        _colors.Clear();
        _sounds.Clear();
        _wetnesses.Clear();
        _layeredQualities.Clear();
        var baseShape = parentPool._shapes.Rand();
        var baseSize = parentPool._sizes.Rand();
        var baseColor = parentPool._colors.Rand();
        var baseSound = parentPool._sounds.Rand();
        var baseWetness = parentPool._wetnesses.Rand();
        _shapes.Add(baseShape);
        _sizes.Add(baseSize);
        _colors.Add(baseColor);
        _wetnesses.Add(baseWetness);
        _sounds.Add(baseSound);
        var allQualities = parentPool.GetAllQualities();
        allQualities.Remove(baseShape);
        allQualities.Remove(baseSize);
        allQualities.Remove(baseColor);
        allQualities.Remove(baseSound);
        allQualities.Remove(baseWetness);
        for (int i = 0; i < nonBaseQualities; i++)
        {
            var randomQuality = allQualities.Rand();
            if (randomQuality is ShapeQuality shape)
                _shapes.Add(shape);
            else if (randomQuality is SizeQuality size)
                _sizes.Add(size);
            else if (randomQuality is ColorQuality color)
                _colors.Add(color);
            else if (randomQuality is SoundQuality sound)
                _sounds.Add(sound);
            else if (randomQuality is SlipperyQuality wetness)
                _wetnesses.Add(wetness);
            else if (randomQuality is LayeredQuality layered)
                _layeredQualities.Add(layered);
        }
    }
    
}
