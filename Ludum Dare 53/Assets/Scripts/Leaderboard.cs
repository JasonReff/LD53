using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;
using Dan.Models;
using System;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private string _leaderboardPublicKey = "2aa37bed856f4b0351ce178680c618c480c8fea605913b778950129326e72a28";
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private List<TextMeshProUGUI> _entryFields;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameObject _input;



    public void Load()
    {
        _playerScoreText.text = $"{_scoreManager.Score}";
        _usernameInput.text = "AAA";
        LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, (entries) => {
            foreach (var entryField in _entryFields)
                entryField.text = "";
            for (int i = 0; i < entries.Length; i++)
            {
                Entry entry = entries[i];
                _entryFields[i].text = $"{i + 1}. {entry.Username}: {entry.Score}";
            }
        });
    }

    public void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (var entryField in _entryFields)
            entryField.text = "";
        for (int i = 0; i < entries.Length; i++)
        {
            Entry entry = entries[i];
            _entryFields[i].text = $"{i + 1}. {entry.Username}: {entry.Score}";
        }
    }

    public void Submit()
    {
        if (_usernameInput.text == "")
            return;
        LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, _usernameInput.text.ToUpper(), _scoreManager.Score, (success) => {
            if (success)
            {
                _input.SetActive(false);
                Load();
            }
        });
        
    }

    public void OnUploadComplete(bool success)
    {
        if (success)
        {
            Load();
        }
    }
}
