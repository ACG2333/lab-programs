using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour {

    [Header("UI")]
    public Slider hpSlider;
    public Text hp;
    public Text bulletsOfGun;
    public Text bulletsOfBomb;
    public Text score;
    public Text nowScore;
    public Text HighScore;
    public Text imageScore;

    [Header("数据")]
    public GameObject shotGun;
    public GameObject rifle;
    public GameObject grenadeGun;
    public GameObject bomb;
    public GameObject player;
    public GameObject changeWeapon;
    public GameObject scoreManger;

    /*当前难度的最高分*/
    private int scoreInDiff;

	void Update () {

        /*根据难度判断当前要显示的最高分*/
        if (TotalManger.GetDifficulty() == "easy")
            scoreInDiff = TotalManger.GetHighScoreE();
        else if (TotalManger.GetDifficulty() == "normal")
            scoreInDiff = TotalManger.GetHighScoreN();
        else if (TotalManger.GetDifficulty() == "hard")
            scoreInDiff = TotalManger.GetHighScoreH();

        /*显示想象力*/
        imageScore.text = "想象力:" + Manager_Update.imageScore;

        /*显示得分*/
        score.text = "得分:" + scoreManger.GetComponent<ManagerScore>().score;
        nowScore.text = "当前得分:" + scoreManger.GetComponent<ManagerScore>().score;
        HighScore.text = "最高分:" + scoreInDiff;

        /*显示血量*/
        hp.text = "血量:" + player.GetComponent<Life_Player_EndlessGame>().hp;
        hpSlider.value = player.GetComponent<Life_Player_EndlessGame>().hp;
        hpSlider.maxValue = player.GetComponent<Life_Player_EndlessGame>().maxHp;

        /*显示子弹数目*/
        if (changeWeapon.GetComponent<ChangeWeapon>().weaponState == ChangeWeapon.Weapon.grenadeGun)
        {
            bulletsOfGun.text = grenadeGun.GetComponent<Weapon_GrenadeGun>().WeaponName + ":" + grenadeGun.GetComponent<Weapon_GrenadeGun>().bulletsLeft;
        }
        else if (changeWeapon.GetComponent<ChangeWeapon>().weaponState == ChangeWeapon.Weapon.rifle)
        {
            bulletsOfGun.text = rifle.GetComponent<Weapon_Rifle>().WeaponName + ":" + rifle.GetComponent<Weapon_Rifle>().bulletsLeft;
        }
        else
        {
            bulletsOfGun.text = shotGun.GetComponent<Weapon_ShotGun>().WeaponName + ":" + shotGun.GetComponent<Weapon_ShotGun>().bulletsLeft;
        }
        bulletsOfBomb.text = "炸弹：" + bomb.GetComponent<Weapon_Bomb_Cast>().bulletsLeft;
	}
}
