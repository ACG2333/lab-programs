using UnityEngine;

public class Weapon_GrenadeGun : MonoBehaviour {
    
    //变量
    [Header("武器属性")]
    /// <summary>
    /// 武器名称
    /// </summary>
    public string WeaponName;
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
    /// <summary>
    /// 弹药
    /// </summary>
    public GameObject bullet;

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
    /// 开枪的声音
    /// </summary>
    public AudioSource shootSound;
    /// <summary>
    /// 没子弹的声音
    /// </summary>
    public AudioSource emptySound;
    /// <summary>
    /// 投射的力量
    /// </summary>
    public float shootPower;

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
    /// <summary>
    /// 枪口
    /// </summary>
    private Transform muzzle;

    void Start()
    {
        /*获取角色刚体*/
        playerRig = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

        /*获取枪口*/
        muzzle = transform.Find("muzzle");

        /*关闭灯光*/
        shootLight.enabled = false;
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
        /*打开闪光*/
        shootLight.enabled = true;
        /*关闭灯光*/
        Invoke("CloseLight", 0.1f);

        /*播放音效*/
        shootSound.Play();

        /*减去1个弹药*/
        bulletsLeft--;

        /*实例化弹药*/
        GameObject theBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
        theBullet.SetActive(true);
        /*投射出弹药*/
        theBullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * shootPower, ForceMode.Impulse);

        /*后座力*/
        if (haveBackForce)
        {
            playerRig.AddForce((Vector3.zero - muzzle.forward) * backForce, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// 关闭灯光
    /// </summary>
    void CloseLight()
    {
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
