using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private float targetaspect = 16.0f / 9.0f;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = GetComponent<Camera>();
        }
    }

    private void Update()
    {
        CheckAspect();
    }

    private void RescaleWindow(float scaleheight)
    {
        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = _camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            _camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = _camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            _camera.rect = rect;
        }
    }

    private void CheckAspect()
    {
        float windowaspect = (float)Screen.width / (float)Screen.height;
        if (targetaspect != windowaspect)
        {
            RescaleWindow(windowaspect / targetaspect);
        }
    }
}
