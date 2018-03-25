using UnityEngine;

public class Weapon_GrenadeGun_Bullet : MonoBehaviour {

    public GameObject boomSound;
    public GameObject boomEffect;

    public float boomRang;
    public float boomPower;
    public float boomDemage;

    void OnCollisionEnter(Collision other)
    {
        /*播放特效*/
        GameObject theEffect = Instantiate(boomEffect, transform.position, transform.rotation);
        theEffect.SetActive(true);

        /*播放声音*/
        GameObject theSound = Instantiate(boomSound, transform.position, transform.rotation);
        theSound.GetComponent<AudioSource>().Play();

        /*攻击*/
        Collider[] colliders = Physics.OverlapSphere(transform.position, boomRang);
        foreach (var item in colliders)
        {
            if (item.GetComponent<Rigidbody>())
            {
                /*攻击到敌人*/
                if (item.tag == "Enemy")
                {
                    item.GetComponent<Life_Enemy>().StopMove(0.5f);
                    item.GetComponent<Rigidbody>().AddExplosionForce(boomPower * 8, transform.position, boomRang);
                    /*通过距离计算伤害*/
                    item.GetComponent<Life_Enemy>().TakeDemage(boomDemage / (Vector3.Distance(transform.position, item.transform.position)));
                }
                else if (item.tag == "Player")//击中玩家
                {
                    item.GetComponent<Rigidbody>().AddExplosionForce(boomPower * 3, transform.position, boomRang);
                    if (TotalManger.GetDifficulty() != "easy")
                        item.GetComponent<Life_Player_EndlessGame>().TakeDemage((boomDemage / (Vector3.Distance(transform.position, item.transform.position))) / 15);
                }
                else//击中其他物体
                {
                    item.GetComponent<Rigidbody>().AddExplosionForce(boomPower, transform.position, boomRang);
                }
            }
        }

        /*销毁物体*/
        Destroy(gameObject);
    }
}
