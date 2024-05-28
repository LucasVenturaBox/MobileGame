using System.Collections;
using System.Collections.Generic;
using MobileGame.Input;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private List<GameObject> _gridCells;
    private float _obstacleSpawnHeight = 17f;
    private float _timeBetweenWaves = 3f;
    private Mesh _obstacleMesh;
    private Material _obstacleMaterial;
    private float _time;
    private bool _ready = false;
    private List<GameObject> _obstaclesInWave = new List<GameObject>();
    private int _prevExcludedIndex;
    private bool _stopped;

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

        if(_time >= _timeBetweenWaves && !_stopped)
        {
            SpawnObstacles();
            _time = 0;
            CheckWinningCondition();
        }
    }
    private void SpawnObstacles()
    {

        int excludedCell = Random.Range(0, _gridCells.Count - 1);

        foreach (var cell in _gridCells)
        {
            Debug.Log(cell.transform.position);

            if (cell != _gridCells[excludedCell])
            {
                CreateObstacle(cell.transform.position);
            }
        }

    }


    private void CreateObstacle(Vector3 place)
    {
        GameObject newObstacle = new GameObject();
        newObstacle.transform.position = place + Vector3.up * _obstacleSpawnHeight;

        newObstacle.layer = LayerMask.NameToLayer("Obstacle");

        newObstacle.AddComponent<MeshFilter>().mesh = _obstacleMesh;
        newObstacle.AddComponent<MeshRenderer>().material = _obstacleMaterial;

        newObstacle.AddComponent<BoxCollider>();
        newObstacle.AddComponent<Rigidbody>();

        newObstacle.AddComponent<Obstacle>()._parent = this;

        _obstaclesInWave.Add(newObstacle);
    }

    public void UpdateWaveState(Obstacle obstacleToRemove)
    {
        if(_obstaclesInWave.Count > 0) _obstaclesInWave.Remove(obstacleToRemove.gameObject);

        if(_obstaclesInWave.Count <= 0) PlayerBrain.JumpHandler?.Invoke();
    }

    private void CheckWinningCondition()
    {
        _timeBetweenWaves -= 0.1f; 
        Debug.Log(_timeBetweenWaves);
        if(_timeBetweenWaves <= 1.5f)
        {
            PlayerBrain.VictoryHandler?.Invoke();
            _stopped = true;
        }
    }

}
