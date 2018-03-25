using UnityEngine;

public class Item_WeaponBox : MonoBehaviour {

    /*武器*/
    public GameObject shotGun;
    public GameObject rifle;
    public GameObject grenadeGun;
    public GameObject bomb;
    public AudioSource getSound;

    private bool bool1;
    private bool bool2;
    private bool bool3;
    private bool bool4;

    void Update () {
        /*物体旋转*/
        transform.eulerAngles += new Vector3(0, 40, 0) * Time.deltaTime;
	}

    void OnTriggerEnter(Collider other)
    {
        /*增加弹药*/
        if (other.tag == "Player")
        {
            /*判断增加情况，有一个增加成功就成功*/
            bool1 = shotGun.GetComponent<Weapon_ShotGun>().AddBullets(10);
            bool2 = rifle.GetComponent<Weapon_Rifle>().AddBullets(100);
            bool3 = bomb.GetComponent<Weapon_Bomb_Cast>().AddBullets(4);
            bool4 = grenadeGun.GetComponent<Weapon_GrenadeGun>().AddBullets(15);
            if (bool1 || bool2 || bool3 || bool4)
            {
                getSound.Play();
                Destroy(gameObject);
            }
        }
    }
}
