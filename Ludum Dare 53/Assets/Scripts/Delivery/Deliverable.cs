using System;
using System.Collections.Generic;
using UnityEngine;

public class Deliverable : MonoBehaviour
{
    private bool _delivered;
    [SerializeField] private int _pointsForDelivering;
    [SerializeField] private Collider2D _collider;
    public bool IsDragged;

    public int Points { get => _pointsForDelivering; }
    public static Action<Deliverable> OnPackageDelivered;

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
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.NameToLayer("UI");
        _collider.OverlapCollider(filter, colliders);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out DeliveryZone zone))
            {
                zone.DeliverPackage(this);
            }
        }
    }
}
