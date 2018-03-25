using UnityEngine;

public class Item_Food : MonoBehaviour {

    /// <summary>
    /// 增加的血量
    /// </summary>
    public float addHp;
    /// <summary>
    /// 拾起的声音
    /// </summary>
    public AudioSource getSound;

    void Update()
    {
        /*物体旋转*/
        transform.eulerAngles += new Vector3(0, 40, 0) * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        /*增加弹药*/
        if (other.tag == "Player")
        {
            /*判断增加情况，有一个增加成功就成功*/
            if (other.GetComponent<Life_Player_EndlessGame>().AddHp(addHp))
            {
                getSound.Play();
                Destroy(gameObject);
            }
        }
    }
}
