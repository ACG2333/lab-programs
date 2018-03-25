using UnityEngine;
using UnityEngine.UI;

public class ShowHighScore : MonoBehaviour {

    public Text easyScore;
    public Text normalScore;
    public Text hardScore;
	
    void Start()
    {
        easyScore.text = TotalManger.GetHighScoreE() + "";
        normalScore.text = TotalManger.GetHighScoreN() + "";
        hardScore.text = TotalManger.GetHighScoreH() + "";
    }
}
