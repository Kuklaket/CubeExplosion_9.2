using System;
using UnityEngine;

public class RayEmitter : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    public event Action<RaycastHit> RayCollisioned;

    private void OnEnable()
    {
        _playerInput.LeftMouseButtonPressed += Raycast;
    }

    private void OnDisable()
    {
        _playerInput.LeftMouseButtonPressed -= Raycast;
    }

    private void Raycast()
    {
        Ray ray;

        ray = _camera.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask);

        if (isHit)
        {
            RayCollisioned?.Invoke(hit);
        }
    }
}
