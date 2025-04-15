using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StationContacter : MonoBehaviour, IContactable
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _target;
    [SerializeField] private InputManager _inputManager;
    private Vector3 _cameraStartPosition;
    private Quaternion _cameraStartRotation;

    private bool IsTransitioning;


    public Action<InputAction.CallbackContext> TransitionBackCall;
    public event Action OnStartedUsing = delegate { };
    public event Action OnFinishedUsing = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        TransitionBackCall += (callback) => { CloseStation(); };
    }

    public void GetContacted()
    {
        OpenStation();
    }

    private void OpenStation()
    {
        if (IsTransitioning) { return; }
        _inputManager.InputController.Player.Esc.started += TransitionBackCall;
        OnStartedUsing?.Invoke();
        TransitionPlayerTowards();
    }

    private void CloseStation()
    {
        if (IsTransitioning) { return; }
        _inputManager.InputController.Player.Esc.started -= TransitionBackCall;
        TransitionPlayerBack();
    }

    private void TransitionPlayerTowards()
    {
        StartCoroutine(TransitionTowardsDelayed());
        IsTransitioning = true;
    }

    private void TransitionPlayerBack()
    {
        StartCoroutine(TransitionBackDelayed());
        IsTransitioning = true;
    }

    private IEnumerator TransitionTowardsDelayed()
    {
        float elapsed = 0f;
        float duration = 0.58f;

        _cameraStartPosition = _playerCamera.transform.position;
        _cameraStartRotation = _playerCamera.transform.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _playerCamera.transform.position = Vector3.Lerp(_cameraStartPosition, _target.position, elapsed / duration);
            _playerCamera.transform.rotation = Quaternion.Lerp(_cameraStartRotation, _target.rotation, elapsed / duration);
            yield return null;
        }

        _playerCamera.transform.position = _target.position;
        _playerCamera.transform.rotation = _target.rotation;

        IsTransitioning = false;
    }

    private IEnumerator TransitionBackDelayed()
    {
        float elapsed = 0f;
        float duration = 0.45f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _playerCamera.transform.position = Vector3.Lerp(_target.position, _cameraStartPosition, elapsed / duration);
            _playerCamera.transform.rotation = Quaternion.Lerp(_target.rotation, _cameraStartRotation, elapsed / duration);
            yield return null;
        }

        _playerCamera.transform.position = _cameraStartPosition;
        _playerCamera.transform.rotation = _cameraStartRotation;

        IsTransitioning = false;

        OnFinishedUsing?.Invoke();
    }

}
