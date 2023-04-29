using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int _layer = LayerMask.NameToLayer("Truck3");
        gameObject.layer = _layer;
        GetComponent<SpriteRenderer>().sortingOrder = 3;
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingOrder = 4;
        }
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
        var _layer = LayerMask.NameToLayer("Truck3");
        gameObject.layer = _layer;
    }

    private IEnumerator SeparateObjectsCoroutine()
    {
        var colliders = new List<Collider2D>();
        _collider.GetContacts(colliders);
        int _layer = LayerMask.NameToLayer("Truck4");
        gameObject.layer = _layer;
        GetComponent<SpriteRenderer>().sortingOrder = 5;
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingOrder = 6;
        }
        foreach (var collider in colliders)
        {
            var force = (collider.transform.position - transform.position) * _separationForce;
            collider.attachedRigidbody.AddForce(force);
        }
        var filter = new ContactFilter2D();
        filter.layerMask = _layer;
        while (_collider.OverlapCollider(filter,colliders) > 0)
        {
            foreach (var collider in colliders)
            {
                var force = (collider.transform.position - transform.position) * _separationForce;
                collider.attachedRigidbody.AddForce(force);
            }
            yield return new WaitForSeconds(_separationDuration);
        }
        _layer = LayerMask.NameToLayer("Truck3");
        gameObject.layer = _layer;
        GetComponent<SpriteRenderer>().sortingOrder = 3;
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingOrder = 4;
        }
    }
}
