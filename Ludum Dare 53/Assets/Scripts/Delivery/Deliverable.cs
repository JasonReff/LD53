using System;
using System.Collections.Generic;
using UnityEngine;

public class Deliverable : MonoBehaviour
{
    private bool _delivered;
    [SerializeField] private int _pointsForDelivering;

    public int Points { get => _pointsForDelivering; }
    public static Action<Deliverable> OnPackageDelivered;

    public void DeliverPackage()
    {
        if (_delivered)
            return;
        _delivered = true;
        OnPackageDelivered?.Invoke(this);
    }
}
