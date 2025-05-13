using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private SpawnerCube _spawnerCube;
    [SerializeField] private LayerMask _layerMask;

    private int _countSides = 3;

    private void OnEnable()
    {
        _spawnerCube.SpawnCompleted += Explode;
        _spawnerCube.CallExploder += ExplodeAll;
    }

    private void OnDisable()
    {
        _spawnerCube.SpawnCompleted -= Explode;
    }

    public void Explode(Collider cubePosition, List<Rigidbody> cubesForExplosion)
    {
        Vector3 explodePosition = cubePosition.transform.position;

        foreach (Rigidbody explodableCube in cubesForExplosion)
            if (explodableCube != null)
                explodableCube.AddExplosionForce(_force, explodePosition, _radius);

        cubesForExplosion.Clear();
    }

    public void ExplodeAll(Collider cube)
    {
        Vector3 explodePosition = cube.transform.position;
        Collider[] cubesForExplosion = Physics.OverlapSphere(explodePosition, _radius, _layerMask);
        float powerModifier = (cube.transform.localScale.x + cube.transform.localScale.y + cube.transform.localScale.z) / _countSides;

        foreach (Collider explodableCube in cubesForExplosion)
        {
            if (explodableCube.TryGetComponent<Rigidbody>(out Rigidbody cubeRigidbody))
            {
                float forse = _force / powerModifier;
                float radius = _radius / powerModifier;

                cubeRigidbody.AddExplosionForce(forse, explodePosition, radius);

                Debug.Log("Модификатор силы взрыва " + powerModifier);
                Debug.Log("Сила взрыва " + forse);
                Debug.Log("Радиус взрыва " + radius);
            }
        }
    }
}
