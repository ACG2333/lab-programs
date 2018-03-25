using UnityEngine;
using UnityEngine.UI;

public class AudiosControl : MonoBehaviour {

    [HideInInspector]
    public AudioSource[] audioEffects;
    public Slider audioSlider;

	void Start () {
        /*获取子物体中的音效*/
        audioEffects = GetComponentsInChildren<AudioSource>();

        /*游戏开始时初始化音量为设置好的音量*/
        foreach (var item in audioEffects)
        {
            item.volume = TotalManger.GetAudioEffectValue();
        }
        audioSlider.value = TotalManger.GetAudioEffectValue();
    }

    public void GetValues()
    {
        foreach (var item in audioEffects)
        {
            item.volume = audioSlider.value;
        }
        /*存储音量信息*/
        TotalManger.SaveAudioEffectValue(audioSlider.value);
    }
}
