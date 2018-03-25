using UnityEngine;
using UnityEngine.UI;

public class ContrlButton : MonoBehaviour {

    public int score;

	void Update () {
        /*满足想象力点数按钮才可用*/
        if (score > Manager_Update.imageScore)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
	}
}
