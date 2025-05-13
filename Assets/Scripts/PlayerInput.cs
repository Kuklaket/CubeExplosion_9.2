using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action ScreenPressed;

    private void Update()
    {
        IsLeftMouseButtonDown();
    }

    public void IsLeftMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
            ScreenPressed?.Invoke();
    }
}
