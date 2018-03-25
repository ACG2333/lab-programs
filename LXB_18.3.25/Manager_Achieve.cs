//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;

//public class Manager_Achieve : MonoBehaviour {

//    /// <summary>
//    /// 成就清单
//    /// </summary>
//    public static Achievements achievementList;

//    void Update()
//    {

//    }

//    void Start()
//    {
//        /*第一次运行游戏时创造成就*/
//        if (PlayerPrefs.GetString("TheFirst", "false") == "false")
//        {
//            achievementList.achievements.Add(new Achievement(1, "你在发呆吗?", "死亡后不做任何操作直到血量小于-2000"));
//            achievementList.achievements.Add(new Achievement(2, "美梦成真", "美梦难度下分数超过2000"));
//            achievementList.achievements.Add(new Achievement(3, "噩梦的开始", "噩梦难度下分数超过3000"));
//            achievementList.achievements.Add(new Achievement(4, "噩梦萦绕", "噩梦难度下分数超过5000"));
//            achievementList.achievements.Add(new Achievement(5, "地狱的磨练", "炼狱难度下分数超过5000"));
//            achievementList.achievements.Add(new Achievement(6, "地狱崩坏", "炼狱难度下分数超过10000"));
//            achievementList.achievements.Add(new Achievement(7, "噩梦的开始", "噩梦难度下分数超过3000"));
//            achievementList.achievements.Add(new Achievement(8, "沉默的杀手", "在不开一枪的情况下度过一晚上"));

//            PlayerPrefs.SetString("TheFirst", "true");
//        }
//    }

//    void Awake()
//    {
//        /*实例化*/
//        achievementList = new Achievements()
//        {
//            achievements = new List<Achievement>()
//        };

//        /*判断成就文件是否存在*/
//        if(File.Exists(Application.dataPath + @"\achievement.json"))
//        {
//            /*读取成就*/
//            achievementList = JsonUtility.FromJson<Achievements>(File.ReadAllText(Application.dataPath + @"\achievement.json"));
//        }
//    }

//    /// <summary>
//    /// 保存成就获得成就情况
//    /// </summary>
//    public void SaveAchievement()
//    {
//        /*写入json文件*/
//        File.WriteAllText(Application.dataPath + @"\achievement.json", JsonUtility.ToJson(achievementList));
//    }
//}

///*成就清单*/
//public class Achievements
//{
//    public List<Achievement> achievements;
//}

///*单个成就*/
//[System.Serializable]
//public class Achievement
//{
//    /*成就的ID*/
//    public int id;
//    /*成就的名称*/
//    public string name;
//    /*成就的描述*/
//    public string description;
//    /*成就获得情况*/
//    public bool got;

//    /*构造函数*/
//    public Achievement(int theID, string theName, string theDescr)
//    {
//        id = theID;
//        name = theName;
//        description = theDescr;
//        got = false;
//    }
//}