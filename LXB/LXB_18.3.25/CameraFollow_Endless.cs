using UnityEngine;

public class CameraFollow_Endless : MonoBehaviour {

    /// <summary>
    /// 角色的位置
    /// </summary>
    public Transform playerPosi;

	void FixedUpdate () {
        /*跟随角色*/
        //transform.position = Vector3.Lerp(transform.position, playerPosi.position + new Vector3(0,6.8f,-7f), 3 * Time.deltaTime);
        FoucsOn(playerPosi,2.5f);
    }

    void FoucsOn(Transform item, float t)
    { 
        transform.position = Vector3.Lerp(transform.position, item.position, t * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, item.rotation, t * Time.deltaTime);
    }
}
