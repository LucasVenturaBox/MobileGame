using System.Collections.Generic;
using UnityEngine;

namespace MobileGame.Grid
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private float _gridCellSize = 0.25f;
        [SerializeField] private float _borderBetweenCells = 0.1f;
        [SerializeField] private int _gridColumnCount = 3;
        [SerializeField] private Mesh _cellMesh;
        [SerializeField] private Material _cellMaterial;

        [SerializeField] private Mesh _obstacleMesh;
        [SerializeField] private Material _obstacleMaterial;

        [SerializeField] private float _deathBorder = 2f;

        //For now only make the grid with the exact same number of rows and collumns

        private List<GameObject> _gridCells = new List<GameObject>();

        //Getter
        public float CellSize => _gridCellSize;
        public List<GameObject> GridCellPositions => _gridCells;

        private void Start()
        {
            PopulateGridList();
            CreateDeathZone();
            CreateObstacleSpawner();
        }

        private void PopulateGridList()
        {
            for (int i = 0; i < _gridColumnCount; i++)//Run through the different items in a collumn
            {
                for (int j = 0; j < _gridColumnCount; j++)//Run through the different items in a row
                {
                    float cellDistance = _gridCellSize + _borderBetweenCells;
                    Vector3 cellPosition = new Vector3(i * cellDistance, -_gridCellSize / 2 - 1, j * cellDistance);

                    GameObject newCell = new GameObject();

                    
                    newCell.transform.parent = transform;
                    newCell.AddComponent<MeshFilter>().mesh = _cellMesh;
                    newCell.AddComponent<MeshRenderer>().material = _cellMaterial;

                    newCell.transform.position = cellPosition;
                    newCell.transform.localScale = Vector3.one * _gridCellSize;

                    newCell.AddComponent<BoxCollider>();

                    newCell.name = "Cell (" + i +", "+ j + ")";
                    newCell.layer = LayerMask.NameToLayer("Ground");

                    _gridCells.Add(newCell);

                }
            }
        }

        private void CreateDeathZone()
        {
            float deathZoneSize = (_gridColumnCount + 1) * _gridCellSize + _gridColumnCount*_borderBetweenCells + _deathBorder*2;

            GameObject newDeathZone = new GameObject();

            Vector3 middleOfGrid = Vector3.Lerp(_gridCells[_gridCells.Count-1].transform.position,_gridCells[0].transform.position, 0.5f);

            newDeathZone.transform.parent = transform;
            newDeathZone.transform.position = middleOfGrid + Vector3.down*2;

            newDeathZone.AddComponent<BoxCollider>().size = new Vector3(deathZoneSize,1,deathZoneSize);

            newDeathZone.name = "DeathZone";
            newDeathZone.gameObject.layer = LayerMask.NameToLayer("DeathZone");
            
        }

        private void CreateObstacleSpawner()
        {
            GameObject newObstacleSpawner = new GameObject();
            newObstacleSpawner.name = "ObstacleSpawner";
            newObstacleSpawner.AddComponent<ObstacleSpawner>().InitializeObstacleSpawner(_gridCells,_obstacleMesh,_obstacleMaterial);
        }

    }
}
