using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera _main;
    [SerializeField] private float _shakeVelocity;
    private FollowCursor _object;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
        if (Input.GetMouseButton(0))
        {
            OnDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnUnclick();
        }
    }

    private void OnClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(_main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit && hit.transform.TryGetComponent(out FollowCursor followCursor))
        {
            _object = followCursor;
            followCursor.OnClick();
        }
    }

    private void OnDrag()
    {
        if (_object == null)
            return;
        _object.OnDrag();
        OnShake();
    }

    private void OnUnclick()
    {
        if (_object == null)
            return;
        EndShake();
        _object.OnUnclick();
        _object = null;
    }

    private void OnShake()
    {
        if (_object != null)
        {
            var speed = _object.GetComponent<Rigidbody2D>().velocity.sqrMagnitude;
            if (speed > _shakeVelocity)
            {
                _object.GetComponent<PackageQualities>().OnShake();
                if (_object.TryGetComponent(out FixedJoint2D joint))
                {
                    if (joint.enabled && joint.connectedBody != null)
                    {
                        joint.connectedBody.GetComponent<PackageQualities>().OnShake();
                    }
                }
            }
            else
            {
                _object.GetComponent<PackageQualities>().EndShake();
                if (_object.TryGetComponent(out FixedJoint2D joint))
                {
                    if (joint.enabled && joint.connectedBody != null)
                    {
                        joint.connectedBody.GetComponent<PackageQualities>().EndShake();
                    }
                }
            }
        }
    }

    private void EndShake()
    {
        if (_object != null)
        {
            _object.GetComponent<PackageQualities>().EndShake();
            if (_object.TryGetComponent(out FixedJoint2D joint))
            {
                if (joint.enabled && joint.connectedBody != null)
                {
                    joint.connectedBody.GetComponent<PackageQualities>().EndShake();
                }
            }
        }
    }

    
}
