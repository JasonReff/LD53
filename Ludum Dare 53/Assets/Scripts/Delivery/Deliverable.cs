using System;
using System.Collections.Generic;
using UnityEngine;

public class Deliverable : MonoBehaviour
{
    private bool _delivered;
    [SerializeField] private int _pointsForDelivering;
    [SerializeField] private Collider2D _collider;
    public bool IsDragged;

    public bool Delivered { get => _delivered; }
    public int Points { get => _pointsForDelivering; }
    public static Action<Deliverable> OnPackageDelivered, OnPackageDropped;

    public void DeliverPackage()
    {
        if (_delivered)
            return;
        _delivered = true;
        OnPackageDelivered?.Invoke(this);
    }

    public void OnUnclick()
    {
        IsDragged = false;
        OnPackageDropped?.Invoke(this);
    }
}
