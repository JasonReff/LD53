using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectOnClick : MonoBehaviour
{
    [SerializeField] private DistanceJoint2D _joint;

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            Connect();
        }
        else
        {
            transform.parent = null;
            _joint.connectedBody = null;
        }
        if (_joint.connectedBody != null)
        {
            _joint.connectedAnchor = transform.localPosition;
        }
    }

    private void Connect()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            transform.parent = hit.rigidbody.transform;
            _joint.connectedBody = hit.rigidbody;
        }
    }
}
