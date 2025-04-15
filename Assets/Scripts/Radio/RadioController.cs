using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour, IContactable
{
    [SerializeField] private SoundManager _soundManager;
    private List<string> _tracks = new List<string>();
    private AudioSource _currentTrack;

    public int CurrentTrackIndex { get; private set; } 

    // Start is called before the first frame update
    void Start()
    {
        _tracks.Add("lo-fi_1");
        _tracks.Add("lo-fi_2");
        _tracks.Add("lo-fi_3");

        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        if (_currentTrack != null)
        {
            _currentTrack.Stop();
        }

        string nextTrack = _tracks[CurrentTrackIndex];
        _currentTrack = _soundManager.FindSound(nextTrack);
        _currentTrack.Play();

        CurrentTrackIndex++;
        if (CurrentTrackIndex >= _tracks.Count)
        {
            CurrentTrackIndex = 0;
        }
    }

    public void GetContacted()
    {
        _soundManager.Play("click_0");
        PlayNextTrack();
    }


}
