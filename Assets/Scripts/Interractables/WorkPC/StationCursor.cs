using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCursor : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivityX;
    [SerializeField] private float _mouseSensitivityY;
    [SerializeField] private RectTransform cursorRectTransform;
    [SerializeField] private RectTransform panelRectTransform;
    [SerializeField] private Vector2 _positionMarginOffset;
    [SerializeField] private CanvasGroup _mouseCanvasGroup;
    [SerializeField] private StationContacter _stationContacter;
    [SerializeField] private InputManager _inputManager;
    private bool _isEnabled;

    private Vector2 inputVector;

    private void Start()
    {
        _stationContacter.OnStartedUsing += () => { StartCoroutine(ActivateDelayed()); };
        _stationContacter.OnFinishedUsing += () => { _isEnabled = false; };
    }

    private void HideCursor()
    {
        _mouseCanvasGroup.alpha = 0;
    }

    private void ShowCursor()
    {
        _mouseCanvasGroup.alpha = 1;
    }

    private IEnumerator ActivateDelayed()
    {
        yield return new WaitForSeconds(0.75f);
        _isEnabled = true;
    }

    void Update()
    {
        if (_isEnabled)
        {
            float mouseX = _inputManager.InputController.Player.MouseX.ReadValue<float>() * Time.deltaTime * _mouseSensitivityX;
            float mouseY = _inputManager.InputController.Player.MouseY.ReadValue<float>() * Time.deltaTime * _mouseSensitivityY;
            Vector2 newPosition = cursorRectTransform.anchoredPosition + new Vector2(mouseX, mouseY);

            newPosition.x = Mathf.Clamp(newPosition.x,
                (-panelRectTransform.rect.width / 2) + 0,
                (panelRectTransform.rect.width / 2) + 0);
            newPosition.y = Mathf.Clamp(newPosition.y,
                (-panelRectTransform.rect.height / 2),
                (panelRectTransform.rect.height / 2));

            cursorRectTransform.anchoredPosition = newPosition;
        }
    }
}
