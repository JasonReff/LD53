using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private Qualities _desiredPackageQualities;
    [SerializeField] private int _startingLives = 3, _minimumPackages = 3;
    [SerializeField] private PackageSpawner _spawner;
    [SerializeField] private int _barcodeLength = 10, _barcodeAlterations = 3;
    private List<PackageQualities> _allPackages = new List<PackageQualities>();
    private int _currentLives;
    private int _points;

    public List<PackageQualities> AllPackages { get => _allPackages; }

    private void Start()
    {
        _currentLives = _startingLives;
    }

    private void OnEnable()
    {
        Deliverable.OnPackageDelivered += OnDelivery;
    }

    private void OnDisable()
    {
        Deliverable.OnPackageDelivered -= OnDelivery;
    }

    private void CreateBarcode()
    {
        var barcode = GameExtensions.RandomNumbers(_barcodeLength);
        _desiredPackageQualities.Barcode = barcode;
        foreach (var package in _allPackages)
        {
            if (package.Qualities != _desiredPackageQualities)
            {
                var qualities = package.Qualities;
                qualities.Barcode = AlteredBarcode(barcode);
                package.Qualities = qualities;
            }
        }
    }

    private string AlteredBarcode(string original)
    {
        var barcode = new string(original);
        var indexesLeft = new List<int>();
        for (int i = 0; i < barcode.Length; i++) 
        {
            indexesLeft.Add(i);
        }
        for (int j = 0; j < _barcodeAlterations; j++)
        {
            var index = indexesLeft.Rand();
            barcode = barcode.Remove(index,1).Insert(index,$"{Random.Range(0, 10)}");
            indexesLeft.Remove(index);
        }
        return barcode;
    }

    private void OnDelivery(Deliverable deliverable)
    {
        if (deliverable.TryGetComponent(out PackageQualities package))
        {
            _allPackages.Remove(package);
            ComparePackage(deliverable);
        }
        if (_allPackages.Count < _minimumPackages)
            _spawner.SpawnPackages();
    }

    private void ComparePackage(Deliverable deliverable)
    {
        if (deliverable.GetComponent<PackageQualities>().Qualities == _desiredPackageQualities)
        {
            DeliverySuccessful(deliverable);
        }
        else
        {
            LoseLife();
        }
    }

    private void DeliverySuccessful(Deliverable deliverable)
    {
        _points += deliverable.Points;
    }

    private void LoseLife()
    {
        _currentLives--;
        if (_currentLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {

    }
}
