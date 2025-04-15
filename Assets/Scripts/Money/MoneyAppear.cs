using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAppear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DestroyAfterDelay();
    }

    private void DestroyAfterDelay()
    {
        StartCoroutine(DestroyDelayed());
    }

    private IEnumerator DestroyDelayed()
    {
        yield return new WaitForSeconds(1.15f);
        Destroy(gameObject);
    }


}
