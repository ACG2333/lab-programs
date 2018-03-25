using UnityEngine;

public class Manager_DayChange : MonoBehaviour {

    /*灯光*/
    public GameObject dayLight;

    /*日光旋转的方向*/
    public Transform morningPosi;
    public Transform noonPosi;
    public Transform nightPosi;

    /*昼夜时间*/
    public float nightTime;
    public float dayTime;

    /*计时器*/
    private float timer;
    private float changeTimer1;
    private float changeTimer2;

    /*时间状态*/
    public static Day dayState;
    public enum Day {
        day,
        night,
    }

	void Awake () {
        /*游戏开始时是黑夜*/
        dayState = Day.night;
	}
	
	void Update () {
        /*计时*/
        timer += Time.deltaTime;

        /*日夜转换*/
        if (dayState == Day.night)
        {
            /*夜晚时间过了就切换到白天*/
            if (timer >= nightTime)
            {
                timer = 0;
                dayState = Day.day;
            }

            /*控制转换角度的计时器*/
            changeTimer1 = 0;
            changeTimer2 = 0;

        } else if(dayState == Day.day)
        {
            
            /*阳光变换*/
            if(timer < dayTime / 2)
            {
                changeTimer1 += Time.deltaTime / (dayTime / 2);
                /*角度转换*/
                dayLight.transform.rotation = Quaternion.Lerp(morningPosi.rotation, noonPosi.rotation, changeTimer1);
                /*亮度转换*/
                dayLight.GetComponent<Light>().intensity = Mathf.Lerp(0, 1.2f, changeTimer1);
            }
            else
            {
                changeTimer2 += Time.deltaTime / (dayTime / 2);
                /*角度转换*/
                dayLight.transform.rotation = Quaternion.Lerp(noonPosi.rotation, nightPosi.rotation, changeTimer2);
                /*亮度转换*/
                dayLight.GetComponent<Light>().intensity = Mathf.Lerp(1.2f, 0, changeTimer2);
            }

            /*白天时间过了就切换到夜晚*/
            if (timer >= dayTime && dayState != Day.night)
            {
                timer = 0;
                dayState = Day.night;

                /*阳光角度转换到早晨*/
                dayLight.transform.rotation = morningPosi.rotation;

                /*白天变短 黑夜变长*/
                if (dayTime > 7)
                {
                    dayTime--;
                    nightTime++;
                }
            }
        }
	}
}
