using UnityEngine;

[CreateAssetMenu(menuName = "ScoreKeeper")]
public class ScoreKeeper : ScriptableObject
{
    [SerializeField] private int _highScore;
    public int HighScore { get => _highScore; }
    public void SetHighScore(int score)
    {
        _highScore = score;
    }
}