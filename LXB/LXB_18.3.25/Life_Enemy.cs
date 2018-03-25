using UnityEngine;
using UnityEngine.AI;

public class Life_Enemy : MonoBehaviour
{
    /// <summary>
    /// 杀死得到的想象力
    /// </summary>
    public int image;
    /// <summary>
    /// 杀死的得分
    /// </summary>
    public int score;
    /// <summary>
    /// 分数管理器
    /// </summary>
    public GameObject scoreManager;
    /// <summary>
    /// 受伤的声音
    /// </summary>
    public AudioSource hurtSound;
    /// <summary>
    /// 死亡的声音
    /// </summary>
    public AudioSource deathSound;
    /// <summary>
    /// 血量
    /// </summary>
    public float hp;
    /// <summary>
    /// 攻击力
    /// </summary>
    public float demage;
    [HideInInspector]
    /// <summary>
    /// 收到炸弹的吸引
    /// </summary>
    public bool interestInBomb = false;

    /// <summary>
    /// 动画状态机
    /// </summary>
    private Animator enemyAnima;
    /// <summary>
    /// 玩家
    /// </summary>
    private GameObject player;
    private Transform playerPosi;
    /// <summary>
    /// 代理人
    /// </summary>
    public NavMeshAgent agent;
    private float timer;
    /// <summary>
    /// 燃烧特效
    /// </summary>
    private ParticleSystem onFire;
    /// <summary>
    /// 受到攻击时死去的时候
    /// </summary>
    private bool firstDeath = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerPosi = player.transform; 
        enemyAnima = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        onFire = GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        /*活着的时候*/
        if(!enemyAnima.GetBool("death"))
        {
            /*跟随主角*/
            if (!interestInBomb)
            {
                if (agent.isActiveAndEnabled)
                    agent.SetDestination(playerPosi.position);
            }
            /*动画播放*/
            if (GetComponent<Rigidbody>().velocity == Vector3.zero)
                enemyAnima.SetBool("move", false);
            else
                enemyAnima.SetBool("move", true);

            /*白天的时候*/
            if(Manager_DayChange.dayState == Manager_DayChange.Day.day)
            {
                /*扣血*/
                if (TotalManger.GetDifficulty() == "easy")
                    hp -= 15 * Time.deltaTime;
                else if (TotalManger.GetDifficulty() == "normal")
                    hp -= 10 * Time.deltaTime;
                else if (TotalManger.GetDifficulty() == "hard")
                    hp -= 7 * Time.deltaTime;
                /*着火*/
                onFire.Play();
            }
            else
            {
                /*不着火*/
                onFire.Stop();
            }

            /*死亡控制*/
            if (hp <= 0) 
            {
                /*关闭自动寻路*/
                agent.enabled = false;
                /*播放死亡音效*/
                deathSound.Play();
                /*播放死亡动画*/
                enemyAnima.SetBool("death", true);
                /*关掉碰撞器（沉入地下）*/
                GetComponent<Collider>().enabled = false;
                /*两秒后销毁物体*/
                Invoke("DestroyIt", 2f);
            }
        }       
    }

    /*攻击玩家*/
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            other.collider.GetComponent<Life_Player_EndlessGame>().TakeDemage(demage);
            timer = 0;
        }
    }
    void OnCollisionStay(Collision other)
    {
        timer += Time.deltaTime;
        if (other.collider.tag == "Player" && timer > 1.5f)
        {
            other.collider.GetComponent<Life_Player_EndlessGame>().TakeDemage(demage);
            timer = 0;
        }
    }

    /// <summary>
    /// 减少敌人的血量
    /// </summary>
    /// <param name="demage">伤害值的大小</param>
    public void TakeDemage(float demage) {
        hp -= demage; hurtSound.Play();
        /*通过TakeDemage方法死亡的 增加游戏得分*/
        if (hp < 0 && !enemyAnima.GetBool("death") && !firstDeath)
        {
            firstDeath = true;
            /*增加得分*/
            scoreManager.GetComponent<ManagerScore>().GetScore(score);
            /*增加想象力*/
            Manager_Update.imageScore += image;
        }
    }
    /// <summary>
    /// 销毁物体
    /// </summary>
    public void DestroyIt() { Destroy(gameObject); }
    /// <summary>
    /// 暂停导航
    /// </summary>
    /// <param name="time">暂停的时间</param>
    public void StopMove(float time)
    {
        agent.enabled = false;
        Invoke("Remove", time);
    }
    private void Remove()
    {
        agent.enabled = true;
    }
}
