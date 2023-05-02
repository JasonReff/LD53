using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;

    public void Pause()
    {
        Time.timeScale = 0;
        _pauseCanvas.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        _pauseCanvas.SetActive(false);
    }
}