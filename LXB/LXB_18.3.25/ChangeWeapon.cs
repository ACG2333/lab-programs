using UnityEngine;

public class ChangeWeapon : MonoBehaviour {

    public AudioSource changeSound;

    [Header("自动步枪")]
    public GameObject rifle;

    [Header("霰弹枪")]
    public GameObject shotGun;

    [Header("手榴弹枪")]
    public GameObject grenadeGun;

    public enum Weapon
    {
        rifle,
        shotGun,
        grenadeGun
    }

    public Weapon weaponState = Weapon.rifle;
	
	void Update () {

        /*通过按键切换武器*/
        if (!GetComponent<Animator>().GetBool("death"))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (weaponState != Weapon.rifle)
                {
                    changeSound.Play();
                    weaponState = Weapon.rifle;
                    OpenRifle();
                    CloseShotGun();
                    CloseGrenadeGun();
                }
            }//自动步枪
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (weaponState != Weapon.shotGun)
                {
                    changeSound.Play();
                    weaponState = Weapon.shotGun;
                    OpenShotGun();
                    CloseRifle();
                    CloseGrenadeGun();
                }
            }//霰弹枪
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (weaponState != Weapon.grenadeGun)
                {
                    changeSound.Play();
                    weaponState = Weapon.grenadeGun;
                    OpenGrenadeGun();
                    CloseRifle();
                    CloseShotGun();
                }
            }//手榴弹枪
        }
    }

    /*自动步枪*/
    void CloseRifle()
    {
        rifle.GetComponent<SkinnedMeshRenderer>().enabled = false;
        rifle.GetComponent<LineRenderer>().enabled = false;
        rifle.GetComponent<Weapon_Rifle>().enabled = false;
        rifle.transform.Find("muzzle").gameObject.SetActive(false);
    }
    void OpenRifle()
    {
        rifle.GetComponent<SkinnedMeshRenderer>().enabled = true;
        rifle.GetComponent<Weapon_Rifle>().enabled = true;
        rifle.transform.Find("muzzle").gameObject.SetActive(true);
    }

    /*霰弹枪*/
    void CloseShotGun()
    {
        shotGun.SetActive(false);
    }
    void OpenShotGun()
    {
        shotGun.SetActive(true);
    }

    /*手榴弹枪*/
    void CloseGrenadeGun()
    {
        grenadeGun.SetActive(false);
    }
    void OpenGrenadeGun()
    {
        grenadeGun.SetActive(true);
    }
}
