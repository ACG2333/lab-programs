using UnityEngine;

public class Weapon_Bomb : MonoBehaviour {

    /// <summary>
    /// 爆炸特效和灯光
    /// </summary>
    public GameObject boomEffect;
    /// <summary>
    /// 爆炸音效
    /// </summary>
    public GameObject boomSound;
    /// <summary>
    /// 爆炸力度
    /// </summary>
    public float boomPower = 500;
    /// <summary>
    /// 爆炸伤害
    /// </summary>
    public float boomDemage;
    /// <summary>
    /// 爆炸时间
    /// </summary>
    public float boomTime = 2;
    /// <summary>
    /// 爆炸范围
    /// </summary>
    public float boomRang = 2;
    /// <summary>
    /// 计时器
    /// </summary>
    private float timer;
    /// <summary>
    /// 正在爆炸
    /// </summary>
    private bool booming = false;
    

	void Update () {
        /*计时*/
        timer += Time.deltaTime;

        /*及时完成后发生爆炸*/
        if (timer >= boomTime && !booming)
        {
            /*切换状态*/
            booming = true;

            /*播放特效灯光*/
            GameObject theEffect = Instantiate(boomEffect, transform.position, transform.rotation);
            theEffect.SetActive(true);

            /*播放音效*/
            GameObject theBoomSound = Instantiate(boomSound, transform.position, transform.rotation);
            theBoomSound.GetComponent<AudioSource>().Play();

            /*造成伤害*/
            Collider[] colliders = Physics.OverlapSphere(transform.position, boomRang);
            foreach (var item in colliders)
            {
                if(item.GetComponent<Rigidbody>())
                {
                    /*攻击到敌人*/
                    if (item.tag == "Enemy")
                    {
                        item.GetComponent<Life_Enemy>().StopMove(0.5f);
                        item.GetComponent<Rigidbody>().AddExplosionForce(boomPower * 8, transform.position, boomRang);
                        /*根据距离计算伤害*/
                        item.GetComponent<Life_Enemy>().TakeDemage(boomDemage / (Vector3.Distance(transform.position, item.transform.position)));
                    }
                    else if (item.tag == "Player")//击中玩家
                    {
                        item.GetComponent<Rigidbody>().AddExplosionForce(boomPower * 3, transform.position, boomRang);
                        /*简单难度不受伤害*/
                        if (TotalManger.GetDifficulty() != "easy")
                            item.GetComponent<Life_Player_EndlessGame>().TakeDemage((boomDemage / (Vector3.Distance(transform.position, item.transform.position))) / 15);
                    }
                    else//击中其他物体
                    {
                        item.GetComponent<Rigidbody>().AddExplosionForce(boomPower, transform.position, boomRang);
                    }
                }
            }

            Collider[] colliders2 = Physics.OverlapSphere(transform.position, 50);
            foreach (var item in colliders2)
            {
                if (item.GetComponent<Rigidbody>())
                {
                    /*攻击到敌人*/
                    if (item.tag == "Enemy")
                    {    
                        /*停止吸引炸弹*/
                        item.GetComponent<Life_Enemy>().interestInBomb = false;
                    }
                }
            }

            /*销毁物体*/
            Destroy(gameObject);
        }
	}

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            /*被炸弹吸引*/
            other.GetComponent<Life_Enemy>().interestInBomb = true;
            /*跟随炸弹*/
            if(other.GetComponent<Life_Enemy>().agent.isActiveAndEnabled)
                other.GetComponent<Life_Enemy>().agent.SetDestination(transform.position);
        }
    }
}
