using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StationClicker : MonoBehaviour
{
    [SerializeField] private StationContacter _stationContacter;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private MoneyController _moneyController;
    [SerializeField] private StaminaController _staminaController;

    [SerializeField] private CanvasGroup _pressCanvasGroup;
    [SerializeField] private CanvasGroup _coursesCanvasGroup;

    [SerializeField] private RectTransform _exitButtonRect;
    [SerializeField] private RectTransform _exitCoursesRect;

    [SerializeField] private RectTransform _cursorRect;
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _pressButtonRect;
    [SerializeField] private RectTransform _logoPress;
    [SerializeField] private RectTransform _logoCourses;

    private OpenedPanel _currentPanel;

    private enum OpenedPanel
    {
        nothing,
        pressButton,
        courses
    }

    public Action<InputAction.CallbackContext> MouseClicked;

    private void Start()
    {
        MouseClicked += (callback) => { ClickMouse(); };
        _stationContacter.OnStartedUsing += InitiateListening;
        _stationContacter.OnFinishedUsing += StopListening;

        _currentPanel = OpenedPanel.pressButton;
    }

    private void InitiateListening()
    {
        _inputManager.InputController.Player.LMB.started += MouseClicked;
    }

    private void StopListening()
    {
        _inputManager.InputController.Player.LMB.started -= MouseClicked;
    }

    private void ClickMouse()
    {
        _soundManager.Play("click_1");

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(_camera, _cursorRect.position);

        if (_currentPanel == OpenedPanel.nothing)
        {
            CheckLogoPress(screenPoint);
            CheckLogoCourses(screenPoint);
        } else if (_currentPanel == OpenedPanel.pressButton)
        {
            CheckPressButton(screenPoint);
            CheckExitButton(screenPoint);
        }
        else if (_currentPanel == OpenedPanel.courses)
        {
            CheckExitCourses(screenPoint);
        }

    }
    private void CheckPressButton(Vector2 screenPoint)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(_pressButtonRect, screenPoint, _camera))
        {
            if (_staminaController.CurrentStamina <= 3)
            {
                return;
            }
            _moneyController.AddMoney(1);
            _staminaController.TakeStamina(3);
            return;
        }
    }

    private void CheckExitButton(Vector2 screenPoint)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(_exitButtonRect, screenPoint, _camera))
        {
            _pressCanvasGroup.alpha = 0;
            _currentPanel = OpenedPanel.nothing;
            return;
        }
    }

    private void CheckLogoPress(Vector2 screenPoint)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(_logoPress, screenPoint, _camera))
        {
            _pressCanvasGroup.alpha = 1;
            _currentPanel = OpenedPanel.pressButton;
            return;
        }
    }

    private void CheckExitCourses(Vector2 screenPoint)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(_exitCoursesRect, screenPoint, _camera))
        {
            _coursesCanvasGroup.alpha = 0;
            _currentPanel = OpenedPanel.nothing;
            return;
        }
    }

    private void CheckLogoCourses(Vector2 screenPoint)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(_logoCourses, screenPoint, _camera))
        {
            _coursesCanvasGroup.alpha = 1;
            _currentPanel = OpenedPanel.courses;
            return;
        }
    }

}
