using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private TargetJoint2D _targetJoint2D;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _maxForce = 10;
    private Vector2 _velocity;


    private void OnMouseDown()
    {
        _targetJoint2D.enabled = true;
        _targetJoint2D.anchor = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseDrag()
    {
        _targetJoint2D.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _velocity = _rigidbody.velocity;
    }

    private void OnMouseUp()
    {
        _targetJoint2D.enabled = false;
        _rigidbody.AddForce(Vector2.ClampMagnitude(_velocity, _maxForce));
    }
}
