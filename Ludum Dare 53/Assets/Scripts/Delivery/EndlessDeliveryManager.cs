﻿using UnityEngine;

public class EndlessDeliveryManager : DeliveryManager
{
    [SerializeField] private TimerManager _timer;
    [SerializeField] private float _timeGain = 5f, _timeLoss = 1f;
    [SerializeField] private int _spawnerIncreaseRate = 3;
    protected override void DeliverySuccessful(Deliverable deliverable)
    {
        base.DeliverySuccessful(deliverable);
        _timer.AddTime(_timeGain);
        if (_scoreManager.Score % _spawnerIncreaseRate == 0)
        {
            _spawner.IncreaseSpawnCounts();
        }
    }

    public override void DeliveryFailed(Deliverable deliverable)
    {
        base.DeliveryFailed(deliverable);
        _timer.LoseTime(_timeLoss);
    }
}