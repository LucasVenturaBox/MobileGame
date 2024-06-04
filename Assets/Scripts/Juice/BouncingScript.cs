using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BounceType
{
    HorizontalTranslate,
    VerticalTranslate,
    UniformScale,
}

public class BouncingScript : MonoBehaviour
{
    [SerializeField] private BounceType bounceType;
    [SerializeField] private float bounceAmount;
    [SerializeField] private float speed;

    [Header("Scaling")]
    [SerializeField] private float minimumScale;
    [SerializeField] private float maximumScale;


    private Vector3 _initialTransformPosition;
    private float _initialScale;
    private float _time;
    private bool _canStartBounce = false;

    private const float SINGLE_BOUNCE_AMOUNT = Mathf.PI*2;
    private void Start() 
    {
        _initialTransformPosition = transform.position;
        _initialScale = transform.localScale.magnitude;
    }

    private void Update() 
    {
        _time += Time.deltaTime;

        float Rhythm = Mathf.PingPong(_time * speed, SINGLE_BOUNCE_AMOUNT);

        switch (bounceType)
        {
            case BounceType.HorizontalTranslate:
                HorizontalTranslate(Rhythm);
                break;

            case BounceType.VerticalTranslate:
                VerticalTranslate(Rhythm);
                break;

            case BounceType.UniformScale:
                UniformScale(Rhythm);
                break;

            default:
                HorizontalTranslate(Rhythm);
                break;

        }
    }

    private void HorizontalTranslate(float amount)
    {

        float translationValue = Mathf.Cos(amount);

        Vector3 newPosition = new Vector3 (_initialTransformPosition.x + translationValue* bounceAmount, transform.position.y, transform.position.z);
        transform.position = newPosition;

    }

    private void VerticalTranslate(float amount)
    {
       float translationValue = Mathf.Sin(amount);

        Vector3 newPosition = new Vector3 (transform.position.x, _initialTransformPosition.y + translationValue* bounceAmount, transform.position.z);
        transform.position = newPosition;
    }

    private void UniformScale(float amount)
    {
        float scaleValue = Mathf.Cos(amount);

        float newScale = Mathf.Lerp(minimumScale, maximumScale, Mathf.Abs(scaleValue));
        transform.localScale = Vector3.one * newScale;
    }
}
