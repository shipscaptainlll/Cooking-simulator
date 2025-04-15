using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _checkSphere;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private PlayerBlocker _playerBlocker;
    private bool _isGrounded;

    float forwardInput;
    float horizontalInput;
    private Vector3 _move;
    private Vector3 _verticalVelocity;
    private const float GRAVITY = -9.8f;

    // Start is called before the first frame update
    void Start()
    {
        //_inputManager.InputController.Player.W.performed += (callback) => { MoveForward(); };
    }


    // Update is called once per frame
    void Update()
    {
        if (_playerBlocker.MovementBlocked)
        {
            return;
        }

        CheckIfGrounded();
        ImpactByGravity();
        UpdateInputs();
    }

    private void ImpactByGravity()
    {
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }

    private void CheckIfGrounded()
    {
        if (Physics.CheckSphere(_checkSphere.position, _groundCheckRadius, _groundLayerMask))
        {
            _isGrounded = true;
            if (_verticalVelocity.y < GRAVITY)
            {
                _verticalVelocity.y = GRAVITY;
            }
        }
        else
        {
            _isGrounded = false;
            _verticalVelocity.y += 2f * GRAVITY * Time.deltaTime;
        }
    }

    private void UpdateInputs()
    {
        forwardInput = _inputManager.InputController.Player.W.IsPressed() ? 1 : 0;
        forwardInput = _inputManager.InputController.Player.S.IsPressed() ? forwardInput - 1 : forwardInput;

        horizontalInput = _inputManager.InputController.Player.A.IsPressed() ? -1 : 0;
        horizontalInput = _inputManager.InputController.Player.D.IsPressed() ? horizontalInput + 1 : horizontalInput;

        _move = _targetTransform.forward * forwardInput * _moveSpeed + _targetTransform.right * horizontalInput * _moveSpeed;

        _characterController.Move(_move * Time.deltaTime);
    }

}
