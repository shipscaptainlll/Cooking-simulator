using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpatialSound : MonoBehaviour
{
    [SerializeField] private bool _initiateOnStart;
    [SerializeField] private string _name;
    public AudioSource AudioSource {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (_initiateOnStart)
        {
            AudioSource = SoundManager.instance.LocateAudioSource($"{_name}", transform);
            AudioSource.Play();
        }
    }

    public void PauseSound()
    {
        AudioSource.Pause();
    }

    public void ContinueWorking()
    {
        AudioSource.Play();
    }

}
