using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour {

    /*升级菜单*/
    public GameObject updateUI;
    /*暂停菜单*/
    public GameObject pauseUi;
    /*死亡菜单*/
    public GameObject deathUI;
    /*角色*/
    public GameObject player;
    /*游戏UI*/
    public GameObject gamingUI;
    /*分数管理器*/
    public GameObject scoreManager;
    /*火把*/
    public GameObject fire;
    /*辅助瞄准*/
    public GameObject assist1;
    public GameObject assist2;
    public GameObject assist3;

    public enum State
    {
        death,
        options,
        help,
        gaming,
        pausing,
        update
    }

    public static State GameState;

    void Awake()
    {
        TotalManger.ReSet();
    }

	void Start () {
        GameState = State.gaming;
	}
	
	void Update () {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Manager_Update.imageScore += 200;
        }

        /*死亡显示死亡界面*/
        if(player.GetComponent<Life_Player_EndlessGame>().hp<0)
        {
            deathUI.SetActive(true);
            gamingUI.SetActive(false);
            scoreManager.GetComponent<ManagerScore>().SaveScore();
            OnDeath();
        }

        /*按下Esc*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameState == State.gaming)
            {
                /*正在游戏就切换成暂停*/
                pauseUi.SetActive(true);
                OnPausing();
                Time.timeScale = 0;
            }
            else if(GameState == State.pausing)
            {
                /*正在暂停就切换为游戏*/
                pauseUi.SetActive(false);
                OnGaming();
                Time.timeScale = 1;
            }
            else if(GameState == State.update)
            {
                /*正在升级就切换为游戏*/
                updateUI.SetActive(false);
                OnGaming();
            }
        }

        /*按下E打开关闭升级菜单*/
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (updateUI.activeSelf)
            {
                updateUI.SetActive(false);
                OnGaming();
            }
            else
            {
                updateUI.SetActive(true);
                OnUpdate();
            }
        }

        /*开关道具*/
        if(Manager_Update.getFire)
        {
            /*获得火把的情况下 按F开关火把*/
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (fire.activeSelf)
                    fire.SetActive(false);
                else
                    fire.SetActive(true);
            }
        }
        if(Manager_Update.getAssist)
        {
            /*如果获取辅助瞄准 按T开关辅助瞄准*/
            if(Input.GetKeyDown(KeyCode.T))
            {
                if (assist1.activeSelf)
                {
                    assist1.SetActive(false);
                    assist2.SetActive(false);
                    assist3.SetActive(false);
                }
                else
                {
                    assist1.SetActive(true);
                    assist2.SetActive(true);
                    assist3.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// 切换为设置状态
    /// </summary>
    public void OnOptions()
    {
        GameState = State.options;
    }
    /// <summary>
    /// 切换为帮助状态
    /// </summary>
    public void OnHelp()
    {
        GameState = State.help;
    }
    /// <summary>
    /// 切换为暂停菜单状态
    /// </summary>
    public void OnPausing()
    {
        GameState = State.pausing;
    }
    /// <summary>
    /// 切换为游戏状态
    /// </summary>
    public void OnGaming()
    {
        GameState = State.gaming;
    }
    /// <summary>
    /// 切换为死亡状态
    /// </summary>
    public void OnDeath()
    {
        GameState = State.death;
    }
    /// <summary>
    /// 切换为升级菜单状态
    /// </summary>
    public void OnUpdate()
    {
        GameState = State.update;
    }

    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Start_scene", LoadSceneMode.Single);
        Time.timeScale = 1;//很重要！！不然返回主菜单后timeScale仍然是0
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// 返回游戏
    /// </summary>
    public void BackGame()
    {
        pauseUi.SetActive(false);
        GameState = State.gaming;
        Time.timeScale = 1;
    }
    /// <summary>
    /// 重新加载场景
    /// </summary>
    public void Replay()
    {
        SceneManager.LoadScene("EndlessGame_scene");
    }
}
