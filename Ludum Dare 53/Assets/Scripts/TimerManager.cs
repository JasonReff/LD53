using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private DeliveryManager _deliveryManager;
    [SerializeField] private TextMeshProUGUI _timerTextbox, _timeGainedTextbox;
    [SerializeField] private float _timer = 120;
    private bool _gameEnded;
    private bool _paused = true;

    private void Update()
    {
        if (_paused)
            return;
        if (_timer <= 0 && _gameEnded == false)
        {
            _gameEnded = true;
            _deliveryManager.GameOver();
            return;
        }
        _timer -= Time.deltaTime;
        _timerTextbox.text = TimeToString();
    }

    public void Pause()
    {
        _paused = true;
        _timerTextbox.color = Color.red;
    }

    public void Resume()
    {
        _paused = false;
        _timerTextbox.color = Color.black;
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

    public void LoseTime(float seconds)
    {
        _timer -= seconds;
    }

    public void AddTime(float seconds)
    {
        _timer += seconds;
        _timerTextbox.text = TimeToString();
        StartCoroutine(GreenCoroutine());

        IEnumerator GreenCoroutine()
        {
            _timeGainedTextbox.text = $"+{seconds}";
            _timeGainedTextbox.enabled = true;
            yield return new WaitForSeconds(1);
            _timeGainedTextbox.enabled = false;
        }
    }
}
