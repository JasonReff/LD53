using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovementManager : MonoBehaviour
{
    [SerializeField] private Animator _truck, _house, _door;
    [SerializeField] private ScrollingImage _sidewalk, _yellowLine;
    [SerializeField] private float _animationDuration;
    public void StartTruck()
    {
        _sidewalk.enabled = true;
        _yellowLine.enabled = true;
        _house.enabled = true;
        _truck.enabled = true;
        _door.enabled = true;
    }

    public void StopTruck()
    {
        _sidewalk.enabled = false;
        _yellowLine.enabled = false;
        _house.enabled = false;
        _truck.enabled = false;
        _door.enabled = false;
    }

    public IEnumerator GoToNextHouse()
    {
        StartTruck();
        yield return new WaitForSeconds(_animationDuration);
        StopTruck();
    }
}
