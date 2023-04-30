using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageRespawner : MonoBehaviour
{
    [SerializeField] private Vector2 _respawnPoint;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Deliverable deliverable))
        {
            if (deliverable.enabled == true)
            {
                RespawnPackage(deliverable);
            }
        }
    }

    public void RespawnPackage(Deliverable deliverable)
    {
        deliverable.transform.position = _respawnPoint;
    }
}