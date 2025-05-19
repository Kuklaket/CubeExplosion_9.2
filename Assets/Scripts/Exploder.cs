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
        _spawnerCube.SpawnCompleted += PushAllChilds;
        _spawnerCube.SpawnNotCompleted += PushAllCubes;
    }

    private void OnDisable()
    {
        _spawnerCube.SpawnCompleted -= PushAllChilds;
        _spawnerCube.SpawnNotCompleted -= PushAllCubes;
    }

    private void PushAllChilds(Collider cubePosition, List<Rigidbody> cubesForExplosion)
    {
        Vector3 explodePosition = cubePosition.transform.position;

        foreach (Rigidbody explodableCube in cubesForExplosion)
            if (explodableCube != null)
                explodableCube.AddExplosionForce(_force, explodePosition, _radius);

        cubesForExplosion.Clear();
    }

    private void PushAllCubes(Collider cube)
    {
        Vector3 explodePosition = cube.transform.position;
        Collider[] cubesForExplosion = Physics.OverlapSphere(explodePosition, _radius, _layerMask);
        float powerModifier = (cube.transform.localScale.x + cube.transform.localScale.y + cube.transform.localScale.z) / _countSides;

        foreach (Collider explodableCube in cubesForExplosion)
        {
            if (explodableCube.TryGetComponent<Rigidbody>(out Rigidbody cubeRigidbody))
            {
                float forñe = _force / powerModifier;
                float radius = _radius / powerModifier;

                cubeRigidbody.AddExplosionForce(forñe, explodePosition, radius);
            }
        }
    }
}
