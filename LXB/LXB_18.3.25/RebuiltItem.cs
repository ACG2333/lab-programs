using UnityEngine;

public class RebuiltItem : MonoBehaviour {

    /*要实例化的物体*/
    public GameObject chesses;
    public GameObject fires;
    public GameObject weaponBox;

    /*实例化的物体*/
    private GameObject item;
    /*随机数*/
    private float n;

	void Start () {
        /*开始*/
        /*随机出要实例化的的物体*/
        n = Random.Range(0, 10);
        if (n < 4.8f)
            item = weaponBox;
        else if (n < 8.5f)
            item = fires;
        else
            item = chesses;

        /*实例化物体成为该物体的子物体*/
        GameObject theItem = Instantiate(item, transform);
        theItem.SetActive(true);
    }

    /*当玩家离开刷新点一定距离*/
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            /*实例化物体*/
            if (transform.childCount < 4)
            {
                /*随机出要实例化的的物体*/
                n = Random.Range(0, 10);
                if (n < 4.8f)
                    item = weaponBox;
                else if (n < 8.5f)
                    item = fires;
                else
                    item = chesses;

                /*实例化物体成为该物体的子物体*/
                GameObject theItem = Instantiate(item, transform);
                theItem.SetActive(true);
            }
        }
    }
}
