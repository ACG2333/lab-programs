using UnityEngine;

public class Life_Player_EndlessGame : MonoBehaviour {

    /// <summary>
    /// 角色行走速度
    /// </summary>
    public float speed = 5;
    /// <summary>
    /// 角色血量
    /// </summary>
    public float hp = 100;
    /// <summary>
    /// 最大血量
    /// </summary>
    public float maxHp = 100;
    /// <summary>
    /// 受伤的声音
    /// </summary>
    public AudioSource hurtSound;
    /// <summary>
    /// 死亡的声音
    /// </summary>
    public AudioSource deathSound;

    private Light hurtLight;
    /// <summary>
    /// 角色的刚体
    /// </summary>
    private Rigidbody playerRig;
    /// <summary>
    /// 输入的角色行走方向向量
    /// </summary>
    private Vector3 directSpeed = Vector3.zero;
    /// <summary>
    /// 控制转身的四元数
    /// </summary>
    private Quaternion turnDirect;
    /// <summary>
    /// 角色动画状态机
    /// </summary>
    private Animator PlayerAnima;
    private int layerIndex = -1;
    /// <summary>
    /// 枪口的位置
    /// </summary>
    private Transform muzzle;
    private Vector3 pointVec;

    void Start () {
        /*获取刚体*/
        playerRig = GetComponent<Rigidbody>();
        /*获取转向的图层*/
        layerIndex = LayerMask.GetMask("Floor");
        /*获取动画状态机*/
        PlayerAnima = GetComponent<Animator>();
        /*在子物体中通过名字找到muzzle*/
        muzzle = transform.Find("muzzle");
        /*获取灯光*/
        hurtLight = GetComponent<Light>();
    }
	
	void FixedUpdate () {

        /*角色或者的时候*/
        if (!PlayerAnima.GetBool("death"))
        {
            {
                /*获取水平方向的输入*/
                directSpeed.x = Input.GetAxis("Horizontal");
                directSpeed.z = Input.GetAxis("Vertical");
                /*将速度单位化*/
                directSpeed.Normalize();
                /*移动角色*/
                playerRig.MovePosition(transform.position + directSpeed * speed * Time.deltaTime);
            }//控制移动

            {
                /*从摄像发出射线到鼠标制定的位置*/
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                /*检测到射线碰撞*/
                if (Physics.Raycast(ray, out hitInfo, 100, layerIndex))
                {
                    /*涉嫌碰撞点距离枪口的距离*/
                    float distance = Vector3.Distance(hitInfo.point, muzzle.position);
                    /*通过距离判断是用过角色位置转向还是枪口位置转换
                     近于1时候用角色位置转向，大于等于1时用枪口位置转换
                     用枪口位置转换确保鼠标指针的位置就是开枪击中的位置
                     近距离转换用于解决角色抽搐问题*/
                    if (distance < 1f)
                        turnDirect = Quaternion.LookRotation(hitInfo.point - transform.position + new Vector3(0, transform.position.y, 0));//加上new Vector3确保水平
                    else
                        turnDirect = Quaternion.LookRotation(hitInfo.point - muzzle.position + new Vector3(0, muzzle.position.y, 0));//加上new Vector3确保水平
                                                                                                                                     /*平滑转向，指针距离角色越远转向素的越快*/
                    playerRig.rotation = Quaternion.Lerp(playerRig.rotation, turnDirect, (15f + distance) * Time.deltaTime);
                }
            }//控制转向

            {
                /*通过是否方向向量的大小控制行走动画*/
                if (directSpeed == Vector3.zero)
                {
                    PlayerAnima.SetBool("move", false);
                }
                else
                    PlayerAnima.SetBool("move", true);
            }//动画控制

            /*死亡控制*/
            if (hp <= 0)
            {
                PlayerAnima.SetBool("death", true);
                deathSound.Play();
            }
        }
    }

    /// <summary>
    /// 添加伤害
    /// </summary>
    /// <param name="demaege">伤害值</param>
    public void TakeDemage(float demaege)
    {
        hp -= demaege;
        hurtSound.Play();
        hurtLight.enabled = true;
        Invoke("CloseLight", 0.2f);
    }
    private void CloseLight()
    {
        hurtLight.enabled = false;
    }
    /// <summary>
    /// 增加血量
    /// </summary>
    /// <param name="hp">增加的血量</param>
    /// <returns>是否成功加血</returns>
    public bool AddHp(float hp)
    {
        /*判断剩余血量*/
        if (this.hp < maxHp - hp)
        {
            this.hp += hp;
            return true;
        }
        else if (this.hp < maxHp)
        {
            this.hp = maxHp;
            return true;
        }
        else
            return false;
    }
}
