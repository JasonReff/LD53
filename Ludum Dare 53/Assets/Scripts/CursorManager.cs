using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera _main;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(_main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.transform.TryGetComponent(out FollowCursor followCursor))
        {

        }
    }
}
