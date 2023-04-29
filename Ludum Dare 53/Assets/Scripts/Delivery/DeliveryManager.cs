using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private Qualities _desiredPackageQualities;
    [SerializeField] private int _startingLives = 3;
    private int _currentLives;
    private int _points;

    private void Start()
    {
        _currentLives = _startingLives;
    }

    private void ComparePackage(PackageQualities package)
    {
        if (package.Qualities == _desiredPackageQualities)
        { 
        
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
