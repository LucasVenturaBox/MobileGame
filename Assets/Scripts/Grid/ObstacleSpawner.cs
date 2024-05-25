using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private List<GameObject> _gridCells;
    private float _obstacleSpawnHeight = 10;
    private float _timeBetweenWaves = 5f;
    private Mesh _obstacleMesh;
    private Material _obstacleMaterial;
    private float _time;
    private bool _ready = false;

    public void InitializeObstacleSpawner(List<GameObject> cellList, Mesh obstacleMesh, Material obstacleMaterial)
    {
        _gridCells = cellList;
        _obstacleMesh = obstacleMesh;
        _obstacleMaterial = obstacleMaterial;
    }

    private void Update() 
    {
        _time += Time.deltaTime;

        if(!_ready)
        {
            if(_time > 2f)
            {
                _ready = true;
                _time = _timeBetweenWaves;
            }

            return;
        }

        if(_time >= _timeBetweenWaves)
        {
            SpawnObstacles();
            _time = 0;
        }
    }
    private void SpawnObstacles()
    {
        int excludedCell = Random.Range(0, _gridCells.Count -1);

        foreach (var cell in _gridCells)
        {
            if(cell != _gridCells[excludedCell])
            {
                CreateObstacle(cell.transform.position);
            }
        }
    }

    private void CreateObstacle(Vector3 place)
    {
        GameObject newObstacle = new GameObject();
        newObstacle.transform.position = place + Vector3.up * _obstacleSpawnHeight;

        newObstacle.AddComponent<MeshFilter>().mesh = _obstacleMesh;
        newObstacle.AddComponent<MeshRenderer>().material = _obstacleMaterial;

        newObstacle.AddComponent<Rigidbody>();
    }

}
