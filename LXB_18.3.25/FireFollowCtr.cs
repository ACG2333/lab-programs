using UnityEngine;

public class FireFollowCtr : MonoBehaviour {

    private Light fireLight;
    private float rang;
    private float intensity;
    private float timer;
    private float lightTime;

	void Start () {
        fireLight = GetComponent<Light>();
        rang = 7;
        intensity = 1;
        lightTime = 1;
	}
	
	void Update () {

        /*计时*/
        timer += Time.deltaTime;

        /*控制光的变化*/
        if(timer < lightTime)
        {
            //print(fireLight.range);
            /*变化范围*/
            fireLight.range = Mathf.Lerp(fireLight.range, rang, 0.15f);
            /*变化亮度*/
            fireLight.intensity = Mathf.Lerp(fireLight.intensity, intensity, 0.15f);
        }
        else
        {
            /*初始化计时器*/
            timer = 0;
            /*随机变化时间*/
            lightTime = Random.Range(0.115f, 0.2f);
            /*随机亮度*/
            intensity = Random.Range(0.75f, 1.1f);
            /*根据亮度改变范围*/
            rang = intensity * 8.5f;
        }
	}
}
