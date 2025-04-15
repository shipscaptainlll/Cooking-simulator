using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterDrinkingEffects : MonoBehaviour
{
    [SerializeField] private BladderController _bladderController;
    private Coroutine _bladderCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddEffects()
    {
        if (_bladderCoroutine != null)
        {
            StopCoroutine(_bladderCoroutine);
            _bladderCoroutine = null;
        }

        _bladderCoroutine = StartCoroutine(BladderDelayed());
    }

    private IEnumerator BladderDelayed()
    {
        float elapsed = 0;
        float duration = 7;

        while (elapsed < duration)
        {
            if (_bladderController.CurrentBladder > 1)
            {
                _bladderController.TakeBladder(1);
            }
            yield return new WaitForSeconds(2);
            elapsed++;
        }

        
    }

}
