using UnityEngine;

public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    [SerializeField] private bool _dontDestroyOnLoad = false;

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<T>();
        }
        else if (Instance != this)
            Destroy(gameObject);
        if (_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
