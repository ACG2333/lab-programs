using UnityEngine;

public class Weapon_Bomb_Cast : MonoBehaviour {

    /// <summary>
    /// 炸弹的实体
    /// </summary>
    public GameObject Bomb;
    /// <summary>
    /// 抛出的力度
    /// </summary>
    public float castPower;
    /// <summary>
    /// 最大的弹药量
    /// </summary>
    public ushort maxBulletsNumber;
    /// <summary>
    /// 剩余弹药量
    /// </summary>
    public ushort bulletsLeft;
    /// <summary>
    /// 射击的间隔 
    /// </summary>
    public float shootPerior;
    /// <summary>
    /// 抛出炸弹的方向
    /// </summary>
    public Transform shootDirection;
    /// <summary>
    /// 炸弹计时器声音
    /// </summary>
    public GameObject timerSound;

    /// <summary>
    /// 间隔开枪的计时器
    /// </summary>
    private float timer;
    /// <summary>
    /// 用来间隔开枪的布尔值
    /// </summary>
    private bool shootAble = true;
	
	void FixedUpdate () {

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

        /*按下G投射炸弹*/
        if (Input.GetKeyDown(KeyCode.G) && 
            ManagerGame.GameState == ManagerGame.State.gaming && shootAble)
        {
            /*有子弹*/
            if (bulletsLeft > 0 &&
                !GameObject.FindWithTag("Player").transform.GetComponent<Animator>().GetBool("death"))
            {
                /*切换状态*/
                shootAble = false;

                /*子弹减一*/
                bulletsLeft--;

                /*播放音效*/
                GameObject theTimer = Instantiate(timerSound, transform.position, transform.rotation);
                theTimer.GetComponent<AudioSource>().Play();

                /*实例化炸弹*/
                GameObject theBomb = Instantiate(Bomb, transform.position, new Quaternion());
                theBomb.SetActive(true);
                theBomb.GetComponent<Rigidbody>().AddForce(shootDirection.forward * castPower, ForceMode.Impulse);
            }
        }
	}

    /// <summary>
    /// 给武器增加弹药
    /// </summary>
    /// <param name="bulletsNumber">增加弹药的数量</param>
    /// <returns>是否成功增加弹药</returns>
    public bool AddBullets(ushort bulletsNumber)
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
