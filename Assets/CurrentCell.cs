using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCell : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [Range(0,1), SerializeField] private float _borderSize = 0.13f;
    [SerializeField] private Color _color = Color.blue;

    [SerializeField] private float incrementationAmount = 0.1f;
    private Coroutine _moveCell;
    private PlayingArea _playingArea;

    private void Awake() 
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _playingArea = GetComponentInParent<PlayingArea>();
    }

    private void Start()
    {
        Vector3 localTransform =  transform.InverseTransformPoint(Vector3.one * _playingArea.CellSize);
        transform.localScale = localTransform;
    }
    
    void Update()
    {
        _meshRenderer?.material.SetFloat("_BorderSize", _borderSize);
        _meshRenderer?.material.SetColor("_Color", _color);
    }

    public void ChangePosition(Vector3 destination)
    {
        if(_moveCell != null)
        {
            StopCoroutine(_moveCell);
        }
        _moveCell = StartCoroutine(MoveHighlightedCell(destination));
    }

    private bool CheckDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination) < 0.05f;
    }

    private IEnumerator MoveHighlightedCell(Vector3 destination)
    {
        while(CheckDistance(destination))
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, incrementationAmount * Time.deltaTime);
            yield return null;
        }
    }
}
