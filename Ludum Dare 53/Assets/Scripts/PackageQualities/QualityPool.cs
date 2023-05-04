using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "QualityPool")]
public class QualityPool : ScriptableObject
{
    [SerializeField] private QualityPool _limitedPool;
    [SerializeField] private int _minimumShapes;
    [SerializeField] private List<ShapeQuality> _shapes = new List<ShapeQuality>();
    [SerializeField] private int _minimumColors;
    [SerializeField] private List<ColorQuality> _colors = new List<ColorQuality>();
    [SerializeField] private int _minimumSizes;
    [SerializeField] private List<SizeQuality> _sizes = new List<SizeQuality>();
    [SerializeField] private int _minimumSounds;
    [SerializeField] private List<SoundQuality> _sounds = new List<SoundQuality>();
    [SerializeField] private int _minimumWetnesses;
    [SerializeField] private List<SlipperyQuality> _wetnesses = new List<SlipperyQuality>();
    [SerializeField] private int _minimumAdditionalQualities, _maximumAdditionalQualities;
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
        var baseShapes = parentPool._shapes.Distinct().ToList().Pull(parentPool._minimumShapes);
        var baseSizes = parentPool._sizes.Distinct().ToList().Pull(parentPool._minimumSizes);
        var baseColors = parentPool._colors.Distinct().ToList().Pull(parentPool._minimumColors);
        var baseSounds = parentPool._sounds.Distinct().ToList().Pull(parentPool._minimumSounds);
        var baseWetnesses = parentPool._wetnesses.Distinct().ToList().Pull(parentPool._minimumWetnesses);
        _shapes.AddRange(baseShapes);
        _sizes.AddRange(baseSizes);
        _colors.AddRange(baseColors);
        _wetnesses.AddRange(baseWetnesses);
        _sounds.AddRange(baseSounds);
        var allQualities = parentPool.GetAllQualities();
        allQualities.RemoveAll(t => baseShapes.Contains(t));
        allQualities.RemoveAll(t => baseSizes.Contains(t));
        allQualities.RemoveAll(t => baseColors.Contains(t));
        allQualities.RemoveAll(t => baseSounds.Contains(t));
        allQualities.RemoveAll(t => baseWetnesses.Contains(t));
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
