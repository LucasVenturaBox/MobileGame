using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Delegates and events
    public delegate void InputDelegate();
    public static InputDelegate KeyInputDelegate;

    public delegate void PauseDelegate();
    public static PauseDelegate PauseHandler;

    //Movement
    private Rigidbody _rigidbody;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float smoothness = 5f;
    private Vector2 _horizontalMovement = Vector2.zero;

    private List<Vector3> _gridCellList = new List<Vector3>();

    private bool _setupDone = false;



    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() 
    {
        SetupInitialPosition();   
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            KeyInputDelegate?.Invoke();
        }

        _rigidbody.velocity = (Vector3.right * _horizontalMovement.x + Vector3.forward * _horizontalMovement.y) * speed * 50 * Time.deltaTime;
    }

    public void UpdateHorizontalDirection(Vector2 horizontalMovement)
    {
        if (!_setupDone) return;
       _horizontalMovement = horizontalMovement;
    }

    public void OpenPauseMenu()
    {
        PauseHandler?.Invoke();
    }

    private void IsGrounded()
    {
        if (CheckCollisionWithPlayArea() == null) return;

    }

    private void SetupInitialPosition()
    {
        if(CheckCollisionWithPlayArea()== null) return;
        float smallerRange = Mathf.Infinity;
        int closerCellIndex = 0;

        for (int i = 0; i < _gridCellList.Count; i++)
        {   
            float newDistance = Vector3.Distance(transform.position, _gridCellList[i]);
            Debug.Log(newDistance);
            if (newDistance < smallerRange)
            {
                smallerRange = newDistance;
                closerCellIndex = i;
            }
        }
        Debug.Log("Closer Cell Index is: " + closerCellIndex + " and smaller Range is " + smallerRange);
        transform.position = new Vector3(_gridCellList[closerCellIndex].x, transform.position.y, _gridCellList[closerCellIndex].z);
        _setupDone = true;
        
    }

    private List<Vector3> CheckCollisionWithPlayArea()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 3, LayerMask.GetMask("Ground"));

        if(hit.collider == null) 
        {
            Debug.Log("Didn't hit a collider with on the Ground LayerMask"); 
            return null;
        }
        
        PlayingArea playingArea = hit.transform.GetComponent<PlayingArea>();
        //_gridCellList = playingArea.GridCellPositions;
        return _gridCellList;
    }
   
}
