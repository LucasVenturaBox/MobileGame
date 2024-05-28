using MobileGame.Input;
using UnityEngine;

namespace MobileGame.Move
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private GameObject _initialPosition;
        //Movement
        private Rigidbody _rigidbody;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float _detectionHeight = 0.5f;
        [SerializeField] private float _distanceMultiplier;
        private Ray _ray;
        private RaycastHit _hit;
        private bool _isOverSomething;

        private void Start() {
            
        }

        private void Update()
        {
            _ray = new Ray(transform.position, Vector3.down);
            _isOverSomething = Physics.Raycast(_ray, out _hit, _detectionHeight);
            Debugging();

            if(_isOverSomething && _hit.collider.gameObject.layer == 6)
            {
                Die();
            }
        }

        private void Debugging()
        {
            //Just debugging a ray
            if (_isOverSomething && _hit.collider.gameObject.layer == 3)
            {
                Debug.DrawRay(transform.position, Vector3.down * _detectionHeight, Color.green);
            }
            else if (_isOverSomething && _hit.collider.gameObject.layer == 6)
            {
                Debug.DrawRay(transform.position, Vector3.down * _detectionHeight, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, Vector3.down * _detectionHeight, Color.white);
            }
        }

        public void Move(Vector2 direction)
        {   

            if(!IsGrounded()) return;

            transform.position += (Vector3.right * direction.x + Vector3.forward * direction.y) * _distanceMultiplier;

        }

        private bool IsGrounded()
        {
            if(_isOverSomething && _hit.collider.gameObject.layer == 3)
            {
                return true;
            }

            return false;
        }

        public void Die()
        {    
            PlayerBrain.FailHandler?.Invoke();
            transform.position = _initialPosition.transform.position;
        }

    }
}
