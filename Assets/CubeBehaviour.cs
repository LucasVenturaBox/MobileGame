using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    [SerializeField] private float moveAmount = 10f;
    [SerializeField] private float speed = 2;
    private bool _looping = false;
    private Vector3 _initialPosition;

    private void OnEnable()
    {
        Movement.KeyInputDelegate += ChangeBehaviour;
    }
    private void Start() 
    {
        _initialPosition = transform.position;
    }
    private void OnDisable()
    {
        Movement.KeyInputDelegate -= ChangeBehaviour;
    }
   
    private void ChangeBehaviour()
    {
        if(!_looping)
        {
            _looping = true;
            StartCoroutine(LoopingMovement());

        }
        else
        {
            _looping = false;
            StopCoroutine(LoopingMovement());

        }
        
    }

    public IEnumerator LoopingMovement()
    {
        float time = 0;
        while(_looping)
        {
            time += Time.deltaTime;
            transform.position = _initialPosition + Vector3.up * Mathf.PingPong(time* speed , moveAmount);
            yield return null;
        }
    }

}
