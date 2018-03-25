using UnityEngine;

public class AudioDestory : MonoBehaviour {

    private bool havePlay = false;

	void Update () {

        /*播放音效*/
        if (GetComponent<AudioSource>().isPlaying)
        {
            havePlay = true;
        }

        /*播放完之后删除*/
        if(havePlay&& !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
	}
}
