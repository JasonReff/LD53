using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private DeliveryManager _deliveryManager;
    [SerializeField] private Deliverable _deliveredPackage;
    [SerializeField] private Vector2 _packagePivot;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _characterDownTime;

    private void OnEnable()
    {
        Deliverable.OnPackageDropped += OnPackageDropped;
    }

    private void OnDisable()
    {
        Deliverable.OnPackageDropped -= OnPackageDropped;
    }

    public void DeliverPackage(Deliverable deliverable)
    {
        if (!_deliveryManager.IsCorrectPackage(deliverable))
        {
            _deliveryManager.DeliveryFailed(deliverable);
            return;
        }
        _deliveredPackage = deliverable;
        TakeControlOfPackage();
        deliverable.DeliverPackage();
    }

    public void OnPackageDropped(Deliverable deliverable)
    {
        var colliders = new List<Collider2D>();
        var filter = new ContactFilter2D();
        _collider.OverlapCollider(filter, colliders);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Deliverable package))
            {
                if (_deliveredPackage == null)
                {
                    DeliverPackage(package);
                    return;
                }
            }
        }
    }

    public void TakeControlOfPackage()
    {

        StartCoroutine(PackageCoroutine());

        IEnumerator PackageCoroutine()
        {
            var grabbable = _deliveredPackage.GetComponent<FollowCursor>();
            grabbable.enabled = false;
            var rigidbody = _deliveredPackage.GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Static;
            var sortingGroup = _deliveredPackage.GetComponent<SortingGroup>();
            _deliveredPackage.enabled = false;
            _deliveredPackage.transform.parent = transform;
            _deliveredPackage.transform.localPosition = _packagePivot;
            sortingGroup.sortingOrder = 5;
            yield return new WaitForSeconds(_characterDownTime);
            Destroy(_deliveredPackage.gameObject);
            _deliveredPackage = null;
        }
        

    }
}