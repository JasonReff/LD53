using UnityEngine;

public class SeparateOnCollision : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 distanceVector = transform.position - collision.otherCollider.transform.position;
        collision.otherRigidbody.AddForce(-distanceVector * _bounceForce);
        collision.rigidbody.AddForce(distanceVector * _bounceForce);
    }
}
