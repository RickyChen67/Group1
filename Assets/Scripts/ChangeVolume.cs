using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider volSlider;
    public AudioListener player;

    public void changeVolume()
    {
        float newVol = AudioListener.volume;
        newVol = volSlider.value;
        AudioListener.volume = newVol;
    }
}
