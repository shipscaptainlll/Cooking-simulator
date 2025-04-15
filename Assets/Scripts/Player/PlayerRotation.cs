using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _minVerticalAngle;
    [SerializeField] private float _maxVerticalAngle;
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private PlayerBlocker _playerBlocker;

    float _xRotation;
    float _yRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateHead();


    }

    private void RotateHead()
    {
        if (_playerBlocker.RotationBlocked)
        {
            return;
        }

        float mouseX = _inputManager.InputController.Player.MouseX.ReadValue<float>() * _mouseSensitivity;
        float mouseY = _inputManager.InputController.Player.MouseY.ReadValue<float>() * _mouseSensitivity;

        _xRotation -= mouseX * 0.0001f;
        _yRotation -= mouseY * 0.0006f;

        _yRotation = Mathf.Clamp( _yRotation,_maxVerticalAngle,_minVerticalAngle);

        _cameraTransform.localRotation = Quaternion.Euler(_yRotation, 0, 0);
        _bodyTransform.Rotate(Vector3.up * mouseX * 0.00055f);
    }
}
