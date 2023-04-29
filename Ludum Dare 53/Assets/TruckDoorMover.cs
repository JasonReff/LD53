using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TruckDoorMover : MonoBehaviour
{
    [SerializeField] private float _upPosition, _downPosition, _moveTime;
    void Start()
    {
        MoveDoorUp();
    }

    public void MoveDoorUp()
    {
        transform.DOLocalMove(new Vector2(0, _upPosition), _moveTime);
    }

    public void MoveDoorDown()
    {
        transform.DOLocalMove(new Vector2(0, _downPosition), _moveTime);
    }
}
