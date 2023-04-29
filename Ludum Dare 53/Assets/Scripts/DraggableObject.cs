using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _minimumDragTime = 0.1f, _maximumForce = 10f;
    private Vector2 _pivot, _startingPosition, _endingPosition;
    private float _timer = 0f;
    private Coroutine _dragCoroutine;

    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        _pivot = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _dragCoroutine = StartCoroutine(DragCoroutine());
    }

    private void OnMouseUp()
    {
        if (_dragCoroutine == null)
            return;
        StopCoroutine(_dragCoroutine);
        ThrowObject();
    }

    private void OnMouseDrag()
    {
        transform.position = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _pivot);
    }

    private IEnumerator DragCoroutine()
    {
        _timer = 0f;
        _startingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        while (_timer < _minimumDragTime)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _endingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _dragCoroutine = StartCoroutine(DragCoroutine());
    }

    private void ThrowObject()
    {
        var difference = _endingPosition - _startingPosition;
        var force = Vector2.ClampMagnitude(difference, _maximumForce);
        _rb.AddForce(force);
    }
}
