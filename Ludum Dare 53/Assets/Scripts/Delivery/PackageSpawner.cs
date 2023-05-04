using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] private QualityPool _pool;
    [SerializeField] private int _nonbaseQualities = 10;
    [SerializeField] private PackageQualities _packagePrefab;
    [SerializeField] private Vector2 _bottomLeftSpawningCorner, _topRightSpawningCorner;
    [SerializeField] private DeliveryManager _deliveryManager;
    [SerializeField] private int _minimumLoad = 5, _maximumLoad = 9;
    [SerializeField] private int _maximumPerLayer = 5;
    [SerializeField] private TruckDoorMover _doorMover;
    [SerializeField] private float _preSpawnDelay, _postSpawnDelay, _doorClose = 1;
    [SerializeField] private HintManager _hintManager;
    [SerializeField] private TruckMovementManager _truckMovement;
    [SerializeField] private TimerManager _timer;
    private Coroutine _deliveryCoroutine;
    private void Start()
    {
        SpawnPackages(true);
    }

    public void SpawnPackages(bool starting = false)
    {
        _pool.LimitPool(_nonbaseQualities);
        StartCoroutine(SpawnCoroutine(starting));
    }

    public void IncreaseSpawnCounts()
    {
        _minimumLoad++;
        _maximumLoad++;
    }

    private IEnumerator SpawnCoroutine(bool starting = false)
    {
        if (!starting)
        {
            _doorMover.MoveDoorDown();
            _hintManager.MoveCharacterDown();
            yield return new WaitForSeconds(_preSpawnDelay);
        }
        var count = Random.Range(_minimumLoad, _maximumLoad + 1);
        for (int i = 0; i < count; i++)
        {
            SpawnPackage(i / _maximumPerLayer + 1);
        }
        yield return new WaitForSeconds(_postSpawnDelay);
        _doorMover.MoveDoorUp();
        _deliveryManager.SetDesiredQualities();
        _hintManager.MoveCharacterUp();
        yield return new WaitForSeconds(2f);
        _timer.Resume();
    }

    public void OnCorrectDelivery(List<PackageQualities> remainingPackages)
    {
        if (_deliveryCoroutine != null)
            StopCoroutine(_deliveryCoroutine);
        _deliveryCoroutine = StartCoroutine(DeliveryCoroutine());

        IEnumerator DeliveryCoroutine() 
        {
            _timer.Pause();
            _pool.LimitPool(_nonbaseQualities);
            yield return StartCoroutine(_hintManager.CorrectDelivery());
            _doorMover.MoveDoorDown();
            _hintManager.MoveCharacterDown();
            yield return new WaitForSeconds(_doorClose);
            yield return _truckMovement.GoToNextHouse();
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
            yield return new WaitForSeconds(2f);
            _timer.Resume();
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