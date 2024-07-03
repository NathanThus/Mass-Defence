using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NathanThus.MassDefence.Input
{
    public class TileSelection : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private Camera _camera;

        #endregion

        #region Fields
        private MassDefenceInputActions _massDefenceInputActions;
        private InputAction _leftMouseAction;
        private InputAction _rightMouseAction;

        private const float MaxDistance = 100f;
        #endregion

        #region Setup
        private void Awake()
        {
            _massDefenceInputActions = new();
            _leftMouseAction = _massDefenceInputActions.TowerSelection.LeftMouse;
            _rightMouseAction = _massDefenceInputActions.TowerSelection.RightMouse;
        }

        private void OnEnable()
        {
            _leftMouseAction.performed += HandleLeftMouseClick;
            _rightMouseAction.performed += HandleRightMouseClick;
            _massDefenceInputActions.Enable();
        }

        private void OnDisable()
        {
            _leftMouseAction.performed -= HandleLeftMouseClick;
            _rightMouseAction.performed -= HandleRightMouseClick;
            _massDefenceInputActions.Disable();
        }

        #endregion

        #region Private
        private void HandleRightMouseClick(InputAction.CallbackContext _)
        {
        }

        private void HandleLeftMouseClick(InputAction.CallbackContext _)
        {
            var mousePos = Mouse.current.position.ReadValue();
            if (!Physics.Raycast(_camera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y)),
                                out RaycastHit hit,
                                MaxDistance))
            {
                return;
            }

            Destroy(hit.collider.gameObject);
        }

        #endregion
    }
}
