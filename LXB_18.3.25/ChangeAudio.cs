using UnityEngine;
using UnityEngine.UI;

public class ChangeAudio : MonoBehaviour {
    
    /*音量滑条*/
    public Slider audioSlider;

    void Start () {
        GetComponent<AudioSource>().volume = TotalManger.GetMusicValue();
        audioSlider.value = TotalManger.GetMusicValue();
    }

    public void GetValue()
    {
        GetComponent<AudioSource>().volume = audioSlider.value;
        TotalManger.SaveMusicValue(audioSlider.value);
    }
}
