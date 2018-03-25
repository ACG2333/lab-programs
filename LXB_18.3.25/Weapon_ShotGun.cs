using UnityEngine;

public class Weapon_ShotGun : MonoBehaviour
{

    //变量
    [Header("武器属性")]
    /// <summary>
    /// 武器名称
    /// </summary>
    public string WeaponName;
    /// <summary>
    /// 武器造成的攻击力
    /// </summary>
    public float AttackPower;
    /// <summary>
    /// 武器的射击距离
    /// </summary>
    public float AttackDistance;
    /// <summary>
    /// 是否有后座力
    /// </summary>
    public bool haveBackForce;
    /// <summary>
    /// 后座力
    /// </summary>
    public float backForce;

    [Header("子弹")]
    /// <summary>
    /// 能容纳的最大子弹数目
    /// </summary>
    public int maxBulletsNumber;
    /// <summary>
    /// 剩余的子弹数量
    /// </summary>
    public int bulletsLeft;
    /// <summary>
    /// 射击的间隔 
    /// </summary>
    public float shootPerior;

    [Header("击中效果")]
    /// <summary>
    /// 击中特效
    /// </summary>
    public ParticleSystem hitEffect;
    /// <summary>
    /// 击中灯光
    /// </summary>
    public Light hitLight;
    /// <summary>
    /// 灯光范围的浮动
    /// </summary>
    public float hitLightRangFloat;
    /// <summary>
    /// 灯光强度的浮动
    /// </summary>
    public float hitLightIntensityFloat;

    [Header("发射效果")]
    /// <summary>
    /// 发射特效
    /// </summary>
    public ParticleSystem shootEffect;
    /// <summary>
    /// 发射灯光
    /// </summary>
    public Light shootLight;
    /// <summary>
    /// 发射灯光范围的浮动
    /// </summary>
    public float shootLightRangFloat;
    /// <summary>
    /// 发射灯光强度的浮动
    /// </summary>
    public float shootLightIntensityFloat;
    /// <summary>
    /// 弹道轨迹
    /// </summary>
    public GameObject shootLine;
    /// <summary>
    /// 开枪的声音
    /// </summary>
    public AudioSource shootSound;
    /// <summary>
    /// 没子弹的声音
    /// </summary>
    public AudioSource emptySound;
    
    [Header("枪口")]
    public Transform muzzle1;
    public Transform muzzle2;
    public Transform muzzle3;
    public Transform muzzle4;
    public Transform muzzle5;
    public Transform muzzle6;
    public Transform muzzle7;
    public Transform muzzle8;
    public Transform muzzle9;

    /// <summary>
    /// 存储9个开枪方向
    /// </summary>
    private Transform[] shootPositions = new Transform[9];
    /// <summary>
    /// 存储9条射线
    /// </summary>
    private GameObject[] lines = new GameObject[9];
    /// <summary>
    /// 间隔开枪的计时器
    /// </summary>
    private float timer;
    /// <summary>
    /// 用来间隔开枪的布尔值
    /// </summary>
    private bool shootAble = true;
    /// <summary>
    /// 玩家的刚体
    /// </summary>
    private Rigidbody playerRig;

    void Start()
    {
        {
            shootPositions[0] = muzzle1;
            shootPositions[1] = muzzle2;
            shootPositions[2] = muzzle3;
            shootPositions[3] = muzzle4;
            shootPositions[4] = muzzle5;
            shootPositions[5] = muzzle6;
            shootPositions[6] = muzzle7;
            shootPositions[7] = muzzle8;
            shootPositions[8] = muzzle9;
        }//射击点的赋值

        /*获取角色刚体*/
        playerRig = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        /*控制射击间隔*/
        if (!shootAble)
        {
            timer += Time.deltaTime;
            if (timer > shootPerior)
            {
                shootAble = true;
                timer = 0;
            }
        }

        /*按下鼠标左键攻击*/
        if (Input.GetMouseButtonDown(0) && ManagerGame.GameState == ManagerGame.State.gaming &&
                !playerRig.transform.GetComponent<Animator>().GetBool("death"))
        {
            /*判断是否有子弹*/
            if (bulletsLeft > 0)
            {
                if (shootAble)
                {
                    Attack();
                    shootAble = false;
                }
            }
            else
            {
                emptySound.Play();
            }
        }
    }

    /// <summary>
    /// 进行攻击,对带有“Enemy”标签的物体进行攻击
    /// </summary>
    public void Attack()
    {
        /*发射效果*/
        shootEffect.Play();

        /*打开闪光*/
        shootLight.enabled = true;

        /*播放音效*/
        shootSound.Play();

        /*减去1个弹药*/
        bulletsLeft--;

        /*散开射击*/
        for (int i = 0; i < 9; i++)
        {
            /*从枪口位置发出射线*/
            lines[i] = Instantiate(shootLine);//实例化射线
            Ray ray = new Ray(shootPositions[i].position, shootPositions[i].forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, AttackDistance))//射中物体
            {
                /*设置射线终点*/
                lines[i].GetComponent<LineRenderer>().SetPosition(1, hitInfo.point);
                /*击中敌人*/
                if (hitInfo.collider.tag == "Enemy")
                {
                    /*给敌人造成伤害*/
                    hitInfo.collider.GetComponent<Life_Enemy>().TakeDemage(AttackPower);
                }
            }
            else//没射中物体
            {
                /*设置射线终点*/
                lines[i].GetComponent<LineRenderer>().SetPosition(1, shootPositions[i].position + shootPositions[i].forward * AttackDistance);
            }
            lines[i].GetComponent<LineRenderer>().SetPosition(0, shootPositions[i].position);//设置射线起点
            lines[i].GetComponent<LineRenderer>().enabled = true;
        }
        Invoke("CloseShootLine", 0.03f);//关闭射线

        /*后座力*/
        if(haveBackForce)
        {
            playerRig.AddForce((Vector3.zero - muzzle1.forward) * backForce, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// 关闭射击效果
    /// </summary>
    void CloseShootLine()
    {
        /*关闭射线*/
        for (int i = 0; i < 9; i++)
        {
            Destroy(lines[i]);
        }
        /*关闭灯光*/
        shootLight.enabled = false;
    }

    /// <summary>
    /// 给武器增加弹药
    /// </summary>
    /// <param name="bulletsNumber">增加弹药的数量</param>
    /// <returns>是否成功增加弹药</returns>
    public bool AddBullets(int bulletsNumber)
    {
        /*判断剩余弹药的情况再增加*/
        if (bulletsLeft < maxBulletsNumber - bulletsNumber)
        {
            /*增加但要后未满则直接增加指定数量的弹药*/
            bulletsLeft += bulletsNumber;
            return true;
        }
        else if (bulletsLeft < maxBulletsNumber)
        {
            /*增加后超过最大载弹量的加到满为止*/
            bulletsLeft = maxBulletsNumber;
            return true;
        }
        else/*弹药是满的不增加弹药*/
            return false;
    }
}
