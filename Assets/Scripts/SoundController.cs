using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{
    private float _currentVolume;
    public void Start()
    { 
        _currentVolume = AudioListener.volume;   
    }
    public void SwitchSound()
    {
        if (AudioListener.volume==_currentVolume)
            AudioListener.volume = 0f;
        else
            AudioListener.volume = _currentVolume;
    }
}

