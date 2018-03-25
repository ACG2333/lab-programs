using UnityEngine;

public class ManagerScore : MonoBehaviour {

    public int score;
    
	void Start () {
        score = 0;
	}

    /// <summary>
    /// 保存最高分
    /// </summary>
    public void SaveScore()
    {
        /*判断难度确定要保存的分数类型*/
        if (TotalManger.GetDifficulty() == "easy")
        {
            /*判断当前分数是否高于最高分*/
            if (TotalManger.GetHighScoreE() < score)
            {
                TotalManger.SaveHighScoreE(score);
            }
        }
        else if (TotalManger.GetDifficulty() == "normal")
        {
            /*判断当前分数是否高于最高分*/
            if (TotalManger.GetHighScoreN() < score)
            {
                TotalManger.SaveHighScoreN(score);
            }
        }
        else if (TotalManger.GetDifficulty() == "hard")
        {
            /*判断当前分数是否高于最高分*/
            if (TotalManger.GetHighScoreH() < score)
            {
                TotalManger.SaveHighScoreH(score);
            }
        }
    }
    /// <summary>
    /// 增加当前分数
    /// </summary>
    /// <param name="n">增加的分值</param>
    public void GetScore(int n)
    {
        score += n;
    }
}
