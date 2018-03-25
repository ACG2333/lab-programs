using UnityEngine;

public class Weapon_Rifle : MonoBehaviour {

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
    /// 按住射击的间隔 
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
    public LineRenderer shootLine;
    /// <summary>
    /// 开枪的声音
    /// </summary>
    public AudioSource shootSound;
    /// <summary>
    /// 没子弹的声音
    /// </summary>
    public AudioSource emptySound;

    /// <summary>
    /// 发起攻击的位置
    /// </summary>
    private Transform ShootPosition;
    /// <summary>
    /// 间隔开枪的计时器
    /// </summary>
    private float timer;
    /// <summary>
    /// 玩家的刚体
    /// </summary>
    private Rigidbody playerRig;

    void Start()
    {
        /*射击点的赋值*/
        ShootPosition = transform.Find("muzzle");

        /*开始关闭时关闭效果*/
        shootLine.enabled = false;
        shootLight.enabled = false;

        /*获取角色刚体*/
        playerRig = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        /*按住鼠标左键*/
        if (Input.GetMouseButton(0) && ManagerGame.GameState == ManagerGame.State.gaming &&
            !playerRig.transform.GetComponent<Animator>().GetBool("death"))
        {
            /*判断是否有子弹*/
            if (bulletsLeft > 0)
            {
                timer += Time.deltaTime;
                if (timer > shootPerior)
                {
                    Attack();
                    timer = 0;
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
        /*播放发射效果*/
        shootEffect.Play();

        /*播放音效*/
        shootSound.Play();

        /*减去1个弹药*/
        bulletsLeft--;

        /*打开开枪灯光*/
        shootLight.range = Random.Range(5 - shootLightRangFloat, 5 + shootLightRangFloat);
        shootLight.intensity = Random.Range(1.2f - shootLightIntensityFloat, 1.2f + shootLightIntensityFloat);
        shootLight.enabled = true;

        /*从枪口发出射线*/
        Ray ray = new Ray(ShootPosition.position, transform.Find("muzzle").forward);
        RaycastHit hitInfo;
        shootLine.SetPosition(0, ShootPosition.position);//设定射线开始点
        if (Physics.Raycast(ray, out hitInfo, AttackDistance))//设定射线结束点
        {
            shootLine.SetPosition(1, hitInfo.point);
            /*击中敌人*/
            if (hitInfo.collider.tag == "Enemy")
            {
                /*给敌人造成伤害*/
                hitInfo.collider.GetComponent<Life_Enemy>().TakeDemage(AttackPower);
            }
        }
        else
            shootLine.SetPosition(1, ShootPosition.position + ShootPosition.forward * AttackDistance);
        shootLine.enabled = true;//激活射线轨迹
        Invoke("CloseShootLine", shootPerior / 3.5f);//关闭射线轨迹

        /*后座力*/
        if (haveBackForce)
        {
            playerRig.AddForce((Vector3.zero - ShootPosition.forward) * backForce, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// 关闭射击效果
    /// </summary>
    void CloseShootLine()
    {
        /*关闭射线*/
        shootLine.enabled = false;
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
