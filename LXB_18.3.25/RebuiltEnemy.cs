using UnityEngine;

public class RebuiltEnemy : MonoBehaviour {

    [Header("bunny")]
    /// <summary>
    /// 最初刷怪时间
    /// </summary>
    private float bunnyReBuiltTime;
    /// <summary>
    /// 敌人物体
    /// </summary>
    public GameObject EnemyBunny;

    [Header("bear")]
    /// <summary>
    /// 最初刷怪时间
    /// </summary>
    private float bearReBuiltTime;
    /// <summary>
    /// 敌人物体
    /// </summary>
    public GameObject EnemyBear;

    [Header("hellephant")]
    /// <summary>
    /// 最初刷怪时间
    /// </summary>
    private float hellephantReBuiltTime;
    /// <summary>
    /// 敌人物体
    /// </summary>
    public GameObject EnemyHellephant;

    /// <summary>
    /// 计时器
    /// </summary>
    private float timer = 0;

    /*结构体*/
    private struct Enemy
    {
        /*刷新时间*/
        public float time;
        /*刷新物体*/
        public GameObject obj;
    }
    private Enemy bunny;
    private Enemy bear;
    private Enemy hellephant;
    private Enemy enemy;

    private float shortTime;

    /*随机数*/
    private float randomFloat;

	void Start () {

        /*判断难度*/
        if(TotalManger.GetDifficulty() == "easy")
        {
            shortTime = 0.05f;
            bunnyReBuiltTime = 9;
            bearReBuiltTime = 8.5f;
            hellephantReBuiltTime = 15;
        }
        else if(TotalManger.GetDifficulty()== "normal")
        {
            shortTime = 0.08f;
            bunnyReBuiltTime = 5f;
            bearReBuiltTime = 7;
            hellephantReBuiltTime = 9;
        }
        else
        {
            shortTime = 0.085f;
            bunnyReBuiltTime = 3.8f;
            bearReBuiltTime = 5f;
            hellephantReBuiltTime = 6.5f;
        }

        /*buny*/
        bunny.time = bunnyReBuiltTime;
        bunny.obj = EnemyBunny;

        /*bear*/
        bear.time = bearReBuiltTime;
        bear.obj = EnemyBear;

        /*hellephant*/
        hellephant.time = hellephantReBuiltTime;
        hellephant.obj = EnemyHellephant;

        /*enemy初始化*/
        enemy = bear;
    }
	
	void Update () {

        /*计时*/
        timer += Time.deltaTime;

        if (timer >= enemy.time)
        {
            /*初始化计时器*/
            timer = 0;

            /*实例化敌人*/
            if (Manager_DayChange.dayState == Manager_DayChange.Day.night)
            {
                GameObject theEnemy = Instantiate(enemy.obj, transform.position, transform.rotation);
                theEnemy.SetActive(true);
            }

            /*随机下一个敌人*/
            randomFloat = Random.Range(0, 10);
            if (randomFloat < 5.5f)
            {
                enemy = bear;
                if (enemy.time > 0.75f)
                    bear.time -= shortTime;
            }
            else if (randomFloat < 8.5f)
            {
                enemy = bunny;
                if (enemy.time > 0.5f)
                    bunny.time -= shortTime;
            }
            else
            {
                enemy = hellephant;
                if (enemy.time > 1.5f)
                    hellephant.time -= shortTime;
            }
        }
    }
}
