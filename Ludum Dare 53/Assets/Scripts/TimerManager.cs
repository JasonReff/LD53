using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private DeliveryManager _deliveryManager;
    [SerializeField] private TextMeshProUGUI _timerTextbox;
    [SerializeField] private float _timer = 120;
    private bool _gameEnded;

    private void Update()
    {
        if (_timer <= 0 && _gameEnded == false)
        {
            _gameEnded = true;
            _deliveryManager.GameOver();
            return;
        }
        _timer -= Time.deltaTime;
        _timerTextbox.text = TimeToString();
    }

    private string TimeToString()
    {
        int minutes = (int)_timer / 60;
        int seconds = (int)_timer % 60;
        var minutesString = $"{minutes}";
        if (minutes < 10)
            minutesString = minutesString.Insert(0, "0");
        var secondsString = $"{seconds}";
        if (seconds < 10)
            secondsString = secondsString.Insert(0, "0");
        return $"{minutesString}:{secondsString}";
    }
}
