using UnityEngine;

[RequireComponent(typeof(FixedJoint2D))]
public class StickToObjects : MonoBehaviour
{
    private FixedJoint2D _fixedJoint;
    private Rigidbody2D _rigidbody;
    private float _maximumVelocity = 50f;

    private void Awake()
    {
        _fixedJoint = GetComponent<FixedJoint2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _fixedJoint.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out PackageQualities packageQualities))
        {
            _fixedJoint.enabled = true;
            _fixedJoint.connectedBody = collision.rigidbody;
        }
    }

    private void Update()
    {
        if (_fixedJoint.enabled &&_rigidbody.velocity.sqrMagnitude > _maximumVelocity)
        {
            _fixedJoint.connectedBody = null;
            _fixedJoint.enabled = false;
        }
    }


}