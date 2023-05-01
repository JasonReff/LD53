using UnityEngine;

public class ControlParticles : MonoBehaviour
{
    [SerializeField] private float _unclickedLifespan, _clickedLifespan;
    [SerializeField] private ParticleSystem _particles;

    public void OnClick()
    {
        var main = _particles.main;
        main.startLifetime = _clickedLifespan;
    }

    public void OnUnclick()
    {
        var main = _particles.main;
        main.startLifetime = _unclickedLifespan;
    }
}