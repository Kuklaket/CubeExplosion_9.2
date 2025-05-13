using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private RayCollisionCheck _rayCollisionCheck;
    [SerializeField] private Cube _cube;
    [SerializeField] private BoxCollider _spawnZone;

    private List<Rigidbody> _spawnedCubes = new List<Rigidbody>();
    private Cube _newCube;

    public event Action<Collider, List<Rigidbody>> SpawnCompleted;
    public event Action<Collider> CallExploder;

    private void Start()
    {
        if (_spawnZone == null)
            _spawnZone = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        _rayCollisionCheck.CorrectColliderHiting += Spawner;
    }

    private void OnDisable()
    {
        _rayCollisionCheck.CorrectColliderHiting -= Spawner;
    }

    private void Spawner(Collider collider)
    {
        int minCountCubes = 2;
        int maxCountCubes = 6;
        int minNumberForGeneration = 0;
        int maxNumberForGeneration = 100;
        int countNewCube = UnityEngine.Random.Range(minCountCubes, maxCountCubes + 1);
        int generatedNumber = UnityEngine.Random.Range(minNumberForGeneration, maxNumberForGeneration);
        bool spawnIsNeeded;
        Cube parentCube;
        Rigidbody newCubeRigidbody;

        Vector3 PositionForNewCude = GetRandomSpawnPoint();
        collider.TryGetComponent(out parentCube);
        spawnIsNeeded = generatedNumber < parentCube.ChanceDuplication;

        if (spawnIsNeeded && parentCube != null)
        {
            for (int i = 0; i < countNewCube; i++)
            {
                _newCube = Instantiate(_cube, PositionForNewCude, Quaternion.identity);
                _newCube.Init(collider.transform.localScale, parentCube.ChanceDuplication);

                _newCube.TryGetComponent<Rigidbody>(out newCubeRigidbody);
                _spawnedCubes.Add(newCubeRigidbody);
            }
        }
        else
        {
            parentCube.TryGetComponent<Rigidbody>(out newCubeRigidbody);
            CallExploder?.Invoke(collider);
        }

        SpawnCompleted?.Invoke(collider, _spawnedCubes);
        Cube.Destroy(collider.gameObject);
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
