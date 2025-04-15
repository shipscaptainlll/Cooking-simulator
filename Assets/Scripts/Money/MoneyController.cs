using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private int _startMoney;
    [SerializeField] private bool _useStartMoney;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Transform _moneyAppearPositive;
    [SerializeField] private Transform _moneyAppearNegative;
    [SerializeField] private Transform _moneyAppearHolder;
    public int CurrentMoney {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (_useStartMoney)
        {
            CurrentMoney = _startMoney;
            UpdateUI();
        }
    }

    public void AddMoney(int ammount)
    {
        CurrentMoney += ammount;
        UpdateUI();
        GameObject newMoneyAppear = Instantiate(_moneyAppearPositive.gameObject, _moneyAppearHolder);
        newMoneyAppear.SetActive(true);
        newMoneyAppear.GetComponent<TextMeshProUGUI>().text = $"+{ammount}$";
    }

    public void TakeMoney(int ammount)
    {
        CurrentMoney -= ammount;
        UpdateUI();
        GameObject newMoneyAppear = Instantiate(_moneyAppearNegative.gameObject, _moneyAppearHolder);
        newMoneyAppear.SetActive(true);
        newMoneyAppear.GetComponent<TextMeshProUGUI>().text = $"+-{ammount}$";
    }

    private void UpdateUI()
    {
        _moneyText.text = $"{CurrentMoney.ToString()}$";
    }


}
