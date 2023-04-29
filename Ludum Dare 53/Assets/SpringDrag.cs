using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringDrag : MonoBehaviour
{
    [SerializeField] private SpringJoint2D _spring;
    [SerializeField] private Rigidbody2D _rb;
    private Vector2 _pivot;

    private void Start()
    {
        _spring.connectedAnchor = transform.position;
    }

    private void OnMouseDown()
    {
        _spring.enabled = true;
        float objectRotation;
        objectRotation = transform.eulerAngles.z;//get angle of rotation
        objectRotation = objectRotation * ((2f * Mathf.PI) / 360f);//convert to radians
        Vector2 rotationCorrection = new Vector2(Mathf.Cos(2 * objectRotation + Mathf.PI / 4) * Mathf.Sqrt(2f), Mathf.Cos(2 * objectRotation - Mathf.PI / 4) * Mathf.Sqrt(2f));
        _pivot = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        _spring.anchor = Vector2.Scale(_pivot, rotationCorrection); // find position on object and correct for rotation
        _rb.drag = 10;//decrease spring oscillation 'jiggle'
        _rb.angularDrag = 5;//same as above
    }

    private void OnMouseUp()
    {
        _spring.enabled = false;
        _rb.drag = 0;
        _rb.angularDrag = 0;
    }

    private void OnMouseDrag()
    {
        if (!_spring.enabled)
            return;
        _spring.connectedAnchor = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _pivot;
    }
}
