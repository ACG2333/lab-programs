using UnityEngine;

public class AvoidBarrier : MonoBehaviour {

    /*位置1*/
    public Transform posi1;
    /*位置2*/
    public Transform posi2;

    private Transform target;

    void Start()
    {
        target = posi1;
    }

	void Update () {

        transform.position = Vector3.Lerp(transform.position, target.position, 1.7f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 1.7f * Time.deltaTime);

        //if (Input.GetMouseButton(1))
        //    target = posi2;
        //else
        //    target = posi1;

        if(target == posi1)
        {
            if (Camera.main.fieldOfView > 60)
                Camera.main.fieldOfView -= 15 * Time.deltaTime;
        }
        else
        {
            if (Camera.main.fieldOfView < 85)
                Camera.main.fieldOfView += 10 * Time.deltaTime;
        }
	}

    /*摄像头碰到障碍*/
    void OnTriggerStay(Collider other)
    {
        if (other.name != "Old-timer bomb shoot(Clone)")
        {
            target = posi2;
        }
    }
    /*摄像头离开障碍*/
    void OnTriggerExit(Collider other)
    {
        if (other.name != "Old-timer bomb shoot(Clone)")
        {
            target = posi1;
        }
    }

}
