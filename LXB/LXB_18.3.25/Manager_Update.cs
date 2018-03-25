using UnityEngine;

public class Manager_Update : MonoBehaviour {

    /*想象力点数*/
    public static int imageScore = 0;

    /*用于修改的物体*/
    public Life_Player_EndlessGame player;
    public Weapon_Bomb_Cast bomb;
    public Weapon_GrenadeGun grenadeGun;
    public Weapon_Rifle rifle;
    public Weapon_ShotGun shotGun;
    public GameObject fire;
    public GameObject assist1;
    public GameObject assist2;
    public GameObject assist3;

    /*是否获取道具*/
    public static bool getFire =false;
    public static bool getAssist = false;

    /*角色属性*/
    public void UpMaxHp() {
        if (imageScore >= 200)
        {
            player.maxHp += 5;
            player.AddHp(20);
            imageScore -= 200;
        }
    }
    public void AddSpeed() {
        if(imageScore >= 250)
        {
            player.speed += 0.2f;
            imageScore -= 250;
        }
    }

    /*武器属性*/
    public void AddBombBullet() {
        if (imageScore >= 250)
        {
            bomb.maxBulletsNumber += 1;
            imageScore -= 250;
        }
    }
    public void AddGrenadeGunBullet() {
        if (imageScore >= 250)
        {
            grenadeGun.maxBulletsNumber += 5;
            imageScore -= 250;
        }
    }
    public void AddRifleBullet() {
        if (imageScore >= 200)
        {
            rifle.maxBulletsNumber += 50;
            imageScore -= 200;
        }
    }
    public void AddShotGunBullet() {
        if (imageScore >= 200)
        {
            shotGun.maxBulletsNumber += 10;
            imageScore -= 200;
        }
    }

    /*额外道具*/
    public void GetFire()
    {
        if (imageScore >= 650)
        {
            fire.SetActive(true);
            getFire = true;
            imageScore -= 650;
        }
    }
    public void GetAssist()
    {
        if (imageScore >= 650)
        {
            assist1.SetActive(true);
            assist2.SetActive(true);
            assist3.SetActive(true);
            getAssist = true;
            imageScore -= 650;
        }
    }

    void Start()
    {
        /*初始化*/
        getFire = false;
        getAssist = false;
        imageScore = 0;
    }
}
