using UnityEngine;

public class TotalManger : MonoBehaviour
{
    /*音乐音量*/
    private static float musicValue = 0.35f;
    /*音效音量*/
    private static float audioEffectValue = 1;
    
    /*难度*/
    private static string difficulty = "easy";

    void Start()
    {
        /*转换场景不销毁*/
        DontDestroyOnLoad(gameObject);//实际上不执行
    }

    /*从硬盘中获取*/
    public static void ReSet()
    {
        /*从硬盘中获取存储的音量*/
        musicValue = PlayerPrefs.GetFloat("musicValue", 0.35f);
        audioEffectValue = PlayerPrefs.GetFloat("audioEffactValue", 1);
    }

    /*存储音量信息*/
    public static void SaveMusicValue(float value)
    {
        musicValue = value;
        PlayerPrefs.SetFloat("musicValue", value);
    }
    public static void SaveAudioEffectValue(float value)
    {
       audioEffectValue = value;
        PlayerPrefs.SetFloat("audioEffactValue", value);
    }
    /*获取音量信息*/
    public static float GetMusicValue()
    {
        return musicValue;
    }
    public static float GetAudioEffectValue()
    {
        return audioEffectValue;
    }

    /*存储难度信息*/
    public static void SaveDifficulty(string str)
    {
        difficulty = str;
    }
    /*获取难度信息*/
    public static string GetDifficulty()
    {
        return difficulty;
    }

    /*存储最高纪录*/
    public static void SaveHighScoreE(int score)
    {
        PlayerPrefs.SetInt("scoreE", score);
    }
    public static void SaveHighScoreN(int score)
    {
        PlayerPrefs.SetInt("scoreN", score);
    }
    public static void SaveHighScoreH(int score)
    {
        PlayerPrefs.SetInt("scoreH", score);
    }
    /*获取最高纪录*/
    public static int GetHighScoreE()
    {
        return PlayerPrefs.GetInt("scoreE", 0);
    }
    public static int GetHighScoreN()
    {
        return PlayerPrefs.GetInt("scoreN", 0);
    }
    public static int GetHighScoreH()
    {
        return PlayerPrefs.GetInt("scoreH", 0);
    }

    /*删除所有储存信息*/
    public static void DeleteAllSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
