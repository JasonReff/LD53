using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] private QualityPool _pool;
    [SerializeField] private PackageQualities _packagePrefab;
    [SerializeField] private Vector2 _bottomLeftSpawningCorner, _topRightSpawningCorner;
    [SerializeField] private DeliveryManager _deliveryManager;
    [SerializeField] private int _minimumLoad = 5, _maximumLoad = 9;

    private void Start()
    {
        SpawnPackages();
    }

    public void SpawnPackages()
    {
        var count = Random.Range(_minimumLoad, _maximumLoad + 1);
        for (int i = 0; i < count; i++)
        {
            SpawnPackage();
        }
    }

    private void SpawnPackage()
    {
        var position = new Vector2(Random.Range(_bottomLeftSpawningCorner.x, _topRightSpawningCorner.x), Random.Range(_bottomLeftSpawningCorner.y, _topRightSpawningCorner.y));
        var package = Instantiate(_packagePrefab, position, Quaternion.identity);
        package.GetComponent<FollowCursor>().BringToLayer(1);
        var qualities = _pool.RandomQualities();
        package.ShowQualities(qualities);
        _deliveryManager.AllPackages.Add(package);
    }

    public void OnDrawGizmosSelected()
    {
        var points = new List<Vector2>() { _bottomLeftSpawningCorner,
            new Vector2(_bottomLeftSpawningCorner.x, _topRightSpawningCorner.y),
            _topRightSpawningCorner,
            new Vector2(_topRightSpawningCorner.x, _bottomLeftSpawningCorner.y)};
        foreach (var point in points)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(point, 0.25f);
            Gizmos.DrawLine(point, points.GetNext(point));
        }
    }
}