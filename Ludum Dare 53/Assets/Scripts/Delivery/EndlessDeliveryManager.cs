using UnityEngine;

public class EndlessDeliveryManager : DeliveryManager
{
    [SerializeField] private TimerManager _timer;
    [SerializeField] private float _timeGain = 5f;
    protected override void DeliverySuccessful(Deliverable deliverable)
    {
        base.DeliverySuccessful(deliverable);
        _timer.AddTime(_timeGain);
    }
}