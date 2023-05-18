using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

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
        RaycastHit2D[] hits = Physics2D.RaycastAll(_main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hits.Length > 0)
        {
            var orderedHits = hits.Where(t => t.transform.GetComponent<SortingGroup>() != null).OrderByDescending(t => t.transform.GetComponent<SortingGroup>().sortingOrder).ToList();
            if (orderedHits.Count == 0)
                return;
            if (orderedHits[0].transform.TryGetComponent(out FollowCursor followCursor))
            {
                _object = followCursor;
                followCursor.OnClick();
            }
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
        }
    }

    
}
