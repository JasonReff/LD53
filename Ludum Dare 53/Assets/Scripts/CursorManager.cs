using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera _main;
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
    }

    private void OnUnclick()
    {
        if (_object == null)
            return;
        _object.OnUnclick();
        _object = null;
    }
}
