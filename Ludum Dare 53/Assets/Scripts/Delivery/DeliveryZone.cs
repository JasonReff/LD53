using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private Deliverable _deliveredPackage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Deliverable deliverable))
        {
            _deliveredPackage = deliverable;
            deliverable.DeliverPackage();
        }
    }
}