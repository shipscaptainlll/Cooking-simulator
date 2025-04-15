using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMixer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static IEnumerator SoundChanging(AudioSource audioSource, float startVolume, float endVolume, float duration)
    {

        float elapsed = 0;
        audioSource.volume = startVolume;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentVolume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            audioSource.volume = currentVolume;
            yield return null;
        }

    }

}
