using System;
using UnityEngine;

public class Deliverable : MonoBehaviour
{
    private bool _delivered;
    [SerializeField] private int _pointsForDelivering;
    public static Action<Deliverable> OnPackageDelivered;

    public void DeliverPackage()
    {
        if (_delivered)
            return;
        _delivered = true;
        OnPackageDelivered?.Invoke(this);
        Debug.Log("Gain 10 points.");
    }
}
