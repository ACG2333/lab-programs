using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MangerOfMainMenu : MonoBehaviour {

    /// <summary>
    /// 用于给introduceOnUI赋值的共有类
    /// </summary>
    public Text PubintroduceOnUI;
    public GameObject mainCamera;

    private CameraFollow menuEvent;
    /// <summary>
    /// ShowIntroduceOnUI所用的静态类
    /// </summary>
    private static Text introduceOnUI;

    /*初始化*/
    void Awake()
    {
        TotalManger.ReSet();
    }

    void Start () {
        
        menuEvent = mainCamera.GetComponent<CameraFollow>();
        //给静态变量赋值
        introduceOnUI = PubintroduceOnUI;
	}

    /*退出游戏*/
    public void QuitGame() { Application.Quit(); }

    /*切换菜单*/
    /// <summary>
    /// 切换到介绍界面
    /// </summary>
    public void MenuToIntorduce() { menuEvent.cameraState = CameraFollow.MenuState.introductionMenu; }
    /// <summary>
    /// 切换到开始游戏界面
    /// </summary>
    public void MenuToIntoStarGame() { menuEvent.cameraState = CameraFollow.MenuState.startGameMenu; }
    /// <summary>
    /// 切换到设置界面
    /// </summary>
    public void MenuToOption() { menuEvent.cameraState = CameraFollow.MenuState.optionMenu; }
    /// <summary>
    /// 切换到成就界面
    /// </summary>
    public void MenuToAchievemen() { menuEvent.cameraState = CameraFollow.MenuState.achievementMenu; }

    /// <summary>
    /// 显示各个介绍信息在IntroduceUI上
    /// </summary>
    /// <param name="introduce">显示的内容</param>
    public static void ShowIntroduceOnUI(string introduce)
    {
        introduceOnUI.text = introduce;
    }
    /// <summary>
    /// 转换到无尽模式
    /// </summary>
    public void ToEndlessGameScene()
    {
        SceneManager.LoadScene("EndlessGame_scene", LoadSceneMode.Single);
    }

    /*设置难度*/
    public void SetDifficulty(string str)
    {
        TotalManger.SaveDifficulty(str);
    }
}
