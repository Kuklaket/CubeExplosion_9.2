using System;
using UnityEngine;

public class RayCubeIntersector : MonoBehaviour
{
    [SerializeField] private RayEmitter _rayHandler;

    public event Action<Collider, Cube> CorrectColliderHiting;

    private void OnEnable()
    {
        _rayHandler.RayCollisioned += HandleCubeHit;
    }

    private void OnDisable()
    {
        _rayHandler.RayCollisioned -= HandleCubeHit;
    }

    private void HandleCubeHit(RaycastHit hit)
    {
        Cube cube;

        if (hit.collider.TryGetComponent<Cube>(out cube))
        {
            CorrectColliderHiting?.Invoke(hit.collider, cube);
        }
    }
}
