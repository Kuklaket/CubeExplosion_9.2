using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private BoxCollider _spawnZone;

    private Cube _newCube;

    public List<Rigidbody> SpawnCubes(Collider collider, float chanceDuplication, int countNewCube, Vector3 positionForNewCude)
    {
        List<Rigidbody> spawnedCubes = new List<Rigidbody>();
        Rigidbody newCubeRigidbody;

        for (int i = 0; i < countNewCube; i++)
        {
            _newCube = Instantiate(_cube, positionForNewCude, Quaternion.identity);
            _newCube.Init(collider.transform.localScale, chanceDuplication);

            _newCube.TryGetComponent<Rigidbody>(out newCubeRigidbody);
            spawnedCubes.Add(newCubeRigidbody);
        }

        return spawnedCubes;
    }

    public Vector3 GetRandomSpawnPoint()
    {
        Bounds bounds = _spawnZone.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
