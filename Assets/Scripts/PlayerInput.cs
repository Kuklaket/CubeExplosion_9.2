using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private int _leftMouseClick = 0;

    public event Action LeftMouseButtonPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseClick))
            LeftMouseButtonPressed?.Invoke();
    }
}
