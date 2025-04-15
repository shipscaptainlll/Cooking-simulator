using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _staminaText;
    [SerializeField] private int _defaultStaminaCount;
    [SerializeField] private bool _useDefaultStamina;
    [SerializeField] private RectTransform _frontRect;
    [SerializeField] private Transform _staminaNegativeRect;
    [SerializeField] private Transform _staminaPositiveRect;
    [SerializeField] private Transform _staminaHolder;

    public int CurrentStamina {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (_useDefaultStamina)
        {
            CurrentStamina = _defaultStaminaCount;
            UpdateUI();
        }
    }

    public void AddStamina(int ammount)
    {
        CurrentStamina += ammount;

        if (CurrentStamina > 100)
        {
            CurrentStamina = 100;
        }

        GameObject newStamina = Instantiate(_staminaPositiveRect.gameObject, _staminaHolder);
        newStamina.SetActive(true);
        newStamina.GetComponent<TextMeshProUGUI>().text = $"+{ammount}";

        UpdateUI();
    }

    public void TakeStamina(int ammount)
    {
        CurrentStamina -= ammount;

        GameObject newStamina = Instantiate(_staminaNegativeRect.gameObject, _staminaHolder);
        newStamina.SetActive(true);
        newStamina.GetComponent<TextMeshProUGUI>().text = $"-{ammount}";

        UpdateUI();
    }

    private void UpdateUI()
    {
        _staminaText.text = $"Stamina: {CurrentStamina}/100";

        float targetWidth = Mathf.Lerp(0, 522.62f, (float)CurrentStamina / 100);

        _frontRect.sizeDelta = new Vector2(targetWidth, _frontRect.sizeDelta.y);
    }


}
