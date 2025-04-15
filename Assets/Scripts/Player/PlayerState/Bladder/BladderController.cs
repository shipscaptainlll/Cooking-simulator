using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BladderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bladderText;
    [SerializeField] private int _defaultBladderCount;
    [SerializeField] private bool _useDefaultBladder;
    [SerializeField] private RectTransform _frontRect;
    [SerializeField] private Transform _bladderNegativeRect;
    [SerializeField] private Transform _bladderPositiveRect;
    [SerializeField] private Transform _bladderHolder;

    public int CurrentBladder { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (_useDefaultBladder)
        {
            CurrentBladder = _defaultBladderCount;
            UpdateUI();
        }
    }

    public void AddBladder(int ammount)
    {
        CurrentBladder += ammount;

        if (CurrentBladder > 100)
        {
            CurrentBladder = 100;
        }

        /*GameObject newBladder = Instantiate(_bladderPositiveRect.gameObject, _bladderHolder);
        newBladder.SetActive(true);
        newBladder.GetComponent<TextMeshProUGUI>().text = $"+{ammount}";
*/
        UpdateUI();
    }

    public void TakeBladder(int ammount)
    {
        CurrentBladder -= ammount;

        /*GameObject newBladder = Instantiate(_bladderNegativeRect.gameObject, _bladderHolder);
        newBladder.SetActive(true);
        newBladder.GetComponent<TextMeshProUGUI>().text = $"-{ammount}";
*/
        UpdateUI();
    }

    private void UpdateUI()
    {
        _bladderText.text = $"Bladder: {CurrentBladder}/100";

        float targetWidth = Mathf.Lerp(0, 522.62f, (float)CurrentBladder / 100);

        _frontRect.sizeDelta = new Vector2(targetWidth, _frontRect.sizeDelta.y);
    }

}
