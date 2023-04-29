using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private TargetJoint2D _targetJoint2D;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _maxForce = 10;
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private float _separationForce, _separationDuration = 0.25f;
    private Vector2 _velocity;


    public void OnClick()
    {
        _targetJoint2D.enabled = true;
        _targetJoint2D.anchor = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        BringToFront();
    }

    private void BringToFront()
    {
        BringToLayer(3);
        StartCoroutine(SeparateObjectsCoroutine());
    }

    public void OnDrag()
    {
        _targetJoint2D.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _velocity = _rigidbody.velocity;
    }

    public void OnUnclick()
    {
        _targetJoint2D.enabled = false;
        _rigidbody.AddForce(Vector2.ClampMagnitude(_velocity, _maxForce));
        BringToLayer(3);
    }

    private IEnumerator SeparateObjectsCoroutine()
    {
        var colliders = new List<Collider2D>();
        _collider.GetContacts(colliders);
        BringToLayer(4);
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingOrder = 6;
        }
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out PackageQualities package))
            {
                var force = (collider.transform.position - transform.position) * _separationForce;
                collider.attachedRigidbody.AddForce(force);
            }
        }
        var filter = new ContactFilter2D();
        filter.layerMask = LayerMask.NameToLayer("Truck3");
        while (_collider.OverlapCollider(filter,colliders) > 0)
        {
            foreach (var collider in colliders)
            {
                var force = (collider.transform.position - transform.position) * _separationForce;
                collider.attachedRigidbody.AddForce(force);
            }
            yield return new WaitForSeconds(_separationDuration);
        }
        BringToLayer(5);
    }

    public void BringToLayer(int layer)
    {
        var _layer = LayerMask.NameToLayer($"Truck{layer}");
        gameObject.layer = _layer;
        GetComponent<SortingGroup>().sortingOrder = layer;
        transform.SetAsLastSibling();
    }
}
