using UnityEngine;
using UnityEngine.InputSystem;
using MobileGame.Move;
using System;

namespace MobileGame.Input
{
    public class PlayerBrain : MonoBehaviour
    {
        private InputActionAsset _inputActionAsset;
        private InputActionMap _inputGameplayActionMap;
        private Movement _movement;
        public static Action PauseHandler;
        public static Action JumpHandler;
        public static Action FailHandler;

        #region Unity Methods
        private void OnEnable()
        {
            _inputActionAsset.actionMaps[0].Enable();
            AddListeners();
        }

        private void Awake()
        {
            _inputActionAsset = GetComponent<PlayerInput>().actions;
            _inputGameplayActionMap = _inputActionAsset.actionMaps[0];
            _movement = GetComponent<Movement>();
        }

        private void OnDisable()
        {
            _inputActionAsset.actionMaps[0].Disable();
            RemoveListeners();
        }
        #endregion

        private void AddListeners()
        {
            if (_inputGameplayActionMap == null) return;

            _inputGameplayActionMap.actions[0].started += MoveHorizontal;

            _inputGameplayActionMap.actions[1].started += Pause;
        }

        private void RemoveListeners()
        {
            if (_inputGameplayActionMap == null) return;
            _inputGameplayActionMap.actions[0].started -= MoveHorizontal;

            _inputGameplayActionMap.actions[1].started -= Pause;

        }

        public void MoveHorizontal(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            _movement.Move(direction);
            Debug.Log(direction);
        }

        public void Pause(InputAction.CallbackContext context)
        {
            PauseHandler?.Invoke();
        }
    }

}
