using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private RayCubeIntersector _rayCollisionCheck;
    [SerializeField] private SpawnerCube _spawnerCubes;
    [SerializeField] private Exploder _exploder;


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
        List<Rigidbody> spawnedCubes = new List<Rigidbody>();

        int minCountCubes = 2;
        int maxCountCubes = 6;
        int minNumberForGeneration = 0;
        int maxNumberForGeneration = 100;

        int countNewCube = Random.Range(minCountCubes, maxCountCubes + 1);
        int chanceToDuplication = Random.Range(minNumberForGeneration, maxNumberForGeneration);


        Vector3 positionForNewCude = _spawnerCubes.GetRandomSpawnPoint();

        if (chanceToDuplication < parentCube.ChanceDuplication)
        {
            spawnedCubes = _spawnerCubes.SpawnCubes(collider, parentCube.ChanceDuplication, countNewCube, positionForNewCude);
            _exploder.PushAllChilds(collider, spawnedCubes);
        }
        else
        {
            _exploder.PushAllCubes(collider);
        }

        Destroy(collider.gameObject);
    }
}
