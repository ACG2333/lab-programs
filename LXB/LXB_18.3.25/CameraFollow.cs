using UnityEngine;

public class CameraFollow : MonoBehaviour {

    /*菜单状态*/
    public enum MenuState
    {
        firstOfAll,
        achievementMenu,
        optionMenu,
        startGameMenu,
        introductionMenu
    }

    /*状态*/
    public MenuState cameraState = MenuState.firstOfAll;

    /*各界面菜单UI画布*/
    public Canvas introduceUI;
    public Canvas achievementUI;
    public Canvas optionsUI;
    public Canvas startGameUI;

    /*各界面菜单对焦位置*/
    public GameObject menuFirst;
    public GameObject menuOption;
    public GameObject menuAchievement;
    public GameObject menuStartGame;
    
    /*介绍界面各个物品对焦的位置*/
    public GameObject targetPlayer;
    public GameObject targetEnemy1;
    public GameObject targetEnemy2;
    public GameObject targetEnemy3;
    public GameObject targetWeapon1;
    public GameObject targetWeapon2;
    public GameObject targetWeapon3;
    public GameObject targetWeapon4;
    public GameObject targetItem1;
    public GameObject targetItem2;
    public GameObject targetItem3;

    /*用于对焦物体的数组*/
    private GameObject[] targets = new GameObject[11];
    private string[] introduces = new string[11];
    private short index = 0;

    void Awake()
    {
        /*开启时关闭所有的UI，除了mainUI*/
        introduceUI.enabled = false;
        achievementUI.enabled = false;
        optionsUI.enabled = false;
        startGameUI.enabled = false;
    }

	void Start ()
    {
        {
            targets[0] = targetPlayer;
            targets[1] = targetEnemy1;
            targets[2] = targetEnemy2;
            targets[3] = targetEnemy3;
            targets[4] = targetWeapon1;
            targets[5] = targetWeapon2;
            targets[6] = targetWeapon3;
            targets[7] = targetWeapon4;
            targets[8] = targetItem1;
            targets[9] = targetItem2;
            targets[10] = targetItem3;
        }//给对焦数组赋值

        {
            introduces[0] = "--主角--\n\n哈哈哈哈";
            introduces[1] = "--蓝兔--\n\n速度快、血量少、攻击力小";
            introduces[2] = "--红猫（熊）--\n\n速度一般、血量一般、攻击力一般";
            introduces[3] = "--黄大奔（象）--\n\n速度慢、血量高、攻击力高";
            introduces[4] = "--自动步枪--\n\n按1切换，后坐力小，伤害一般\n长按左键攻击";
            introduces[5] = "--手榴弹发射器--\n\n按3切换，后座力小，爆炸伤害\n单击左键攻击";
            introduces[6] = "--霰弹枪--\n\n按2切换，后座力大，全部集中时攻击力强\n单击左键攻击";
            introduces[7] = "--定时炸弹--\n\n按G投掷，威力大，范围广";
            introduces[8] = "--弹药箱--\n\n给已获取的武器增加弹药（不超过弹药上限）";
            introduces[9] = "--薯条--\n\n可恢复15血量（不超过血量上限）";
            introduces[10] = "--奶酪--\n\n可以恢复35血量（不超过血量上限）";
        }//给对应对焦物体的介绍信息赋值
    }

    void Update() {

        /*摄像机距目标对象的距离*/
        float distance;

        /*视角控制&&UI控制*/
        if (cameraState == MenuState.firstOfAll)
        {
            CameraMoveTo(menuFirst);
        }//刚进入游戏的界面
        else if (cameraState == MenuState.optionMenu)
        {
            CameraMoveTo(menuOption);

            /*打开UI*/
            distance = Vector3.Distance(transform.position, menuOption.transform.position);
            if (distance < 3f)
            {
                optionsUI.enabled = true;
            }
        }//设置界面
        else if (cameraState == MenuState.achievementMenu)
        {
            CameraMoveTo(menuAchievement);

            /*打开UI*/
            distance = Vector3.Distance(transform.position, menuAchievement.transform.position);
            if (distance < 3f)
            {
                achievementUI.enabled = true;
            }
        }//成就界面
        else if (cameraState == MenuState.startGameMenu)
        {
            CameraMoveTo(menuStartGame);
            /*打开UI*/
            distance = Vector3.Distance(transform.position, menuStartGame.transform.position);
            if (distance < 3f)
            {
                startGameUI.enabled = true;
            }
        }//开始游戏界面
        else if (cameraState == MenuState.introductionMenu)
        {
            /*打开UI*/
            distance = Vector3.Distance(transform.position, targets[index].transform.position);
            if (distance < 1.4f)
            {
                introduceUI.enabled = true;//显示UI
                MangerOfMainMenu.ShowIntroduceOnUI(introduces[index]);//显示介绍
            }

            //通过A/D或左右箭头切换对焦对象
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                IndexSubtract();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                IndexPlus();
            }

            /*平滑移动到对应的对象上*/
            transform.position = Vector3.Lerp(transform.position, targets[index].transform.position, 2.5f * Time.deltaTime);
            /*平滑旋转角度*/
            transform.rotation = Quaternion.Lerp(transform.rotation, targets[index].transform.rotation, 2.5f * Time.deltaTime);
        }//介绍界面
    }

    /*移动摄像头到指定位置*/
    void CameraMoveTo(GameObject target)
    {
        /*平滑移动到对应的对象上*/
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.85f * Time.deltaTime);
        /*平滑旋转角度*/
        transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, 0.9f * Time.deltaTime);
    }

    /*关闭除了mainUI之外的所有UI*/
    public void CloseUI()
    {
        introduceUI.enabled = false;
        achievementUI.enabled = false;
        optionsUI.enabled = false;
        startGameUI.enabled = false;
    }

    /*切换对焦对象*/
    public void IndexPlus()
    {
        //进行循环
        if (index == 10)
            index = 0;
        else
            index++;
    }
    public void IndexSubtract()
    {
        //进行循环
        if (index == 0)
            index = 10;
        else
            index--;
    }

}
