using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private Qualities _desiredPackageQualities;
    [SerializeField] private int _startingLives = 3, _minimumPackages = 3;
    [SerializeField] private PackageSpawner _spawner;
    [SerializeField] private int _barcodeLength = 10, _barcodeAlterations = 3;
    [SerializeField] private TextMeshProUGUI _desiredBarcode, _selectedBarcode;
    [SerializeField] private HintManager _hintManager;
    [SerializeField] private PackageRespawner _respawner;
    [SerializeField] private ScoreManager _scoreManager;
    private List<PackageQualities> _allPackages = new List<PackageQualities>();
    private int _currentLives;

    public List<PackageQualities> AllPackages { get => _allPackages; }

    private void Start()
    {
        _currentLives = _startingLives;
    }

    private void OnEnable()
    {
        Deliverable.OnPackageDelivered += OnDelivery;
        PackageQualities.OnPackageHeld += ShowSelectedPackage;
        PackageQualities.OnPackageDropped += HideSelectedPackage;
    }

    private void OnDisable()
    {
        Deliverable.OnPackageDelivered -= OnDelivery;
        PackageQualities.OnPackageHeld -= ShowSelectedPackage;
        PackageQualities.OnPackageDropped -= HideSelectedPackage;
    }

    public void SetDesiredQualities()
    {
        if (_desiredPackageQualities != null)
            return;
        _desiredPackageQualities = _allPackages.Rand().Qualities;
        _hintManager.SetQualities(_desiredPackageQualities);
        CreateBarcode();
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
        _desiredBarcode.text = barcode;
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

    private void ShowSelectedPackage(PackageQualities package)
    {
        _selectedBarcode.text = package.Qualities.Barcode;
    }

    private void HideSelectedPackage()
    {
        _selectedBarcode.text = "";
    }

    public void OnDelivery(Deliverable deliverable)
    {
        if (deliverable.TryGetComponent(out PackageQualities package))
        {
            ComparePackage(deliverable);
        }
    }

    private void ComparePackage(Deliverable deliverable)
    {
        if (IsCorrectPackage(deliverable))
        {
            _allPackages.Remove(deliverable.GetComponent<PackageQualities>());
            DeliverySuccessful(deliverable);
        }
        else
        {
            DeliveryFailed(deliverable);
        }
    }

    public void DeliveryFailed(Deliverable deliverable)
    {
        LoseLife();
        _respawner.RespawnPackage(deliverable);
    }

    public bool IsCorrectPackage(Deliverable deliverable)
    {
        return deliverable.GetComponent<PackageQualities>().Qualities == _desiredPackageQualities;
    }

    private void DeliverySuccessful(Deliverable deliverable)
    {
        _scoreManager.OnPackageDelivered();
        _desiredPackageQualities = null;
        _spawner.OnCorrectDelivery(_allPackages);
    }

    private void LoseLife()
    {
        _currentLives--;
        if (_currentLives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {

    }
}
