using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private Deliverable _deliveredPackage;
    [SerializeField] private Vector2 _packagePivot;

    public void DeliverPackage(Deliverable deliverable)
    {
        _deliveredPackage = deliverable;
        TakeControlOfPackage();
        deliverable.DeliverPackage();
    }

    public void TakeControlOfPackage()
    {
        var grabbable = _deliveredPackage.GetComponent<FollowCursor>();
        grabbable.OnUnclick();
        grabbable.enabled = false;
        var rigidbody = _deliveredPackage.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Static;
        _deliveredPackage.transform.parent = transform;
        _deliveredPackage.transform.localPosition = _packagePivot;

    }
}