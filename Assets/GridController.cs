using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayingArea : MonoBehaviour
{
    [SerializeField] private float _gridCellSize = 0.25f;
    [SerializeField] private float _borderBetweenCells = 0.1f;
    [SerializeField] private int _gridColumnCount = 3;
    [SerializeField] private Mesh _cellMesh;
    [SerializeField] private Material _cellMaterial;

    //For now only make the grid with the exact same number of rows and collumns

    private List<GameObject> _gridCellPositions = new List<GameObject>();

    //Getter
    public float CellSize => _gridCellSize;
    public List<GameObject> GridCellPositions => _gridCellPositions;
    
    private void Start() {
        
        PopulateGridList();
    }

    private void PopulateGridList()
    {
        for (int i = 0; i < _gridColumnCount; i++)//Run through the different items in a collumn
        {
            for (int j = 0; j < _gridColumnCount; j++)//Run through the different items in a row
            {
                float cellDistance = _gridCellSize+_borderBetweenCells;
                Vector3 cellPosition = new Vector3(i*cellDistance, -_gridCellSize/2 -1 , j*cellDistance);

                GameObject cellCreation = new GameObject();

                cellCreation.AddComponent<MeshFilter>().mesh = _cellMesh;
                cellCreation.AddComponent<MeshRenderer>().material = _cellMaterial;

                cellCreation.transform.position = cellPosition;
                cellCreation.transform.localScale = Vector3.one * _gridCellSize;

                cellCreation.AddComponent<BoxCollider>();

                GameObject newCell = Instantiate(cellCreation,transform,true);

                _gridCellPositions.Add(newCell);

            }
        }
    }
}
