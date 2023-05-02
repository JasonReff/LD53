using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreKeeper _scoreKeeper;
    [SerializeField] private int _score = 0, _highScore = 0;
    [SerializeField] private TextMeshProUGUI _scoreTextbox, _highScoreTextbox, _endScoreTextbox, _endHighScore;

    private void Start()
    {
        _highScore = _scoreKeeper.HighScore;
    }

    private void Update()
    {
        _scoreTextbox.text = $"{_score}";
        _highScoreTextbox.text = $"{_highScore}";
    }

    public void OnPackageDelivered()
    {
        _score++;
        if (_score > _highScore)
        {
            _highScore = _score;
            _scoreKeeper.SetHighScore(_highScore);
        }
        _endScoreTextbox.text = $"Delivered: {_score}";
        _endHighScore.text = $"Record: {_highScore}";
    }



}
