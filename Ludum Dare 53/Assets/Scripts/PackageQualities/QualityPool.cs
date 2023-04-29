using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QualityPool")]
public class QualityPool : ScriptableObject
{
    [SerializeField] private List<Quality> _shapes = new List<Quality>();
    [SerializeField] private List<Quality> _colors = new List<Quality>();
    [SerializeField] private List<Quality> _sizes = new List<Quality>();
}
