using System.Collections.Generic;
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
        if (_fixedJoint.enabled == false)
        {
            StickToObjectsInOtherLayers();
        }
        if (_fixedJoint.enabled &&_rigidbody.velocity.sqrMagnitude > _maximumVelocity)
        {
            _fixedJoint.connectedBody.GetComponent<PackageQualities>().EndShake();
            _fixedJoint.connectedBody = null;
            _fixedJoint.enabled = false;
        }
    }

    private void StickToObjectsInOtherLayers()
    {
        var layers = new List<LayerMask>() { LayerMask.NameToLayer("Truck1"), LayerMask.NameToLayer("Truck2"), LayerMask.NameToLayer("Truck3") };
        var filter = new ContactFilter2D();
        var results = new List<Collider2D>();
        foreach (var layer in layers)
        {
            filter.layerMask = layer;
            GetComponent<PolygonCollider2D>().OverlapCollider(filter, results);
            foreach (var result in results)
            {
                if (result.TryGetComponent(out PackageQualities package))
                {
                    _fixedJoint.enabled = true;
                    _fixedJoint.connectedBody = results[0].attachedRigidbody;
                }
            }
            
                
        }
        
    }


}