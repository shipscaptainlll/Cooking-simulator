using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCooler : MonoBehaviour, IContactable
{
    [SerializeField] private StaminaController _staminaController;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _cupAnimator;
    [SerializeField] private AfterDrinkingEffects _afterDrinkingEffects;
    [SerializeField] private BladderController _bladderController;

    public bool DrinkEnabled { get; private set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetContacted()
    {
        if (!DrinkEnabled)
        {
            return;
        }
        /*if (_bladderController.CurrentBladder <= 30)
        {
            return;
        }*/

        DrinkWater();
    }

    private void DrinkWater()
    {
        DrinkEnabled = false;
        StartCoroutine(Drink());
        StartCoroutine(DelayDrinking());
    }

    private IEnumerator DelayDrinking()
    {
        yield return new WaitForSeconds(3.2f);
        DrinkEnabled = true;
    }

    private IEnumerator Drink()
    {
        _animator.Play("FillWater");
        yield return new WaitForSeconds(1);
        
        _soundManager.Play("bubbles_water_0");
        yield return new WaitForSeconds(1.6f);
        _cupAnimator.gameObject.SetActive(true);
        _cupAnimator.Play("Drink");
        yield return new WaitForSeconds(1.1f);
        _soundManager.Play("drink_0");
        yield return new WaitForSeconds(1.0f);
        _staminaController.AddStamina(50);
        //_bladderController.TakeBladder(7);
        //_afterDrinkingEffects.AddEffects();
        yield return new WaitForSeconds(0.4f);
        _cupAnimator.gameObject.SetActive(false);
        
    }


}
