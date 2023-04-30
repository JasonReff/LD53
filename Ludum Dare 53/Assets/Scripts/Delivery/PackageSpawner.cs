﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] private QualityPool _pool;
    [SerializeField] private PackageQualities _packagePrefab;
    [SerializeField] private Vector2 _bottomLeftSpawningCorner, _topRightSpawningCorner;
    [SerializeField] private DeliveryManager _deliveryManager;
    [SerializeField] private int _minimumLoad = 5, _maximumLoad = 9;
    [SerializeField] private int _maximumPerLayer = 5;
    [SerializeField] private TruckDoorMover _doorMover;
    [SerializeField] private float _preSpawnDelay, _postSpawnDelay;
    [SerializeField] private HintManager _hintManager;
    private void Start()
    {
        SpawnPackages();
    }

    public void SpawnPackages()
    {
        StartCoroutine(SpawnCoroutine());

        
    }

    private IEnumerator SpawnCoroutine()
    {
        _doorMover.MoveDoorDown();
        _hintManager.MoveCharacterDown();
        yield return new WaitForSeconds(_preSpawnDelay);
        var count = Random.Range(_minimumLoad, _maximumLoad + 1);
        for (int i = 0; i < count; i++)
        {
            SpawnPackage(i / _maximumPerLayer + 1);
        }
        yield return new WaitForSeconds(_postSpawnDelay);
        _doorMover.MoveDoorUp();
        _deliveryManager.SetDesiredQualities();
        _hintManager.MoveCharacterUp();
    }

    public void OnCorrectDelivery(List<PackageQualities> remainingPackages)
    {
        StartCoroutine(DeliveryCoroutine());

        IEnumerator DeliveryCoroutine() 
        {
            _doorMover.MoveDoorDown();
            _hintManager.MoveCharacterDown();
            yield return new WaitForSeconds(_preSpawnDelay);
            for (int i = 0; i < remainingPackages.Count; i++)
            {
                Destroy(remainingPackages[i].transform.gameObject);
            }
            _deliveryManager.AllPackages.Clear();
            var count = Random.Range(_minimumLoad, _maximumLoad + 1);
            for (int i = 0; i < count; i++)
            {
                SpawnPackage(i / _maximumPerLayer + 1);
            }
            yield return new WaitForSeconds(_postSpawnDelay);
            _doorMover.MoveDoorUp();
            _hintManager.MoveCharacterUp();
            _deliveryManager.SetDesiredQualities();

        }
    }

    private void SpawnPackage(int layer)
    {
        var position = new Vector2(Random.Range(_bottomLeftSpawningCorner.x, _topRightSpawningCorner.x), Random.Range(_bottomLeftSpawningCorner.y, _topRightSpawningCorner.y));
        var package = Instantiate(_packagePrefab, position, Quaternion.identity);
        package.GetComponent<FollowCursor>().BringToLayer(layer);
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