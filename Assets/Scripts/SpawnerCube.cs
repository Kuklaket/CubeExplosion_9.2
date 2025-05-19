using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private RayCubeIntersector _rayCollisionCheck;
    [SerializeField] private Cube _cube;
    [SerializeField] private BoxCollider _spawnZone;

    private List<Rigidbody> _spawnedCubes = new List<Rigidbody>();
    private Cube _newCube;

    public event Action<Collider, List<Rigidbody>> SpawnCompleted;
    public event Action<Collider> SpawnNotCompleted;

    private void Awake()
    {
        if (_spawnZone == null)
            _spawnZone = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        _rayCollisionCheck.CorrectColliderHiting += TrySpawnNewCubesAndExplode;
    }

    private void OnDisable()
    {
        _rayCollisionCheck.CorrectColliderHiting -= TrySpawnNewCubesAndExplode;
    }

    private void TrySpawnNewCubesAndExplode(Collider collider, Cube parentCube)
    {
        int minCountCubes = 2;
        int maxCountCubes = 6;
        int minNumberForGeneration = 0;
        int maxNumberForGeneration = 100;

        int countNewCube = UnityEngine.Random.Range(minCountCubes, maxCountCubes + 1);
        int generatedNumber = UnityEngine.Random.Range(minNumberForGeneration, maxNumberForGeneration);
    
        Vector3 positionForNewCude = GetRandomSpawnPoint();

        if (generatedNumber < parentCube.ChanceDuplication)
        {
            _spawnedCubes = SpawnCubes(collider, parentCube.ChanceDuplication,  countNewCube, positionForNewCude);
            SpawnCompleted?.Invoke(collider, _spawnedCubes);
        }
        else
        {
            SpawnNotCompleted?.Invoke(collider);
        }

        Destroy(collider.gameObject);
    }

    private List<Rigidbody> SpawnCubes(Collider collider, float chanceDuplication, int countNewCube, Vector3 positionForNewCude)
    {
        Rigidbody newCubeRigidbody;

        for (int i = 0; i < countNewCube; i++)
        {
            _newCube = Instantiate(_cube, positionForNewCude, Quaternion.identity);
            _newCube.Init(collider.transform.localScale, chanceDuplication);

            _newCube.TryGetComponent<Rigidbody>(out newCubeRigidbody);
            _spawnedCubes.Add(newCubeRigidbody);
        }

        return _spawnedCubes;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Bounds bounds = _spawnZone.bounds;

        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
